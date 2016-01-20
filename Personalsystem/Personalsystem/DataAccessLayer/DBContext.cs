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
    }
}