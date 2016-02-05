using Personalsystem.DataAccessLayer;
using Personalsystem.Models;
using Personalsystem.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

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

            var userId = User.Identity.GetUserId();
            var user = db.user.Find(userId);
            var userCompany = db.company.First(c => c.Id == user.cId);

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
    }
}