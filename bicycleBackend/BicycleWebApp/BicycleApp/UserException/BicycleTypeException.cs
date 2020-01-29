using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleApp.UserException
{
    public class BicycleTypeException:ApplicationException
    {
        public BicycleTypeException(string msg):base(msg)
        {

        }
    }
}
