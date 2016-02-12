using Personalsystem.DataAccessLayer;
using Personalsystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Personalsystem.Repositories
{
    public class ApplicationRepo : IRepository<Application>
    {
        private PersonalSystemContext db = new PersonalSystemContext();
        public IEnumerable<Application> GetAll()
        {
            return db.application.ToList();
        }

        public Application Find(int id)
        {
            return db.application.Find(id);
        }

        public void Save(Application Entity)
        {
            db.application.Add(Entity);
            db.SaveChanges();
        }

        public void Edit(Application Entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Application Entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Application> del)
        {
            db.application.RemoveRange(del);
            
        }

    }
}