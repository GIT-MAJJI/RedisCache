using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POC_AzureRedis_Vs_AzureSQL.Models;

namespace POC_AzureRedis_Vs_AzureSQL.Data
{
    public static class DataBuilder
    {
        public static List<Customer> BuildCustomers(int start, int count)
        {
            var customers = new List<Customer>();
            int j = 1000 + start;
            for (int i = j; i < j + count; i++)
            {
                var customer = new Customer
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = $"Cust-{i}",
                    Address = $"Address-{i}",
                    Email = $"email.{i}@fake.dat",
                    Phone = $"425-444-{i}"
                };
                customers.Add(customer);
            }
            return customers;
        }
    }
}
