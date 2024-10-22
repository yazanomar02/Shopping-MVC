using Amazon.Domain.Models;
using Amazon.Infrastructure.Repositorys;
using AmazonMVCApp.Attributes;
using AmazonMVCApp.Filters;
using AmazonMVCApp.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace AmazonMVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Product> productRepository;
        private readonly IStringLocalizer<HomeController> stringLocalizer;

        public HomeController(
            ILogger<HomeController> logger,
            IRepository<Product> productRepository,
            IStringLocalizer<HomeController> stringLocalizer
            )
        {
            _logger = logger;
            this.productRepository = productRepository;
            this.stringLocalizer = stringLocalizer;
        }

        // ????? ????? ??? ????? ????????
        //[ViewData]
        //public string myString { get; set; } = "Hello World";



        //[ServiceFilter(typeof(TimerFilter))] // OR -->
        [TimerFilter]
        public IActionResult Index()
        {
            ViewData["Message"] = stringLocalizer["greeting_message"];
            return View();
        }

        [Route("/details/{productId:guid}/{slug:slugTramsformer}")]
        public async Task<IActionResult> ProductDetails
            (
            Guid productId,[RegularExpression("^[a-zA-Z0-9- ]+$")] string slug
            )
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var product = productRepository.Get(productId);

            return View(product);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public IActionResult ChangeLang(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Redirect(Request.Headers["Referer"].ToString()); // إعادة السمتخدم الى الصفحة التي كان  بها
        }

    }
}
