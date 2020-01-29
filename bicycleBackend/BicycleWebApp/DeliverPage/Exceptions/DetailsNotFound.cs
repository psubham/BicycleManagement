using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverPage.Exceptions
{
    public class DetailsNotFound:ApplicationException
    {
        public DetailsNotFound(string msg) : base(msg)
        {

        }
    }
}
