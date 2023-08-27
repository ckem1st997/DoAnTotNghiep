using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Core.Filters
{
    /// <summary>
    ///     Ensures that the request is not cached by the browser
    /// </summary>
    public class DisableBrowserCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);

            HttpResponse httpResponse = context.HttpContext.Response;

            if (httpResponse.StatusCode != 200)
            {
                return;
            }

            httpResponse.GetTypedHeaders().CacheControl =
                new CacheControlHeaderValue { NoCache = true, MaxAge = TimeSpan.Zero, MustRevalidate = true, NoStore = true };

            httpResponse.Headers[HeaderNames.LastModified] = DateTime.Now.ToString("R"); // Format RFC1123
            httpResponse.Headers[HeaderNames.Pragma] = "no-cache";
            httpResponse.Headers[HeaderNames.Expires] = new DateTime(1990, 1, 1, 0, 0, 0).ToString("R");
        }
    }
}
