﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleApp.UserException
{
    public class BicyclesException:ApplicationException
    {
        public BicyclesException(string msg):base(msg)
        {

        }
    }
}
