using Contoso.HAServer.Common;
using Contoso.HAServer.Common.Interfaces;
using Contoso.HAServer.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.HAServer.Core
{
    class RateLimitCore : IRateLimitCore
    {
        private readonly ILogger<RateLimitCore> _logger;
        private readonly IMemoryCacheRateLimitCounter _memoryCache;

        public RateLimitCore(ILogger<RateLimitCore> logger, IMemoryCacheRateLimitCounter memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }
        public RateLimitCounter HandleClient(string clientId, DateTime requestTime)
        {
            try
            {
                RateLimitCounter rateLimitCounter;
                if (_memoryCache.Exists(clientId))
                {
                    rateLimitCounter = _memoryCache.Get(clientId);
                    rateLimitCounter.TotalRequests++;
                }
                else
                {
                    rateLimitCounter = new RateLimitCounter(requestTime, 1);
                    _memoryCache.Set(clientId, rateLimitCounter);
                }
                return rateLimitCounter;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
