using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.HAServer.Common.Interfaces
{
    public interface IMemoryCacheRateLimitCounter
    {
        bool Exists(string clientId);
        void Set(string clientId, RateLimitCounter counter);
        RateLimitCounter Get(string clientId);
    }
}
