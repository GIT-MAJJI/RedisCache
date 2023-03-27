using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using POC_AzureRedis_Vs_AzureSQL.Models;
using StackExchange.Redis;

namespace POC_AzureRedis_Vs_AzureSQL.Managers
{
    public class AzureRedisManager
    {
        private readonly IDatabase cacheDB;
        public AzureRedisManager()
        {
            cacheDB = Connection.GetDatabase();
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(SettingsManager.Config.RedisConnection);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        public void AddCustomersToAzureRedis(List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                AddCustomer(customer);
            }
        }

        public void AddCustomer(Customer customer)
        {
            cacheDB.StringSet(customer.Id, JsonConvert.SerializeObject(customer));
        }

        public Customer GetCustomerById(string id)
        {
            var customer = cacheDB.StringGet(id);
            if (customer.IsNull)
                return null;
            else
                return JsonConvert.DeserializeObject<Customer>(customer);
        }
    }
}
