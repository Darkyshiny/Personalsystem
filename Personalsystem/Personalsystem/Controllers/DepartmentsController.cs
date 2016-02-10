using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Personalsystem.DataAccessLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Personalsystem.Models;
using Personalsystem.Repositories;

namespace Personalsystem.Controllers
{
    [Authorize(Roles = ("Super Admin, Admin"))]
    public class DepartmentsController : Controller
    {
        private DepartmentRepo departmentRepo = new DepartmentRepo();
        private Repo repo = new Repo();
        // GET: Departments
        public ActionResult Index()
        {
            IEnumerable<Department> departmentList = new List<Department>();
            ApplicationUser user = repo.FindUserById(User.Identity.GetUserId());
            if (!User.IsInRole("Super Admin"))
                departmentList = departmentRepo.DepartmentList(user.cId.Value);
            else
                departmentList = departmentRepo.GetAll();

            return View(departmentList);
        }

        // GET: Departments/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = departmentRepo.Find(id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create(int? cId)
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Department department, int? cId)
        {
            
            if (ModelState.IsValid)
            {
                department.cId = cId.Value;
                departmentRepo.Save(department);
                return RedirectToAction("Details", "Companies", new { id = department.cId });
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = departmentRepo.Find(id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,cId")] Department department)
        {
            if (ModelState.IsValid)
            {
                departmentRepo.Edit(department);
                return RedirectToAction("Details", "Companies", new { id = department.cId });
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = departmentRepo.Find(id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = departmentRepo.Find(id);
            departmentRepo.Delete(department);
            return RedirectToAction("Details", "Companies", new { id = department.cId });
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
