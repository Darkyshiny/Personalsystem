using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Personalsystem.Models
{
    public class BlogPost : Message
    {
        public string Name { get; set; }
        public int cId { get; set; }
        [ForeignKey("cId")]
        public virtual Company company { get; set; }
    }
}