using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Personalsystem.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Did { get; set; }
        [ForeignKey("Did")]
        public virtual Department Department { get; set; }
    }
}