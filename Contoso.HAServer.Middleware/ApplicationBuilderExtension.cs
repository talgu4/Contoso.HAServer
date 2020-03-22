using Contoso.HAServer.Middleware.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Contoso.HAServer.Middleware
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder services)
        {
            return services.UseMiddleware<RateLimitMiddleware>();
        }
    }
}
