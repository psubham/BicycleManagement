using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleWebApp.Data.Models
{
    public class LoginResponse
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string  token { get; set; }
    }
}
