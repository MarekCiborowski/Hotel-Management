﻿using DomainObjects.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels.Admin
{
    public class AccountListVM
    {
        public IEnumerable<UserDto> AccountList { get; set; }
       
    }
}