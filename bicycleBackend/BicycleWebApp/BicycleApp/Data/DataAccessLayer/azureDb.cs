using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleApp.Data.DataAccessLayer
{
    public class azureDb
    {
        public string accesKey { get; set; }
        IConfiguration configuration;
        public azureDb(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.accesKey = configuration.GetConnectionString("AccessKey");
        }
    }
}
