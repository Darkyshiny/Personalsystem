﻿using System;
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
        public int Uid { get; set; }
        [ForeignKey("Uid")]
        public virtual ApplicationUser User { get; set; }
        [Required]
        public int Vid { get; set; }
        [ForeignKey("Vid")]
        public virtual Vacancy Vacancy { get; set; }

    }
}