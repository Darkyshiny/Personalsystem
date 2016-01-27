using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int DId { get; set; }
        [ForeignKey("DId")]
        public virtual Department department { get; set; }
    }
}