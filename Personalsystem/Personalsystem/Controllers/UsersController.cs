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
    public class UsersController : Controller
    {
        private PersonalSystemContext db = new PersonalSystemContext();
        private Repo repo = new Repo();
       

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.user.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }


//Begin, written by Ali ********************************************************
        public ActionResult Index()
        {
            var user = db.user;
            return View(user.ToList());
        }

        [HttpPost]
        public ActionResult FindUserByUserName(string search)
        {
            if (search != null)
            {
                //var re = repo.FindPersonalsBySearchId(search);
                search = search.Trim();
                var re = repo.FindPersonalsBySearchUserName(search);
                return View("Index", re.ToList());
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult FindUserByUserId(string search)
        {
            search = search.Trim();
            if (search != null)
            {
                var re = repo.FindPersonalsBySearchId(search);

                //var re = repo.FindPersonalsBySearchUserName(search);
                return View("Index", re.ToList());
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: ApplicationUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.user.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }
        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.user.Find(id);
            db.user.Remove(applicationUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    
//********************************************************************

        public JsonResult GetUsers()
        {
            var result = db.user.ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
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
