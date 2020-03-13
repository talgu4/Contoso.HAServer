using Contoso.HAServer.Common.Interfaces;
using Contoso.HAServer.Core.Interfaces;
using Contoso.HAServer.RateLimit.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace Contoso.HAServer.RateLimitService
{
    class RateLimitService : IRateLimitService
    {
        private readonly ILogger<RateLimitService> _logger;
        private readonly IRateLimitCore _rateLimitCore;
        private readonly IRateLimitOptions _options;

        public RateLimitService(ILogger<RateLimitService> logger, IRateLimitOptions options, IRateLimitCore rateLimitCore)
        {
            _logger = logger;
            _rateLimitCore = rateLimitCore;
            _options = options;
        }
        public bool IsRateLimitReach(string clientId,DateTime requestTime)
        {
            _logger.LogInformation("Rate limit service - is rate limit reach");
            
            var rateLimitCounter = _rateLimitCore.HandleClient(clientId,requestTime);
            if (rateLimitCounter.TotalRequests > _options.MaxConnectionPerUser)
            {
                return false;
            }


            throw new NotImplementedException();
        }
    }
}
