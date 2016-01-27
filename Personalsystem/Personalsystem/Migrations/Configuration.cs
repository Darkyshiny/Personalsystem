namespace Personalsystem.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Personalsystem.Models;
    using System;
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
            context.company.AddOrUpdate(new Company { Name = "Test Company", Description = "Testing Company" });
            context.SaveChanges();
            if(!context.post.Find(1).Name.Equals("Test Post"))
                context.post.AddOrUpdate(new BlogPost { Name="Test Post", Content="Test Content", Timestamp= DateTime.Now, cId = 1 });
            context.SaveChanges();
            
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            roleManager.Create(new IdentityRole("Super Admin"));
            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("Executive"));
            roleManager.Create(new IdentityRole("Employee"));
            roleManager.Create(new IdentityRole("Job Searcher"));
            
        }
    }
}
