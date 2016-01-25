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
    using System.Web.Security;
    using Personalsystem.Repositories;

    internal sealed class Configuration : DbMigrationsConfiguration<Personalsystem.DataAccessLayer.PersonalSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
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

            // Seed some companies (as well as substructures of company)

            context.company.AddOrUpdate(

                new Company
                {
                    Id = 0,
                    Name = "Company 1",
                    Description = "Getting crunk Incorporated",
                    Posts = new List<BlogPost>
                    {
                        new BlogPost 
                        {
                            Id = 0,
                            Content = "Lorem ipsum 1",
                            Timestamp = DateTime.Now
                        },
                        new BlogPost 
                        {
                            Id = 1, 
                            Content = "Lorem ipsum 2",
                            Timestamp = DateTime.Now
                        },
                        new BlogPost 
                        {
                            Id = 2, 
                            Content = "Lorem ipsum 3",
                            Timestamp = DateTime.Now
                        }
                    },
                    Vacancies = new List<Vacancy>
                    {
                        new Vacancy 
                        {
                            Id = 0,
                            Did = 1,
                            Cid = 0,
                            Description = "You need 10 years of experience and magical powers to get this job"
                        },
                        new Vacancy 
                        {
                            Id = 1, 
                            Did = 1,
                            Cid = 0,
                            Description = "You need 10 years of experience and magical powers to get this job"
                        },
                        new Vacancy 
                        {
                            Id = 2, 
                            Did = 2,
                            Cid = 0,
                            Description = "You need 10 years of experience and magical powers to get this job"
                        },
                        new Vacancy 
                        {
                            Id = 3,
                            Did = 2,
                            Cid = 0,
                            Description = "You need 10 years of experience and magical powers to get this job"
                        }
                    },
                    Applications = new List<Application>(),

                    Departments = new List<Department>
                    {
                        new Department { 
                            Id = 0,
                            Name = "Department 1", 
                            Description = "R&D", 
                            Groups = new List<Group>{
                                new Group
                                {
                                    Id = 0,
                                    Name = "Group 1",
                                    Description = "R&D Executives",
                                    Employees = new List<ApplicationUser>()

                                },
                                new Group
                                {
                                    Id = 1, 
                                    Name = "Group 2",
                                    Description = "R&D Internal systems",
                                    Employees = new List<ApplicationUser>()

                                },
                                new Group
                                {
                                    Id = 2,
                                    Name = "Group 3",
                                    Description = "R&D Facility management",
                                    Employees = new List<ApplicationUser>()
                                },
                                new Group
                                {
                                    Id = 3,
                                    Name = "Group 4",
                                    Description = "R&D Research staff",
                                    Employees = new List<ApplicationUser>()
                                }
                            }
                        },
                        new Department { 
                            Id = 1,
                            Name = "Department 2", 
                            Description = "Human relations", 
                            Groups = new List<Group>{
                                new Group
                                {
                                    Id = 4,
                                    Name = "Group 1",
                                    Description = "HR Executives",
                                    Employees = new List<ApplicationUser>()

                                },
                                new Group
                                {
                                    Id = 5,
                                    Name = "Group 2",
                                    Description = "HR Internal systems",
                                    Employees = new List<ApplicationUser>()

                                },
                                new Group
                                {
                                    Id = 6,
                                    Name = "Group 3",
                                    Description = "HR Facility management",
                                    Employees = new List<ApplicationUser>()
                                },
                                new Group
                                {
                                    Id = 7,
                                    Name = "Group 4",
                                    Description = "HR staff",
                                    Employees = new List<ApplicationUser>()
                                }
                            }
                        },
                        new Department { 
                            Id = 2,
                            Name = "Department 3", 
                            Description = "Production", 
                            Groups = new List<Group>{
                                new Group
                                {
                                    Id = 8,
                                    Name = "Group 1",
                                    Description = "Production Internal systems",
                                    Employees = new List<ApplicationUser>()

                                },
                                new Group
                                {
                                    Id = 9,
                                    Name = "Group 2",
                                    Description = "Production Facility management",
                                    Employees = new List<ApplicationUser>()
                                },
                                new Group
                                {
                                    Id = 10,
                                    Name = "Group 3",
                                    Description = "Production Employees",
                                    Employees = new List<ApplicationUser>()
                                },
                                new Group
                                {
                                    Id = 11,
                                    Name = "Group 3",
                                    Description = "Production Employees",
                                    Employees = new List<ApplicationUser>()
                                }
                            }
                        },
                    }
                },
                new Company
                {
                    Id = 1,
                    Name = "Company 1",
                    Description = "Another company",
                    Posts = new List<BlogPost>
                    {
                        new BlogPost 
                        {
                            Id = 3,
                            Content = "Lorem ipsum 1",
                            Timestamp = DateTime.Now
                        },
                        new BlogPost 
                        {
                            Id = 4, 
                            Content = "Lorem ipsum 2",
                            Timestamp = DateTime.Now
                        },
                        new BlogPost 
                        {
                            Id = 5,
                            Content = "Lorem ipsum 3",
                            Timestamp = DateTime.Now
                        }
                    },
                    Vacancies = new List<Vacancy>
                    {
                        new Vacancy 
                        {
                            Id = 4,
                            Did = 3,
                            Cid = 1,
                            Description = "You need 10 years of experience and magical powers to get this job"
                        },
                        new Vacancy 
                        {
                            Id = 5,
                            Did = 3,
                            Cid = 1,
                            Description = "You need 10 years of experience and magical powers to get this job"
                        },
                        new Vacancy 
                        {
                            Id = 6,
                            Did = 4,
                            Cid = 1,
                            Description = "You need 10 years of experience and magical powers to get this job"
                        },
                        new Vacancy 
                        {
                            Id = 7,
                            Did = 4,
                            Cid = 1,
                            Description = "You need 10 years of experience and magical powers to get this job"
                        }
                    },
                    Applications = new List<Application>(),

                    Departments = new List<Department>
                    {
                        new Department { 
                            Id = 3,
                            Name = "Department 1", 
                            Description = "R&D", 
                            Groups = new List<Group>{
                                new Group
                                {
                                    Id = 12,
                                    Name = "Group 1",
                                    Description = "R&D Executives",
                                    Employees = new List<ApplicationUser>()

                                },
                                new Group
                                {
                                    Id = 13,
                                    Name = "Group 2",
                                    Description = "R&D Internal systems",
                                    Employees = new List<ApplicationUser>()

                                },
                                new Group
                                {
                                    Id = 14,
                                    Name = "Group 3",
                                    Description = "R&D Facility management",
                                    Employees = new List<ApplicationUser>()
                                },
                                new Group
                                {
                                    Id = 15,
                                    Name = "Group 4",
                                    Description = "R&D Research staff",
                                    Employees = new List<ApplicationUser>()
                                }
                            }
                        },
                        new Department {
                            Id = 4,
                            Name = "Department 2", 
                            Description = "Human relations", 
                            Groups = new List<Group>{
                                new Group
                                {
                                    Id = 16,
                                    Name = "Group 1",
                                    Description = "HR Executives",
                                    Employees = new List<ApplicationUser>()

                                },
                                new Group
                                {
                                    Id = 17,
                                    Name = "Group 2",
                                    Description = "HR Internal systems",
                                    Employees = new List<ApplicationUser>()

                                },
                                new Group
                                {
                                    Id = 18,
                                    Name = "Group 3",
                                    Description = "HR Facility management",
                                    Employees = new List<ApplicationUser>()
                                },
                                new Group
                                {
                                    Id = 19,
                                    Name = "Group 4",
                                    Description = "HR staff",
                                    Employees = new List<ApplicationUser>()
                                }
                            }
                        },
                        new Department { 
                            Id = 5,
                            Name = "Department 3", 
                            Description = "Production", 
                            Groups = new List<Group>{
                                new Group
                                {
                                    Id = 20,
                                    Name = "Group 1",
                                    Description = "Production Internal systems",
                                    Employees = new List<ApplicationUser>()

                                },
                                new Group
                                {
                                    Id = 21,
                                    Name = "Group 2",
                                    Description = "Production Facility management",
                                    Employees = new List<ApplicationUser>()
                                },
                                new Group
                                {
                                    Id = 22,
                                    Name = "Group 3",
                                    Description = "Production Employees",
                                    Employees = new List<ApplicationUser>()
                                },
                                new Group
                                {
                                    Id = 23,
                                    Name = "Group 4",
                                    Description = "Production Employees",
                                    Employees = new List<ApplicationUser>()
                                }
                            }
                        },
                    }
                }
            );
            context.SaveChanges();

            // Create password hasher (required to get a proper hash into PasswordHash property @ ApplicationUser

            var hasher = new PasswordHasher();

            // Create roles (through some Microsoft Identity magic)

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            
            RoleManager.Create(new IdentityRole("SuperAdmin"));
            RoleManager.Create(new IdentityRole("Admin"));
            RoleManager.Create(new IdentityRole("Executive"));
            RoleManager.Create(new IdentityRole("Employee"));
            RoleManager.Create(new IdentityRole("JobSearch"));

            
            /* 
             * 
             * 
             * GER UPP PÅ ATT SEEDA USERS SKAPA DOM MANUELLT!!!
             * 
             * 
            */

            //// Seed a superadmin
            //if (!context.user.Any(u => u.UserName == "SuperAdmin"))
            //{
            //    string password = hasher.HashPassword("admin");
            //    var useradmin = new ApplicationUser { UserName = "SuperAdmin", Name = "Admeen", Surname = "Admeenian", Email = "admin@personalsystem.com", PasswordHash = password};
            //    UserManager.Create(useradmin);
            //    //context.group.Find(1).Employees.Add(useradmin);
            //    //context.SaveChanges();
            //    UserManager.AddToRole(useradmin.Id, "SuperAdmin");
            //}
            //// Seed some users into a temporary list

            //var tempGroup = new List<ApplicationUser>();
            



            //context.SaveChanges();

            //// Add users from temporary list to userlist and random group
            //if (!context.user.Any(u => u.UserName == "User1"))
            //{
            //    Random rng = new Random();
            //    for (int i = 0; i < 100; i++ )
            //    {
            //        string password = hasher.HashPassword("user" + i);
            //        var user = new ApplicationUser { UserName = "User" + i, Name = "Usain", Surname = "Userian", Email = "user" + i + "@gmail.com", PasswordHash = password };
            //        UserManager.Create(user);
            //        int r = rng.Next(1, context.group.Count());
            //        //context.group.Find(3).Employees.Add(user);
            //        //context.SaveChanges();
            //        UserManager.AddToRole(user.Id, "Employee");
            //    }
            //    foreach (ApplicationUser user in tempGroup)
            //    {
            //    }
            //    context.SaveChanges();
            //}
            
        }
    }
}