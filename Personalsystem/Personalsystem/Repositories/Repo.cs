using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Personalsystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Personalsystem.Repositories
{
    public class Repo
    {
        static Random rnd = new Random();

        public void GetRandom(int first, int last)
        {
            last++;
            rnd.Next(first, last);
        }

        public void SetUserRole()
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>());
        }

    }
}
