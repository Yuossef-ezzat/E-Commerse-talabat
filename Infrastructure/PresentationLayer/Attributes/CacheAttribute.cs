using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstractionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Attributes
{
    public class CacheAttribute(int durationInSeconds = 100) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // create caching key
            //  Products?BrandId=1 
            var cacheKey = CreateCacheKey(context.HttpContext.Request);
            // search for value in cache with key
            var _cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var CacheValue = await _cacheService.GetAsync(cacheKey);
            // if found, return cached value
            if (CacheValue != null)
            {
                context.Result = new ContentResult()
                {
                    Content = CacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                };
                return;
            }

            // if not found, proceed to action and cache the result
            var executedContext = await next.Invoke();
            if (executedContext.Result is OkObjectResult result) {
                await _cacheService.SetAsync(cacheKey, result.Value, TimeSpan.FromSeconds(durationInSeconds));
            }

        }
        private string CreateCacheKey(HttpRequest request) 
        {
            StringBuilder key = new StringBuilder();
            key.Append(request.Path+"?");
            foreach(var item  in request.Query.OrderBy(q=>q.Key))
            {
                key.Append($"{item.Key}={item.Value}&");
            }
            return key.ToString();
        }
    }
}
