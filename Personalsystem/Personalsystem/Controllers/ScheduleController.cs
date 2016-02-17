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
using Personalsystem.Repositories;

namespace Personalsystem.Controllers
{
    public class ScheduleController : Controller
    {
        private UserManager<ApplicationUser> UM = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new PersonalSystemContext()));
        private EventVM vm = new EventVM { week = 0, year = 0, eventList = new List<Event>() };
        private ScheduleRepo scheduleRepo = new ScheduleRepo();
        private CompanyRepo companyRepo = new CompanyRepo();
        private Repo repo = new Repo();

        [Authorize]
        public ActionResult Index(int? setWeek)
        {
            
            var uID = User.Identity.GetUserId();
            var user = UM.FindById(uID);

            var currentWeek = scheduleRepo.GetIso8601WeekOfYear(DateTime.Now);
            

            if (vm.week == 0)
            {
                vm.week = currentWeek;
            }

            if (setWeek != null)
            {
                vm.week = setWeek;
            }

            ViewBag.Menu = vm.week;

            var userCompany = companyRepo.Find(user.cId.Value);

            var companyEvents = scheduleRepo.ListCompanyEvents(userCompany);

            foreach (var item in companyEvents)
            {
                if (scheduleRepo.GetIso8601WeekOfYear(item.Time) == vm.week)
                {
                    vm.eventList.Add(item);
                }

            }

            return View(vm);
        }

        [Authorize(Roles = "Admin, Executive")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Executive")]
        [HttpPost]
        public ActionResult Create([Bind(Include = "Title,Content,Time")] Event e)
        {
            var user = repo.FindUserById(User.Identity.GetUserId());
            var company = companyRepo.Find(user.cId.Value);

            if (ModelState.IsValid)
            {
                scheduleRepo.CreateCompanyEvent(company, e.Title, e.Content, e.Time);
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        public ActionResult _GroupSchedule()
        {
            var uID = User.Identity.GetUserId();
            var user = UM.FindById(uID);

            return PartialView(user);
        }
    }
}