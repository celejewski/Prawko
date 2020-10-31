using Microsoft.AspNetCore.Http;
using Prawko.Blazor.Services;
using System;
using System.Threading.Tasks;

namespace Prawko.Blazor.Middlewares
{
    public class SetGuidMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly GuidProvider _guidProvider;

        public SetGuidMiddleware(RequestDelegate next, GuidProvider guidProvider)
        {
            _next = next;
            _guidProvider = guidProvider;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            const string key = "GUID";
            if( httpContext.Request.Cookies.ContainsKey(key) )
            {
                _guidProvider.SetGuid(httpContext.Request.Cookies[key]);
            }
            else
            {
                var guid = Guid.NewGuid().ToString();
                httpContext.Response.Cookies.Append(key, guid);
                _guidProvider.SetGuid(guid);
            }

            await _next(httpContext);
        }
    }
}
