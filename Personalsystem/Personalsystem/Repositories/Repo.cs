using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Personalsystem.DataAccessLayer;
using Personalsystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Personalsystem.Repositories
{
    public class Repo
    {
        private PersonalSystemContext db = new PersonalSystemContext();


        public void SetUserRole(string roleName, string userName)
        {
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var user = UserManager.FindByName(userName);
            var rolesForUser = UserManager.GetRoles(user.Id);

            if (!rolesForUser.Contains(roleName))
            {
                UserManager.AddToRole(user.Id, roleName);
            }

        }
    }
}