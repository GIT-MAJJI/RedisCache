using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using POC_AzureRedis_Vs_AzureSQL.Configuration;
using Serilog;

namespace POC_AzureRedis_Vs_AzureSQL.Managers
{
    public static class SettingsManager
    {
        public static IConfig Config { get; private set; }
        public static void InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            Config = configuration.GetSection("ConnectionStrings").Get<Config>();

            string outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} | {Level:u3} | T{ThreadId} | {SourceContext} | {RateRequestId} | {Message:l}{NewLine}{Exception}{NewLine}";
            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Information()
                            .WriteTo.File("C:\\logs\\POC_AzureRedis_Vs_AzureSQL.log", outputTemplate: outputTemplate, shared: true)
                            .CreateLogger();
        }
    }
}
