﻿using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RamQuest.Security.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
        public string EnterpriseId { get; set; }
    }
}

