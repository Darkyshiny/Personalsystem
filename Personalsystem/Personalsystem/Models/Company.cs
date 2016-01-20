using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Personalsystem.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Department> Departments { get; set; }
        public List<Vacancy> Vacancies { get; set; }
        public List<Application> Applications { get; set; }
        public List<BlogPost> Posts { get; set; }
    }
}