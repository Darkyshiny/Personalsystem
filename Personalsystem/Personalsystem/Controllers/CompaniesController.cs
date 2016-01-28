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
        public ActionResult Index(string redirected)
        {
            ViewBag.Redirected = redirected;
            if (User.Identity.Name == "admin@personalsystem.com" && !User.IsInRole("Super Admin"))
                repo.SetUserRoleToSuperAdmin(User.Identity.GetUserId());
            if (User.Identity.Name == "companyadmin@personalsystem.com" && !User.IsInRole("Admin"))
                repo.SerUserRoleToAdmin(User.Identity.GetUserId(), 1);
            return View(db.company.ToList());
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
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

        // GET: Companies/Create 
        [Authorize(Roles=("Super Admin"))]
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
            ApplicationUser user = db.user.Find(User.Identity.GetUserId());

            if (!User.IsInRole("Admin") || user.cId != id)
                if (!User.IsInRole("Super Admin"))
                    return RedirectToAction("Index", new { redirected = "You are only allowed to edit your own company" });
            
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
