using MyLogistics.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace MyLogistics.Application.Services
{
    public class TenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string HeaderName = "X-Tenant-Id";

        public TenantProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetTenantId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext is null)
            {
                throw new InvalidOperationException("HTTP context is not available.");
            }

            // 1. First priority: read from Claims (if JWT Authentication is existed)
            var tenantClaim = httpContext.User?.FindFirst("tenant_id")?.Value
                           ?? httpContext.User?.FindFirst("TenantId")?.Value;

            if (!string.IsNullOrWhiteSpace(tenantClaim))
            {
                return tenantClaim;
            }

            // 2. Second prioriry: Read from HTTP Header
            if (httpContext.Request.Headers.TryGetValue(HeaderName, out var headerValue) && !string.IsNullOrWhiteSpace(headerValue))
            {
                return headerValue.ToString();
            }

            // 3. if none of them existed
            throw new InvalidOperationException($"The required HTTP Header '{HeaderName}' or Tenant Claim is missing.");
        }
    }
}
