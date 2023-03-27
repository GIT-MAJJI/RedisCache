using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POC_AzureRedis_Vs_AzureSQL.Managers;

namespace POC_AzureRedis_Vs_AzureSQL.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AzureSqlManager azureSqlManager;
        private readonly AzureRedisManager azureRedisManager;

        public CustomerController()
        {
            SettingsManager.InitializeConfiguration();
            azureSqlManager = new AzureSqlManager();
            azureRedisManager = new AzureRedisManager();
        }


        [HttpGet, Route("sql/{id}")]
        public IActionResult GetFromSql(string id)
        {
            var customer = azureSqlManager.GetCustomerById(id);
            if (customer != null)
                return Ok(customer);
            else
                return StatusCode(500);
        }

        [HttpGet, Route("redis/{id}")]
        public IActionResult GetFromRedis(string id)
        {
            var customer = azureRedisManager.GetCustomerById(id);
            if (customer != null)
                return Ok(customer);
            else
                return StatusCode(500);
        }


    }
}
