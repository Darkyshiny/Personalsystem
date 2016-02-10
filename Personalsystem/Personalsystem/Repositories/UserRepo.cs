using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Personalsystem.DataAccessLayer;
using Personalsystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Personalsystem.Repositories
{
    public class UserRepo
    {
        private PersonalSystemContext db = new PersonalSystemContext();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new PersonalSystemContext()));

        public IEnumerable<ApplicationUser> GetAll()
        {
            return db.user.ToList();
        }

        //public ApplicationUser Find(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Save(ApplicationUser Entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Edit(ApplicationUser Entity)
        //{
        //    throw new NotImplementedException();
        //}

        public void Delete(ApplicationUser Entity)
        {
            db.user.Remove(Entity);
            db.SaveChanges();
        }
    }
}