using Contoso.HAServer.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.HAServer.Common
{
    public class RateLimitOptions : IRateLimitOptions
    {
        public int MaxConnectionPerUser { get; set; }
        public TimeSpan ExpirationTime { get; set; }
        
        public RateLimitOptions(IConfiguration configuration)
        {
            MaxConnectionPerUser = int.Parse(configuration.GetSection("MaxConnectionPerUser").Value);
            ExpirationTime = TimeSpan.Parse(configuration.GetSection("ExpirationTime").Value);
        }
    }
}
