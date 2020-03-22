using Contoso.HAServer.Common.Interfaces;
using Contoso.HAServer.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;

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
        public long HandleClient(string clientId)
        {
            try
            {
                return _memoryCache.GetOrCreate(clientId).Increment();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
