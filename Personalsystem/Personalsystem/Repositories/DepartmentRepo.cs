using Personalsystem.DataAccessLayer;
using Personalsystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Personalsystem.Repositories
{
    public class DepartmentRepo : IRepository<Department>
    {
        private PersonalSystemContext db = new PersonalSystemContext(); 
        public IEnumerable<Department> GetAll()
        {
            return db.department.OrderBy(r => r.cId).ToList();
        }

        public Department Find(int id)
        {
            return db.department.Find(id);
        }

        public void Save(Department Entity)
        {
            db.department.Add(Entity);
            db.SaveChanges();
        }

        public void Edit(Department Entity)
        {
            db.Entry(Entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Department Entity)
        {
            db.department.Remove(Entity);
            db.SaveChanges();
        }
        public List<Department> DepartmentList(int cId)
        {
            return db.department.Where(r => r.cId == cId).ToList();
        }

        public SelectList GetDepartmentSelectList(int cId)
        {
            return new SelectList(db.department.Where(d => d.cId == cId), "Id", "Name");
        }
    }
}