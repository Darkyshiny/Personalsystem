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

namespace Personalsystem.Controllers
{
    [Authorize(Roles=("Super Admin, Admin"))]
    public class GroupsController : Controller
    {
        private PersonalSystemContext db = new PersonalSystemContext();
        // GET: Groups
        public ActionResult Index()
        {
            var group = db.group.Include(g => g.department);
            return View(group.ToList());
        }

        // GET: Groups/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.group.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            ApplicationUser user = db.user.Find(User.Identity.GetUserId());
            if(!User.IsInRole("Super Admin"))
                ViewBag.dId = new SelectList(db.department.Where(d => d.cId == user.cId), "Id", "Name");
            else
                ViewBag.dId = new SelectList(db.department, "Id", "Name");
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,dId")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.group.Add(group);
                db.SaveChanges();
                int redir = db.department.Find(group.dId).cId;
                return RedirectToAction("Details", "Companies", new { id = redir });
            }
            ApplicationUser user = db.user.Find(User.Identity.GetUserId());
            if (!User.IsInRole("Super Admin"))
                ViewBag.dId = new SelectList(db.department.Where(d => d.cId == user.cId), "Id", "Name", group.dId);
            else
                ViewBag.dId = new SelectList(db.department, "Id", "Name", group.dId);
            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.group.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            ApplicationUser user = db.user.Find(User.Identity.GetUserId());
            if (!User.IsInRole("Super Admin"))
                ViewBag.dId = new SelectList(db.department.Where(d => d.cId == user.cId), "Id", "Name", group.dId);
            else
                ViewBag.dId = new SelectList(db.department, "Id", "Name", group.dId);
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,dId")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                int redir = db.department.Find(group.dId).cId;
                return RedirectToAction("Details", "Companies", new { id = redir });
            }
            ApplicationUser user = db.user.Find(User.Identity.GetUserId());
            if (!User.IsInRole("Super Admin"))
                ViewBag.dId = new SelectList(db.department.Where(d => d.cId == user.cId), "Id", "Name", group.dId);
            else
                ViewBag.dId = new SelectList(db.department, "Id", "Name", group.dId);
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.group.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.group.Find(id);
            db.group.Remove(group);
            db.SaveChanges();
            int redir = db.department.Find(group.dId).cId;
            return RedirectToAction("Details", "Companies", new { id = redir });
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
