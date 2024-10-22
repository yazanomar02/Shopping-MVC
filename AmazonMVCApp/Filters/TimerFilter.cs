using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace AmazonMVCApp.Filters
{
    public class TimerFilter : IAsyncActionFilter
    {
        private readonly ILogger<TimerFilter> logger;

        public TimerFilter(ILogger<TimerFilter> logger)
        {
            this.logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            await next();

            stopwatch.Stop();

            logger.LogInformation($"running for {stopwatch.ElapsedMilliseconds}");
        }
    }

}
