using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Contoso.HAServer.RateLimitService.Interfaces;

namespace Contoso.HAServer.Middleware.Middlewares
{
    internal class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private IRateLimitService _rateLimitService;

        public RateLimitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IRateLimitService service)
        {
            _rateLimitService = service;
            var clientId = context.Request.Query["clientId"];
            if (string.IsNullOrWhiteSpace(clientId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else
            {
                if (_rateLimitService.IsRateLimitReach(clientId))
                {
                    context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    await _next(context);
                }
            }
        }
    }
}
