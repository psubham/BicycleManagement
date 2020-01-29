using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleWebApp.UserException
{
    public class LoginExceptionPassword:ApplicationException
    {
        public LoginExceptionPassword(string message):base(message)
        {

        }
    }
}
