using Microsoft.AspNet.Identity.EntityFramework;
using Personalsystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Personalsystem.DataAccessLayer
{
    public class PersonalSystemContext : DbContext
    {
        public PersonalSystemContext() : base("DefaultConnection") { }

        public DbSet<Company> company { get; set; }
        public DbSet<Department> department { get; set; }
        public DbSet<Group> group { get; set; }
        public DbSet<Vacancy> vacancy { get; set; }
        public DbSet<ApplicationUser> user { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            base.OnModelCreating(modelBuilder);
        }
    }
}