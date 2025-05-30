﻿using OpenCleanArchitectureAccount.Abstraction.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCleanArchitectureAccount.Demo
{
    public class CustomUser : IUserLogin
    {
        public string SubjectId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
