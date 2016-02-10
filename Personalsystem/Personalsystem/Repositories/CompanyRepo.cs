using Personalsystem.DataAccessLayer;
using Personalsystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Personalsystem.Repositories
{
    public class CompanyRepo : IRepository<Company>
    {
        private PersonalSystemContext db = new PersonalSystemContext();
        public IEnumerable<Company> GetAll()
        {
            return db.company.ToList();
        }

        public Company Find(int id)
        {
            return db.company.Find(id);
        }

        public void Save(Company Entity)
        {
            db.company.Add(Entity);
            db.SaveChanges();
        }

        public void Edit(Company Entity)
        {
            db.Entry(Entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Company Entity)
        {
            db.company.Remove(Entity);
            db.SaveChanges();
        }

        public List<Department> GetCompanyDepartments(int id)
        {
            return db.department.Where(r => r.cId == id).ToList();
        }

        public List<Group> GetCompanyGroups(int id)
        {
            return db.group.Where(r => r.department.cId == id).ToList();
        }
    }
}