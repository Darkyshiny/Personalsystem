using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Personalsystem.Models
{
    public class Application
    {
        public int Id { get; set; }

        [Required]
        public string Uid { get; set; }
        [ForeignKey("Uid")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int Vid { get; set; }
        [ForeignKey("Vid")]
        public virtual Vacancy Vacancy { get; set; }

        public string CoverLetter { get; set; }

    }
}