using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleWebApp.UserException
{
    public class DuplicateElement : ApplicationException
    {
        public DuplicateElement(string msg) : base(msg)
        {

        }
    }
}
