using Microsoft.AspNetCore.Builder;
using System;
using Contoso.HAServer.Middleware.Middlewares;

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
