using Contoso.HAServer.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.HAServer.Common
{
    public class RateLimitOptions
    {
        public int MaxConnectionPerUser { get; set; }
        public TimeSpan ExpirationTime { get; set; }        
       
    }
}
