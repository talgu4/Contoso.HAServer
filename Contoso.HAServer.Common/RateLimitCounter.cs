using System;
using System.Threading;

namespace Contoso.HAServer.Common
{
    public class RateLimitCounter
    {
        public DateTime Timestamp;

        public long TotalRequests;

        public RateLimitCounter() : this(DateTime.Now, 0)
        {

        }

        public RateLimitCounter(DateTime timestamp, long totalRequests)
        {
            Timestamp = timestamp;
            TotalRequests = totalRequests;
        }

        public long Increment()
        {
            return Interlocked.Increment(ref TotalRequests);
        }
    }
}
