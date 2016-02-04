using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Personalsystem.DataAccessLayer;
using Personalsystem.Models;
using Microsoft.AspNet.Identity;
using Personalsystem.Models.VM;

namespace Personalsystem.Controllers
{
    public class EventsController : Controller
    {
        private PersonalSystemContext db = new PersonalSystemContext();
        private EventVM vm = new EventVM();
        Event repo = new Event();

        // GET: Events
        [Authorize]
        public ActionResult Index(int? week)
        {
            if (week != null)
            {
                vm.week += week;
            }

            var userId = User.Identity.GetUserId();
            var user = db.user.Find(userId);
            var userCompany = db.company.First(c => c.Id == user.cId);
            var currentYear = DateTime.Now.Year;
            var currentWeek = repo.GetIso8601WeekOfYear(DateTime.Now) + vm.week;
          


            //if (currentWeek == 53)
            //{
            //    currentYear + 1;
            //    currentWeek = 1;
            //}
            //if (currentWeek == 0)
            //{
            //    currentYear - 1;
            //    currentWeek = 52;
            //}

            var companyEvents = vm.events().Where(c => c.cId == userCompany.Id);

            foreach (var item in companyEvents)
            {
                if (repo.GetIso8601WeekOfYear(item.Time) == currentWeek)
                {
                    vm.eventList.Add(item);
                }

            }

            return View(vm);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.cId = new SelectList(db.company, "Id", "Name");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,time,cId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.companyEvent.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cId = new SelectList(db.company, "Id", "Name", @event.cId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.companyEvent.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.cId = new SelectList(db.company, "Id", "Name", @event.cId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,time,cId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cId = new SelectList(db.company, "Id", "Name", @event.cId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.companyEvent.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.companyEvent.Find(id);
            db.companyEvent.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
