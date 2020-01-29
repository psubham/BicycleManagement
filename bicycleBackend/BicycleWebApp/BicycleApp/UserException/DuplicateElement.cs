using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleApp.UserException
{
    public class DuplicateElement:ApplicationException
    {
        public DuplicateElement(string message):base(message)
        {

        }
    }
}
