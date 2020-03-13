using Contoso.HAServer.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.HAServer.Core.Interfaces
{
    public interface IRateLimitCore
    {
        RateLimitCounter HandleClient(string clientId, DateTime requestTime);
    }
}
