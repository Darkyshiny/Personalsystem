using Personalsystem.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Personalsystem.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }

        public int cId { get; set; }
        [ForeignKey("cId")]
        public virtual Company company { get; set; }

    }
}
