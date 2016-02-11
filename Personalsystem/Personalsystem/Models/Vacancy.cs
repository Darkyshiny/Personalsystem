﻿using Personalsystem.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Personalsystem.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int cId { get; set; }
        [ForeignKey("cId")]
        public virtual Company Company { get; set; }
        public int? dId { get; set; }
        [ForeignKey("dId")]
        public virtual Department Department { get; set; }
        public bool Active { get; set; }
        
        private PersonalSystemContext db = new PersonalSystemContext();

        public List<Vacancy> ListVacancies(Company company)
        {
            var vacancies = db.vacancy.ToList();
            var result = vacancies.Where(v => v.cId == company.Id && v.Active).ToList();
            return result.OrderBy(v => v.Id).ToList();
        }

        public List<Vacancy> ListVacancies(Company company, string search)
        {
            var vacancies = db.vacancy.ToList();
            var result = vacancies.Where(v => v.cId == company.Id).ToList();
            var searchString = result.Where(r => r.Description.Contains(search)).ToString();
            return result.OrderBy(v => v.Id).ToList();
        }
    }
}
