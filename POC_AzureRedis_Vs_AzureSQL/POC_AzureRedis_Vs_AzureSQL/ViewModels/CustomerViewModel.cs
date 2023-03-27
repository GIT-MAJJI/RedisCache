﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC_AzureRedis_Vs_AzureSQL.ViewModels
{
    public class CustomerViewModel
    {
        public long ElapsedMilliseconds { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}