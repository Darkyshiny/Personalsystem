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
        private CompanyRepo companyRepo = new CompanyRepo();
        private Repo repo = new Repo();

        // GET: Companies
        public ActionResult Index()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Executive") || User.IsInRole("Employee"))
            {
                ApplicationUser user = repo.FindUserById(User.Identity.GetUserId());
                return RedirectToAction("Details", new { id = user.cId });
            }
            return View(companyRepo.GetAll());
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            List<Department> DepartmentList = companyRepo.GetCompanyDepartments(id.Value);
            List<Group> GroupList = companyRepo.GetCompanyGroups(id.Value);
            ViewBag.GroupList = GroupList;
            ViewBag.DepartmentList = DepartmentList;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = companyRepo.Find(id.Value);
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
                companyRepo.Save(company);
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
            Company company = companyRepo.Find(id.Value);
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
                companyRepo.Edit(company);
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
            Company company = companyRepo.Find(id.Value);
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
            Company company = companyRepo.Find(id);
            companyRepo.Delete(company);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
