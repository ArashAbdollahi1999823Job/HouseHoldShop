﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contracts.Account
{
    public class ChangePasswordModel
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }

    }
}
