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
using Microsoft.AspNet.Identity.EntityFramework;
using Personalsystem.Repositories;

namespace Personalsystem.Controllers
{
    public class CompaniesController : Controller
    {
        private Repo repo = new Repo();
        private PersonalSystemContext db = new PersonalSystemContext();

        // GET: Companies
        public ActionResult Index()
        {
            ApplicationUser user = db.user.Find(User.Identity.GetUserId());
            if (User.IsInRole("Admin") || User.IsInRole("Executive") || User.IsInRole("Employee"))
                return RedirectToAction("Details", new { id = user.cId });

            return View(db.company.ToList());
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            List<Department> DepartmentList = db.department.Where(r => r.cId == id).ToList();
            List<Group> GroupList = db.group.Where(r => r.department.cId == id).ToList();
            ViewBag.GroupList = GroupList;
            ViewBag.DepartmentList = DepartmentList;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.company.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create 
        [Authorize(Roles = ("Super Admin"))]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Super Admin"))]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.company.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company);
        }

        // GET: Companies/Edit/5
        [Authorize(Roles = ("Super Admin, Admin"))]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.company.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Super Admin, Admin"))]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // GET: Companies/Delete/5
        [Authorize(Roles = ("Super Admin"))]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.company.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.company.Find(id);

            db.company.Remove(company);
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
