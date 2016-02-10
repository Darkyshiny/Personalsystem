using Personalsystem.DataAccessLayer;
using Personalsystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Personalsystem.Repositories
{
    public class BlogPostRepo : IRepository<BlogPost>
    {
        PersonalSystemContext db = new PersonalSystemContext();
        public IEnumerable<BlogPost> GetAll()
        {
            return db.post.ToList();
        }

        public BlogPost Find(int id)
        {
            return db.post.Find(id);
        }

        public void Save(BlogPost Entity)
        {
            db.post.Add(Entity);
            db.SaveChanges();
        }

        public void Edit(BlogPost Entity)
        {
            db.Entry(Entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(BlogPost Entity)
        {
            db.post.Remove(Entity);
            db.SaveChanges();
        }
    }
}