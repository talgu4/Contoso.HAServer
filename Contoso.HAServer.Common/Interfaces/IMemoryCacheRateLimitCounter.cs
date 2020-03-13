using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.HAServer.Common.Interfaces
{
    public interface IMemoryCacheRateLimitCounter
    {
        RateLimitCounter GetOrCreate(string clientId);
    }
}
