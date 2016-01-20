namespace Personalsystem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using System.Linq;
    using Personalsystem.Models;
    using Personalsystem.DataAccessLayer;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<Personalsystem.DataAccessLayer.PersonalSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Personalsystem.DataAccessLayer.PersonalSystemContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var hasher = new PasswordHasher();
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            string roleName = "SuperAdmins";
            IdentityResult roleResult;

            if (!RoleManager.RoleExists(roleName))
            {
                roleResult = RoleManager.Create(new IdentityRole(roleName));
            }

            context.user.AddOrUpdate(

                new ApplicationUser { UserName = "SuperAdmin", Name = "Admeen", Surname = "Admeenian", Email = "admin@personalsystem.com", PasswordHash = hasher.HashPassword("admin") },
                new ApplicationUser { UserName = "User1", Name = "Usain", Surname = "Userian", Email = "usain@company1.com", PasswordHash = hasher.HashPassword("user1") },
                new ApplicationUser { UserName = "User2", Name = "Michael", Surname = "Persson", Email = "michael@company1.com", PasswordHash = hasher.HashPassword("user2") }

                );
            context.SaveChanges();

            context.company.AddOrUpdate(

                new Company
                {
                    Id = 0,
                    Name = "Company 1",
                    Departments = new List<Department>
                    {
                        new Department { 
                            Id = 0, 
                            Name = "Department 1", 
                            Description = "R&D", 
                            Groups = new List<Group>{
                                new Group{
                                    Id = 0,
                                    
                                }
                            }
                        }
                    }
                }

                );

        }
    }
}
