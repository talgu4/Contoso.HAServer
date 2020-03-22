using Contoso.HAServer.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Contoso.HAServer.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterCores(this IServiceCollection services)
        {
            return services.AddScoped<IRateLimitCore, RateLimitCore>();
        }
    }
}
