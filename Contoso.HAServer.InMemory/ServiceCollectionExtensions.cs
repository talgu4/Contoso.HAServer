using Contoso.HAServer.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Contoso.HAServer.InMemory
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterMemoryCacheRateLimitCounter(this IServiceCollection services)
        {
            return services.AddScoped<IMemoryCacheRateLimitCounter, MemoryCacheRateLimitCounter>();

        }
    }
}
