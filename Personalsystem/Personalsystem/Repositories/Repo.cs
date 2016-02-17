using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Personalsystem.DataAccessLayer;
using Personalsystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Personalsystem.Repositories
{
    public class Repo
    {
        private PersonalSystemContext db = new PersonalSystemContext();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new PersonalSystemContext()));
        static Random rnd = new Random();

        public void GetRandom(int first, int last)
        {
            last++;
            rnd.Next(first, last);
        }

        public void SetUserRoleToSuperAdmin(string userid)
        {
            userManager.AddToRole(userid, "Super Admin");
        }

        public void SetUserRoleToExecutive(string userid)
        {
            userManager.AddToRole(userid, "Executive");
        }

        public ApplicationUser FindUserByName(string name)
        {
            return userManager.FindByName(name);
        }

        public ApplicationUser FindUserById(string id)
        {
            return userManager.FindById(id);
        }
        //public List<ApplicationUser> FindPersonalsBySearchDepId(string uname)
        
        //{
        //    var query = db.user.Where(u => u.UserName == uname);
        //    return query.ToList();
        //}

        internal void SetUserRoleToAdmin(string userid, int companyid)
        {
            userManager.AddToRole(userid, "Admin");
            db.user.Find(userid).cId = companyid;
            db.SaveChanges();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
        public void Edit(string userid, string path)
        {
            var user = db.user.Find(userid);
            user.CVurl = path;
            db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();
        }
//Begin, Written by Ali 
        //public List<ApplicationUser> FindPersonalsBySearchUserName(string USERNAME) 
        //{
        //    var query = db.user.Where(u => u.UserName  == USERNAME);
        //    return query.ToList();
        //}

        //public List<ApplicationUser> FindPersonalsBySearchId(string USERID)
        //{
        //    var query = db.user.Where(u => u.Id == USERID);
        //    return query.ToList();
        //}

        //public List<ApplicationUser> FindUserHandleEmploymentById(string usrid)       
        //{
        //    var query = db.user.Where(u => u.Id == usrid);
        //    return query.ToList();
        //}
//End
    }
}
