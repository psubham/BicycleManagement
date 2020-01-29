using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapPointApi.UserException
{
    public class MapException:ApplicationException
    {
       public MapException(string msg):base(msg)
        {

        }
    }
}
