﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhiyun.Utilities.Exceptions
{
    public class UnreadableException : Exception
    {
        public UnreadableException() { }
        public UnreadableException(string message) : base(message) { }
    }
}