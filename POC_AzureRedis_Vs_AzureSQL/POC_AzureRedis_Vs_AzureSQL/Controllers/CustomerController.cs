using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POC_AzureRedis_Vs_AzureSQL.Managers;
using POC_AzureRedis_Vs_AzureSQL.Models;
using POC_AzureRedis_Vs_AzureSQL.ViewModels;

namespace POC_AzureRedis_Vs_AzureSQL.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AzureSqlManager azureSqlManager;
        private readonly AzureRedisManager azureRedisManager;
        public CustomerController()
        {
            SettingsManager.InitializeConfiguration();
            azureSqlManager = new AzureSqlManager();
            azureRedisManager = new AzureRedisManager();
        }

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customers
        public async Task<ActionResult> List()
        {
            List<Customer> list = await azureSqlManager.GetCustomers();
            return View(list);
        }

        // GET: Customer/Details/5
        public ActionResult Details(string id, bool fromRedis)
        {
            var watch = new System.Diagnostics.Stopwatch();
            Customer customer;
            if (fromRedis)
            {
                watch.Start();
                customer = azureRedisManager.GetCustomerById(id);
                watch.Stop();
            }
            else
            {
                watch.Start();
                customer = azureSqlManager.GetCustomerById(id);
                watch.Stop();
            }
            CustomerViewModel customerViewModel = new CustomerViewModel
            {
                ElapsedMilliseconds = watch.ElapsedMilliseconds,
                Id = customer.Id,
                Address = customer.Address,
                Email = customer.Email,
                Name = customer.Name,
                Phone = customer.Phone
            };
            return PartialView("_Details", customerViewModel);
        }
    }
}