namespace Personalsystem.Migrations
{
    using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Personalsystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Personalsystem.DataAccessLayer.PersonalSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Personalsystem.DataAccessLayer.PersonalSystemContext context)
        {

            if (!context.company.Any(c => c.Name == "Company 1"))
            {
                context.company.AddOrUpdate(

                new Company
                {
                    Name = "Company 1",
                    Description = "Getting crunk Incorporated"
                },

                new Company
                {
                    Name = "Test Company",
                    Description = "Company made for testing."
                }
                );
                context.SaveChanges();
            }

            if (!context.post.Any(p => p.Content == "Lorem ipsum 1"))
            {
                context.post.AddOrUpdate(

                            new BlogPost
                            {
                                Id = 0,
                                Content = "Lorem ipsum 1",
                                Timestamp = DateTime.Now,
                                cId = 1,
                                postedBy = "user1@gmail.com",
                                publicPost = true,
                                Title = "Lorem1"
                            },

                            new BlogPost
                            {
                                Id = 1,
                                Content = "Lorem ipsum 2",
                                Timestamp = DateTime.Now,
                                cId = 1,
                                postedBy = "user2@gmail.com",
                                publicPost = false,
                                Title = "Lorem2"
                            },

                            new BlogPost
                            {
                                Id = 2,
                                Content = "Lorem ipsum 3",
                                Timestamp = DateTime.Now,
                                cId = 1,
                                postedBy = "user1@gmail.com",
                                publicPost = false,
                                Title = "Lorem3"
                            }

                );
                context.SaveChanges();
            }

            if (!context.vacancy.Any(v => v.Description == ".net Developer"))
            {
                context.vacancy.AddOrUpdate(

                        new Vacancy
                        {
                            Id = 0,
                            cId = 1,
                            Description = ".net Developer"
                        },

                        new Vacancy
                        {
                            Id = 1,
                            cId = 1,
                            Description = "Scrum master"
                        },

                        new Vacancy
                        {
                            Id = 2,
                            cId = 1,
                            Description = "Coffee barista"
                        },

                        new Vacancy
                        {
                            Id = 3,
                            cId = 1,
                            Description = "Janitor"
                        }

            );
                context.SaveChanges();
            }


            if (!context.department.Any(d => d.Name == "Department 1"))
            {
                context.department.AddOrUpdate(

                        new Department
                        {
                            Name = "Department 1",
                            Description = "R&D",
                            cId = 1
                        },
                        new Department
                        {
                            Name = "Test Department",
                            Description = "Department made for testing",
                            cId = 2
                        }
                        );

                context.SaveChanges();
            }

            if (!context.group.Any(g => g.Name == "Group 1"))
            {
                context.group.AddOrUpdate(

                                new Group
                                {
                                    Id = 0,
                                    Name = "Group 1",
                                    Description = "R&D Executives",
                                    dId = 1

                                },
                                new Group
                                {
                                    Id = 1,
                                    Name = "Group 2",
                                    Description = "R&D Internal systems",
                                    dId = 1


                                },
                                new Group
                                {
                                    Id = 2,
                                    Name = "Group 3",
                                    Description = "R&D Facility management",
                                    dId = 1

                                },
                                new Group
                                {
                                    Id = 3,
                                    Name = "Group 4",
                                    Description = "R&D Research staff",
                                    dId = 1

                                },
                                new Group
                                {
                                    Name = "Test Group",
                                    Description = "Group made for testing",
                                    dId = 2
                                }


                );
                context.SaveChanges();
            }
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!RoleManager.RoleExists("Super Admin"))
            {
                RoleManager.Create(new IdentityRole("Super Admin"));
                RoleManager.Create(new IdentityRole("Admin"));
                RoleManager.Create(new IdentityRole("Executive"));
                RoleManager.Create(new IdentityRole("Employee"));
                RoleManager.Create(new IdentityRole("Job Searcher"));       
            }
            var admin = new ApplicationUser { UserName = "admin@personalsystem.com", Name = "Admeen", Surname = "Admeenian", Email = "admin@personalsystem.com", gId = 1 };

            if (!context.user.Any(u => u.UserName == "admin@personalsystem.com"))
            {
                
                var result = UserManager.Create(admin, "admin1");
                if (result.Succeeded)
                {
                    UserManager.AddToRole(admin.Id, "Super Admin");
                }

            }

            var tempGroup = new List<ApplicationUser>();

              for (int i = 0; i < 100; i++)
              {
                  tempGroup.Add(new ApplicationUser { UserName = "user" + i + "@gmail.com", Name = "Usain", Surname = "Userian", Email = "user" + i + "@gmail.com" });
              }

            if (!context.user.Any(u => u.Name == "user1@gmail.com"))
              foreach (var user in tempGroup)
              {
                  var result = UserManager.Create(user, "password");
                  if (result.Succeeded)
                  {
                      UserManager.AddToRole(user.Id, "Employee");
                  }
              }

            if (context.user.Any(u => u.gId == null) & context.group.Any())
            {

                Random rng = new Random();
                var groupCount = context.group.Count();
                foreach (ApplicationUser user in context.user.Where(u => u.UserName != "admin@personalsystem.com"))
                {
                    int target = rng.Next(1, groupCount + 1);
                    user.gId = target;
                }
                context.SaveChanges();
            }

            if (!context.companyEvent.Any(e => e.Id == 1))
            {
                Random rng = new Random();
                for (var i = 0; i < 40; i++)
                {
                    context.companyEvent.AddOrUpdate(new Event
                    {
                        
                        cId = 1,
                        Time = DateTime.Now.AddDays(rng.Next(-1, 6)),
                        Title = "Hamburger party " + i,
                        Content =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                    });
                }

                context.SaveChanges();
            }


            if (!context.user.Any(u => u.start.ToString() == "00:00:00"))
            {
                var company = context.company.Find(1);
                List<Group> groups = context.group.Where(g => g.Id != null).ToList();
                foreach (Group g in groups)
                {
                    company.SetWorkTimeToGroup(g, 9, 17);
                }
                context.SaveChanges();

            }
            if (context.user.Any(u => u.cId == null & context.company.Any()))
            {
                List<ApplicationUser> test = context.user.Where(u => u.UserName != "admin@personalsystem.com").ToList();
                foreach (ApplicationUser user in test)
                {
                    ApplicationUser currentuser = context.user.Find(user.Id);
                    int dId = context.group.Find(user.gId.Value).dId;
                    int companyid = context.department.Find(dId).cId;
                    currentuser.cId = companyid;
                }
                context.SaveChanges();
            }
        }
    }
}
