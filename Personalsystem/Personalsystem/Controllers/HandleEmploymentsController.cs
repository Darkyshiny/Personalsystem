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
using Personalsystem.Repositories;

namespace Personalsystem.Controllers
{
    public class HandleEmploymentsController : Controller
    {
        //private PersonalSystemContext db = new PersonalSystemContext();
        private Repo repo = new Repo(); 

        // GET: HandleEmploymentrs
        public ActionResult Index()
        {
            //var user = db.user.Include(a => a.company).Include(a => a.group);
            return View();
        }



        // GET: HandleEmploymentrs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = repo.FindUserById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }       

        ////////////////////////////////////////////////////////////////Begin, writen by Ali
        [HttpPost]
        public ActionResult Find(string search)
        {
            if (search != null)
            {
                search = search.Trim();
                var re = repo.FindUserById(search);
                return View("Index", re);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        ////////////////////////////////////////////////////////////////end

        //// GET: HandleEmploymentrs/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ApplicationUser applicationUser = repo.FindUserById(id);
        //    if (applicationUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.cId = new SelectList(db.company, "Id", "Name", applicationUser.cId);
        //    ViewBag.gId = new SelectList(db.group, "Id", "Name", applicationUser.gId);
        //    return View(applicationUser);
        //}

        //// POST: HandleEmploymentrs/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Surname,Salary,CVurl,start,end,cId,gId,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(applicationUser).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.cId = new SelectList(db.company, "Id", "Name", applicationUser.cId);
        //    ViewBag.gId = new SelectList(db.group, "Id", "Name", applicationUser.gId);
        //    return View(applicationUser);
        //}

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
