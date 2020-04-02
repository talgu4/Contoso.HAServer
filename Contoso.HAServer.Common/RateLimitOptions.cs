using System;

namespace Contoso.HAServer.Common
{
    public class MyOptions
    {
        public int MaxConnectionPerUser { get; set; }
        public TimeSpan ExpirationTime { get; set; }

    }
}
