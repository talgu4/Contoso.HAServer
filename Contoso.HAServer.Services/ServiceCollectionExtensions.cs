using Contoso.HAServer.RateLimitService.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Contoso.HAServer.RateLimitService
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            return services.AddScoped<IRateLimitService, RateLimitService>();
        }
    }
}
