using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Personalsystem.Models
{
    public class PrivateMessage : Message
    {
        public string uId { get; set; }
        [ForeignKey("uId")]
        public virtual ApplicationUser user { get; set; }

    }
}