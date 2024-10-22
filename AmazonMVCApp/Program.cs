using Amazon.Domain.Models;
using Amazon.Infrastructure.DbContexts;
using Amazon.Infrastructure.Repositorys;
using AmazonMVCApp.Constraints;
using AmazonMVCApp.Filters;
using AmazonMVCApp.Repo;
using AmazonMVCApp.Transformers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLocalization(option => option.ResourcesPath = "Resources");

// Add services to the container.
builder.Services.AddControllersWithViews(option =>
{
    option.Filters.AddService<TimerFilter>();
}).AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
  .AddDataAnnotationsLocalization();



// اعدادات مكاتب اللغة
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedLanguages = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-YS")
    };  

    options.SupportedCultures = supportedLanguages;
    options.SupportedUICultures = supportedLanguages;

    options.DefaultRequestCulture = new RequestCulture("ar-SY");

    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider()
        //new AcceptLanguageHeaderRequestCultureProvider(),
    };
}
);



builder.Services.AddTransient<TimerFilter>();


builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IRepository<Cart>, CartRepository>();
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();


builder.Services.AddDbContext<AmazonDbContext>();

//Register Constraint and Transformer
builder.Services.AddRouting(option =>
{
    option.ConstraintMap["validateSlug"] = typeof(SlugConstraint);
    option.ConstraintMap["slugTramsformer"] = typeof(SlugOParameterTransformer);
});



// Register Session
builder.Services.AddSession();
// Register HttpContextAccessor
builder.Services.AddHttpContextAccessor();
// Register SessionStateRepository
builder.Services.AddTransient<IStateRepository, SessionStateRepository>();



// Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    option =>
    {
        option.ExpireTimeSpan = TimeSpan.FromMinutes(60); // عمر الكوكي
        option.LoginPath = "/Login"; // في حال لم يحقق سيوجه اليوزر الى صفحة تسجيل الدخول
        option.SlidingExpiration = true; // توليد كوكي جديد كل نصف الوقت
    });





var app = builder.Build();

app.UseSession(); //

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);

//app.MapControllerRoute(
//    name: "ProductDetalisRoute",
//    defaults: new { controller = "Home", action = "ProductDetails" },
//    pattern: "/details/{productId}/{slug?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    // أخذ انستانس من سيرفس مسجلة 
    //AmazonDbContext والتي لدي هنا هي 
    var context = scope.ServiceProvider.GetRequiredService<AmazonDbContext>();

    AmazonDbContext.CreateInitialTestingDatabase(context);
}


app.Run();
