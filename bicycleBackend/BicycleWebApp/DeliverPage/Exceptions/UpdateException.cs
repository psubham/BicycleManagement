﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverPage.Exceptions
{
    public class UpdateException : ApplicationException
    {
        public UpdateException(string msg) : base(msg)
        {

        }
    }
    
}
