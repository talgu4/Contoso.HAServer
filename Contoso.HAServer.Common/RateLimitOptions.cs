using System;

namespace Contoso.HAServer.Common
{
    public class RateLimitOptions
    {
        public int MaxConnectionPerUser { get; set; }
        public TimeSpan ExpirationTime { get; set; }

    }
}
