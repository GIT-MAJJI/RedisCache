using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC_AzureRedis_Vs_AzureSQL.Configuration
{
    public class Config : IConfig
    {
        public string RedisConnection { get; set; }
        public string AzureSQLConnection { get; set; }
    }
}
