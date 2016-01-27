using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Personalsystem.Models
{
    public class PrivateMessage : Message
    {
        public string Uid { get; set; }
        [ForeignKey("Uid")]
        public virtual ApplicationUser user { get; set; }

    }
}