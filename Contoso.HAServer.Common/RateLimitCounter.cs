using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.HAServer.Common
{
    public class RateLimitCounter
    {
        public RateLimitCounter(DateTime timestamp, long totalRequests)
        {
            Timestamp = timestamp;
            TotalRequests = totalRequests;
        }

        public DateTime Timestamp { get; set; }

        public long TotalRequests { get; set; }
    }
}
