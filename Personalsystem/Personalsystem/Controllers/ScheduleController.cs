using Personalsystem.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Personalsystem.Controllers
{
    public class ScheduleController : Controller
    {
        private PersonalSystemContext db = new PersonalSystemContext();

        // GET: Events
        public ActionResult Index()
        {
            var result = db.companyEvent.Where(e => e.cId != null).OrderBy(e => e.Time);
            return View(result.ToList());
        }
    }
}