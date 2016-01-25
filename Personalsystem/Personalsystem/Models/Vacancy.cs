using Personalsystem.DataAccessLayer;
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
        public int Cid { get; set; }
        [ForeignKey("Cid")]
        public virtual Company Company { get; set; }

        private PersonalSystemContext db = new PersonalSystemContext();

        public List<Vacancy> ListVacancies(Company company)
        {
            var vacancies = db.vacancy.ToList();
            var result = vacancies.Where(v => v.Cid == company.Id).ToList();
            if (result.Count > 0)
            {
                return result.OrderBy(v => v.Id).ToList();
            }
            else
            {
                throw new Exception();
            }
        }

        public List<Vacancy> ListVacancies(Company company, string search)
        {
            var vacancies = db.vacancy.ToList();
            var result = vacancies.Where(v => v.Cid == company.Id).ToList();
            var searchString = result.Where(r => r.Description.Contains(search)).ToString();

            if (result.Count > 0 & searchString.Contains(search))
            {
                return result.OrderBy(v => v.Id).ToList();
            }
            else
            {
                throw new Exception();
            }
        }
    }
}