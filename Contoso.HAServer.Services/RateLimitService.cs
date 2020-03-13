﻿using Contoso.HAServer.Common;
using Contoso.HAServer.Common.Interfaces;
using Contoso.HAServer.Core.Interfaces;
using Contoso.HAServer.RateLimit.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Contoso.HAServer.RateLimitService
{
    class RateLimitService : IRateLimitService
    {
        private readonly ILogger<RateLimitService> _logger;
        private readonly IRateLimitCore _rateLimitCore;
        private readonly IOptions<RateLimitOptions> _options;

        public RateLimitService(ILogger<RateLimitService> logger, IOptions<RateLimitOptions> options, IRateLimitCore rateLimitCore)
        {
            _logger = logger;
            _rateLimitCore = rateLimitCore;
            _options = options;
        }
        public bool IsRateLimitReach(string clientId,DateTime requestTime)
        {   
            var rateLimitCounter = _rateLimitCore.HandleClient(clientId,requestTime);
            if (rateLimitCounter.TotalRequests > _options.Value.MaxConnectionPerUser)
            {
                _logger.LogWarning($"clientId: {clientId} has reach the limit");
                return true;
            }
            return false;
        }
    }
}
