using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Personalsystem.Models
{
    public class Group
    {
        public int Id { get; set; }
        public List<ApplicationUser> Employees { get; set; }
    }
}