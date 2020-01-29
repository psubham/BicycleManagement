using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleWebApp.UserException
{
    public class ElementCannotCreated : ApplicationException
    {
        public ElementCannotCreated(string msg) : base(msg)
        {

        }
    }
}
