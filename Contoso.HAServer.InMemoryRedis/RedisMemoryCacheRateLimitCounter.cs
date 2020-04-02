using Contoso.HAServer.Common;
using Contoso.HAServer.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;

namespace Contoso.HAServer.InMemoryRedis
{
    public class RedisMemoryCacheRateLimitCounter : IMemoryCacheRateLimitCounter
    {
        private readonly ILogger<RedisMemoryCacheRateLimitCounter> _logger;
        private readonly IOptions<MyOptions> _options;
        private readonly IDistributedCache _memoryCache;
        private readonly object _locker = new object();
        DistributedCacheEntryOptions _distributedCacheEntryOptions;

        public RedisMemoryCacheRateLimitCounter(ILogger<RedisMemoryCacheRateLimitCounter> logger, IDistributedCache memoryCache,
            IOptions<MyOptions> options)
        {
            _logger = logger;
            _options = options;
            _memoryCache = memoryCache;
            _distributedCacheEntryOptions = new DistributedCacheEntryOptions().SetAbsoluteExpiration(_options.Value.ExpirationTime);
        }

        public RateLimitCounter GetOrCreate(string clientId)
        {
            try
            {
                var strResults = _memoryCache.GetString(clientId);
                if (strResults == null)
                {
                    var rateLimitCounter = new RateLimitCounter();
                    _memoryCache.SetString(clientId, JsonConvert.SerializeObject(rateLimitCounter), _distributedCacheEntryOptions);
                    return rateLimitCounter;
                }
                else
                {
                    var ratelimit = JsonConvert.DeserializeObject<RateLimitCounter>(strResults);
                    ratelimit.Increment();

                    return ratelimit;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception when Set ClientId: {clientId}. Exception: {ex}");
                return null;
            }
        }
    }
}
