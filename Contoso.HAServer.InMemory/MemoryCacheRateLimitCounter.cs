using Contoso.HAServer.Common;
using Contoso.HAServer.Common.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Contoso.HAServer.InMemory
{
    public class MemoryCacheRateLimitCounter : IMemoryCacheRateLimitCounter
    {
        private readonly ILogger<MemoryCacheRateLimitCounter> _logger;
        private readonly IOptions<RateLimitOptions> _options;
        private readonly IMemoryCache _memoryCache;
        private readonly object _locker = new object();

        public MemoryCacheRateLimitCounter(ILogger<MemoryCacheRateLimitCounter> logger, IMemoryCache memoryCache,
            IOptions<RateLimitOptions> options)
        {
            _logger = logger;
            _options = options;
            _memoryCache = memoryCache;
        }

        public RateLimitCounter GetOrCreate(string clientId)
        {
            try
            {
                lock (_locker)
                {
                    return _memoryCache.GetOrCreate(clientId, entry =>
                    {
                        var rateLimitCounter = new RateLimitCounter();
                        entry.SetValue(rateLimitCounter);
                        entry.SetAbsoluteExpiration(_options.Value.ExpirationTime);
                        return rateLimitCounter;
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception when Set ClientId: {clientId}. Exception: {ex.ToString()}");
                return null;
            }
        }
    }
}
