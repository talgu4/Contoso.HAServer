using Contoso.HAServer.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.HAServer.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterCores(this IServiceCollection services)
        {
            return services.AddSingleton<IRateLimitCore, RateLimitCore>();
        }
    }
}
