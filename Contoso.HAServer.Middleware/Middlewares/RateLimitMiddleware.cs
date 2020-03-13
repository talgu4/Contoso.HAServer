using Contoso.HAServer.RateLimit.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.HAServer.Middleware.Middlewares
{
    internal class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRateLimitService _RateLimitService;

        public RateLimitMiddleware(RequestDelegate next, IRateLimitService service)
        {
            _next = next;
            _RateLimitService = service;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var clientId = context.Request.Query["clientId"];
            var requestTime = DateTime.Now;
            if (_RateLimitService.IsRateLimitReach(clientId, requestTime))
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            }
            else
            {
                await _next(context);
            }
        }
    }
}
