﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.ApplicationException
{
 
        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message)
            {
            }
        }

    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
        }
    }



}
