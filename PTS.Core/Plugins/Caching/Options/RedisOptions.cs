using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Core.Plugins.Caching.Options
{
    public class RedisOptions
    {
        public bool Enabled { get; set; }
        public string InstanceName { get; set; }
        public string ConnectionString { get; set; }
        public int AbsoluteExpiration { get; set; } = 10;
    }
}
