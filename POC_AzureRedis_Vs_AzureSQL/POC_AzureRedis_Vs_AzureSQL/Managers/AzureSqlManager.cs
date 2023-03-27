using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POC_AzureRedis_Vs_AzureSQL.Models;

namespace POC_AzureRedis_Vs_AzureSQL.Managers
{
    public class AzureSqlManager
    {

        public async Task<List<Customer>> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection connection = new SqlConnection(SettingsManager.Config.AzureSQLConnection))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * FROM Customer");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new Customer
                            {
                                Id = reader.GetString(0),
                                Name = reader.GetString(1),
                                Address = reader.GetString(2),
                                Email = reader.GetString(3),
                                Phone = reader.GetString(4)
                            });
                        }
                    }
                }
            }
            return customers;
        }

        public void AddCustomersToAzureSQL(List<Customer> customers)
        {
            using (SqlConnection connection = new SqlConnection(SettingsManager.Config.AzureSQLConnection))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO dbo.Customer (Id, Name, Address, Email, Phone) VALUES ");

                foreach (var customer in customers)
                {
                    sb.Append($"('{customer.Id}', '{customer.Name}', '{customer.Address}', '{customer.Email}', '{customer.Phone}'),");
                }
                String sql = sb.ToString();
                sql = sql.Substring(0, sql.Length - 1);

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public Customer GetCustomerById(string id)
        {
            Customer customer = new Customer();

            using (SqlConnection connection = new SqlConnection(SettingsManager.Config.AzureSQLConnection))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"SELECT * FROM dbo.Customer WHERE Id = '{id}'");

                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customer.Id = reader.GetString(0);
                            customer.Name = reader.GetString(1);
                            customer.Address = reader.GetString(2);
                            customer.Email = reader.GetString(3);
                            customer.Phone = reader.GetString(4);
                        }
                    }
                }
            }
            return customer;
        }
    }
}
