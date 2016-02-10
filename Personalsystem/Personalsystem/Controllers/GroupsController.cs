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
using Personalsystem.Repositories;

namespace Personalsystem.Controllers
{
    [Authorize(Roles=("Super Admin, Admin"))]
    public class GroupsController : Controller
    {
        private PersonalSystemContext db = new PersonalSystemContext();
        private GroupRepo groupRepo = new GroupRepo();
        private Repo repo = new Repo();
        // GET: Groups
        public ActionResult Index()
        {
            return View(groupRepo.GetAll());
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
        public ActionResult Create(int? cId)
        {
            string userid = User.Identity.GetUserId();
            ApplicationUser user = repo.FindUserById(userid);
            ViewBag.dId = new SelectList(db.department.Where(d => d.cId == cId), "Id", "Name");
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,dId")] Group group, int? cId)
        {
            if (ModelState.IsValid)
            {
                groupRepo.Save(group);
                int redir = groupRepo.Redir(group);
                return RedirectToAction("Details", "Companies", new { id = redir });
            }
            string userid = User.Identity.GetUserId();
            ApplicationUser user = repo.FindUserById(userid);
            ViewBag.dId = new SelectList(db.department.Where(d => d.cId == cId), "Id", "Name");
            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id, int? cId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = groupRepo.Find(id.Value);
            if (group == null)
            {
                return HttpNotFound();
            }
            string userid = User.Identity.GetUserId();
            ApplicationUser user = repo.FindUserById(userid);
            ViewBag.dId = new SelectList(db.department.Where(d => d.cId == group.department.cId), "Id", "Name");
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,dId")] Group group, int? cId)
        {
            if (ModelState.IsValid)
            {
                groupRepo.Edit(group);
                int redir = groupRepo.Redir(group);
                return RedirectToAction("Details", "Companies", new { id = redir });
            }
            string userid = User.Identity.GetUserId();
            ApplicationUser user = repo.FindUserById(userid);
            ViewBag.dId = new SelectList(db.department.Where(d => d.cId == group.department.cId), "Id", "Name");
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = groupRepo.Find(id.Value);
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
            Group group = groupRepo.Find(id);
            groupRepo.Delete(group);
            int redir = groupRepo.Redir(group);
            return RedirectToAction("Details", "Companies", new { id = redir });
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
