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
        
        public void SetUserRole()
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>());
        }
    }
}