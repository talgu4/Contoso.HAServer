using Contoso.HAServer.Common;
using Contoso.HAServer.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Contoso.HAServer.RateLimitService.Interfaces;

namespace Contoso.HAServer.RateLimitService
{
    class RateLimitService : IRateLimitService
    {
        private static readonly object ProcessLocker = new object();
        private readonly ILogger<RateLimitService> _logger;
        private readonly IRateLimitCore _rateLimitCore;
        private readonly IOptions<MyOptions> _options;

        public RateLimitService(ILogger<RateLimitService> logger, IOptions<MyOptions> options, IRateLimitCore rateLimitCore)
        {
            _logger = logger;
            _rateLimitCore = rateLimitCore;
            _options = options;
        }

        public bool IsRateLimitReach(string clientId)
        {
            try
            {
                if (_rateLimitCore.HandleClient(clientId) > _options.Value.MaxConnectionPerUser)
                {
                    _logger.LogWarning($"clientId: {clientId} has reach the limit");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return false;
        }
    }
}
