using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace AmazonMVCApp.Attributes
{
    public class TimerFilterAttribute : ActionFilterAttribute
    {

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<TimerFilterAttribute>>(); // جلب سيرفس ILogger

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            await next();

            stopwatch.Stop();

            logger.LogInformation($"running for {stopwatch.ElapsedMilliseconds}");
        }
    }
}
