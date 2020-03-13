using Contoso.HAServer.Common;
using Contoso.HAServer.Common.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;

namespace Contoso.HAServer.InMemory
{
    public class MemoryCacheRateLimitCounter : IMemoryCacheRateLimitCounter
    {
        private readonly ILogger<MemoryCacheRateLimitCounter> _logger;
        private readonly IRateLimitOptions _options;
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheRateLimitCounter(ILogger<MemoryCacheRateLimitCounter> logger ,IMemoryCache memoryCache,IRateLimitOptions options)
        {
            _logger = logger;
            _options = options;
            _memoryCache = memoryCache;
        }

        public bool Exists(string clientId)
        {
            return _memoryCache.TryGetValue(clientId, out _);
        }

        public RateLimitCounter Get(string clientId)
        {
            if (_memoryCache.TryGetValue(clientId, out object value))
            {
                return value as RateLimitCounter;
            }
            return null;
        }

        public void Set(string clientId, RateLimitCounter counter)
        {
            try
            {
                _memoryCache.Set(clientId, counter,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(_options.ExpirationTime));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception when Set ClientId: {clientId}. Exception: {ex.ToString()}");
            }
        }
    }
}
