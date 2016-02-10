using Personalsystem.DataAccessLayer;
using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Personalsystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Personalsystem.Repositories
{
    public class RepoPlus
    {
        //Begin, Written by Ali 
        private PersonalSystemContext db = new PersonalSystemContext();
        static Random rnd = new Random();

        public void GetRandom(int first, int last)
        {
            last++;
            rnd.Next(first, last);
        }

        
        public List<ApplicationUser> FindPersonalsBySearchUserName(string USERNAME)
        {
            var query = db.user.Where(u => u.UserName == USERNAME);
            return query.ToList();
        }

        public List<ApplicationUser> FindPersonalsBySearchId(string USERID)
        {
            var query = db.user.Where(u => u.Id == USERID);
            return query.ToList();
        }

        public List<ApplicationUser> FindUserHandleEmploymentById(string usrid)
        {
            var query = db.user.Where(u => u.Id == usrid);
            return query.ToList();
        }

        //End





    }
}

