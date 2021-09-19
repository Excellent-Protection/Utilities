﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public enum HttpStatusCodeEnum
    {
        Ok=200,
        NotFound = 404,
        Ambiguous = 300,
        IneternalServerError=500
    }
}