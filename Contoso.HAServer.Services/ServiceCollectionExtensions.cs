using Contoso.HAServer.RateLimit.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.HAServer.RateLimitService
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            return services.AddSingleton<IRateLimitService, RateLimitService>();
        }
    }
}
