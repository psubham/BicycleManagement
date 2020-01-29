using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleWebApp.UserException
{
    public class LoginException: ApplicationException
    {
        public LoginException(string message):base(message)
        {

        }
    }
}
