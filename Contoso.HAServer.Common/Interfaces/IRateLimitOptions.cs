using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.HAServer.Common.Interfaces
{
    public interface IRateLimitOptions
    {
        int MaxConnectionPerUser { get; set; }
        TimeSpan ExpirationTime { get; set; }
    }
}
