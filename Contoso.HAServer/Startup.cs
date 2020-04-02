using Contoso.HAServer.Common;
using Contoso.HAServer.Core;
using Contoso.HAServer.InMemory;
using Contoso.HAServer.InMemoryRedis;
using Contoso.HAServer.Middleware;
using Contoso.HAServer.RateLimitService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Contoso.HAServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            if (Configuration.GetSection("UseRedisCache").Get<bool>())
            {
                services.RegisterMemoryRedisCache(Configuration);
            }
            else
            {
                services.AddSingleton<IMemoryCache, MemoryCache>()
                        .RegisterMemoryCacheRateLimitCounter();
            }
            services.RegisterCores()
                    .RegisterServices()
                    .Configure<MyOptions>(Configuration.GetSection("MyOptions"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //register middle wares
            app.UseMiddlewares();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                 {
                     context.Response.StatusCode = StatusCodes.Status200OK;
                     await context.Response.WriteAsync("OK");
                 });
            });
        }
    }
}
