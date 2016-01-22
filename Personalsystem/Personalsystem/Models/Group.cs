﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Personalsystem.Models
{
    public class Group
    {
        public Group()
        {
            List<ApplicationUser> Employees = new List<ApplicationUser>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ApplicationUser> Employees { get; set; }
    }
}