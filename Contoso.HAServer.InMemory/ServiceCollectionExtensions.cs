using Contoso.HAServer.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.HAServer.InMemory
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterMemoryCacheRateLimitCounter(this IServiceCollection services)
        {
            return services.AddSingleton<IMemoryCacheRateLimitCounter, MemoryCacheRateLimitCounter>();
        }
    }
}
