﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TddExample.Application.Model
{
    public class AuthModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }
}
