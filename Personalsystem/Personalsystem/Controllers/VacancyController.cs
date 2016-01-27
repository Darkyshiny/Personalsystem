using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Personalsystem.Models;
using Personalsystem.DataAccessLayer;

namespace Personalsystem.Controllers
{
    public class VacancyController : Controller
    {
        private PersonalSystemContext db = new PersonalSystemContext();
        private Vacancy repo = new Vacancy();

        // GET: Vacancy
        public ActionResult Index()
        {
            var companyId = db.company.Find(1);
            var vacancy = repo.ListVacancies(companyId);
            return View(vacancy);
        }
    }
}