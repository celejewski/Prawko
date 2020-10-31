using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Prawko.Blazor.Middleware
{
    public class SetGuidMiddleware
    {
        private readonly RequestDelegate _next;

        public SetGuidMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            const string key = "GUID";
            if( !httpContext.Request.Cookies.ContainsKey(key) )
            {
                var guid = Guid.NewGuid().ToString();
                httpContext.Response.Cookies.Append(key, guid);
                httpContext.Items[key] = guid;
            }
            else
            {
                httpContext.Items[key] = httpContext.Request.Cookies[key];
            }

            await _next(httpContext);
        }
    }
}
