using Contoso.HAServer.Common;
using Contoso.HAServer.Core;
using Contoso.HAServer.InMemory;
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
            services.AddOptions()
                    .AddSingleton<IMemoryCache,MemoryCache>()
                    .RegisterCores()
                    .RegisterServices()
                    .RegisterMemoryCacheRateLimitCounter();

            services.Configure<RateLimitOptions>(Configuration.GetSection("RateLimitOptions"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //register middlewares
            app.UseMiddlewares();
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/",async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    await context.Response.WriteAsync("OK");
                });
            });
        }
    }
}
