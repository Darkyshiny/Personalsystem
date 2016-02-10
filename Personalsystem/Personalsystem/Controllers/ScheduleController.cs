using Personalsystem.DataAccessLayer;
using Personalsystem.Models;
using Personalsystem.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Personalsystem.Controllers
{
    public class ScheduleController : Controller
    {
        private PersonalSystemContext db = new PersonalSystemContext();
        private EventVM vm = new EventVM { week = 0, year = 0, eventList = new List<Event>() };
        Event repo = new Event();

        [Authorize]
        public ActionResult Index(int? setWeek)
        {
            UserManager<ApplicationUser> UM = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var uID = User.Identity.GetUserId();
            var user = UM.FindById(uID);

            var currentWeek = repo.GetIso8601WeekOfYear(DateTime.Now);
            

            if (vm.week == 0)
            {
                vm.week = currentWeek;
            }

            if (setWeek != null)
            {
                vm.week = setWeek;
            }

            ViewBag.Menu = vm.week;

            var userCompany = db.company.Find(user.cId);

            var companyEvents = db.companyEvent.Where(c => c.cId == userCompany.Id);

            foreach (var item in companyEvents)
            {
                if (repo.GetIso8601WeekOfYear(item.Time) == vm.week)
                {
                    vm.eventList.Add(item);
                }

            }

            return View(vm);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create([Bind(Include = "Title,Content,Time")] Event e)
        {
            var user = db.user.Find(User.Identity.GetUserId());
            var company = db.company.First(c => c.Id == user.cId);

            if (ModelState.IsValid)
            {
                e.CreateCompanyEvent(company, e.Title, e.Content, e.Time);
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        public ActionResult _GroupSchedule()
        {
            UserManager<ApplicationUser> UM = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var uID = User.Identity.GetUserId();
            var user = UM.FindById(uID);

            return PartialView(user);
        }
    }
}