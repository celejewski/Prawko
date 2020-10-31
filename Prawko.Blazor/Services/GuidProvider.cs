using Microsoft.AspNetCore.Http;

namespace Prawko.Blazor.Services
{
    public class GuidProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string _key = "GUID";

        public GuidProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetGuid()
        {
            return (string) _httpContextAccessor.HttpContext.Items[_key];
        }

        public void SetGuid(string guid)
        {
            _httpContextAccessor.HttpContext.Items[_key] = guid;
        }
    }
}
