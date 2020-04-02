using Contoso.HAServer.Common;
using Contoso.HAServer.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Contoso.HAServer.InMemoryRedis
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterMemoryRedisCache(this IServiceCollection services,
            IConfiguration config)
        {
            return services.AddScoped<IMemoryCacheRateLimitCounter, RedisMemoryCacheRateLimitCounter>()
                           .AddDistributedRedisCache(options =>
            {
                options.Configuration = config.GetConnectionString("RedisConnectionString");
                options.InstanceName = "RedisMaster";
            });
        }
    }
}
