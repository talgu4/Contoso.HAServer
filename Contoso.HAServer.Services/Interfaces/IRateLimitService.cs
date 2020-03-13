using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.HAServer.RateLimit.Interfaces
{
    public interface IRateLimitService
    {
        bool IsRateLimitReach(string clientId,DateTime requestTime);
    }
}
