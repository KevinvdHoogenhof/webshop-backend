﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopBackend.Services
{
    public class HashSalt
    {
        public string Hash { get; set; }
        public byte[] Salt { get; set; }
    }
}