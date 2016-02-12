using Personalsystem.DataAccessLayer;
using Personalsystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Personalsystem.Repositories
{
    public class VacancyRepo
    {
        private PersonalSystemContext db = new PersonalSystemContext();
        public IEnumerable<Vacancy> GetAll()
        {
            throw new NotImplementedException();
        }

        public Vacancy Find(int id)
        {
            return db.vacancy.Find(id);
        }

        public void Save(Vacancy Entity)
        {

        }

        public void Edit(Vacancy Entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Vacancy Entity)
        {
            throw new NotImplementedException();
        }

        public List<Vacancy> ListVacancies(Company Entity)
        {
            var vacancies = db.vacancy.ToList();
            var result = vacancies.Where(v => v.cId == Entity.Id && v.Active).ToList();
            return result.OrderBy(v => v.Id).ToList();
        }

        public List<Vacancy> ListVacancies(Company Entity, string search)
        {
            var vacancies = db.vacancy.ToList();
            var result = vacancies.Where(v => v.cId == Entity.Id).ToList();
            var searchString = result.Where(r => r.Description.Contains(search)).ToString();
            return result.OrderBy(v => v.Id).ToList();
        }
    }
}