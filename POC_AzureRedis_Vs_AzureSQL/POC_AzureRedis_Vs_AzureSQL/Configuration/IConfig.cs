using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC_AzureRedis_Vs_AzureSQL.Configuration
{
    public interface IConfig
    {
        string RedisConnection { get; set; }
        string AzureSQLConnection { get; set; }
    }
}
