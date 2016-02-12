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
    public class GroupRepo : IRepository<Group>
    {
        private PersonalSystemContext db = new PersonalSystemContext();
        public IEnumerable<Group> GetAll()
        {
            return db.group.Include("department").ToList();
        }

        public Group Find(int id)
        {
            return db.group.Find(id);
        }

        public void Save(Group Entity)
        {
            db.group.Add(Entity);
            db.SaveChanges();
        }

        public void Edit(Group Entity)
        {
            db.Entry(Entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Group Entity)
        {
            db.group.Remove(Entity);
            db.SaveChanges();
        }
        
        public int Redir(Group Entity)
        {
            return db.department.Find(Entity.dId).cId;
        }

        public SelectList GetGroupSelectList()
        {
            return new SelectList(db.group, "Id", "Name");
        }
    }
}