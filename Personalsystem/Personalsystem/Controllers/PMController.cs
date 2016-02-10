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
using Microsoft.AspNet.Identity.EntityFramework;
using Personalsystem.Repositories;

namespace Personalsystem.Controllers
{
    public class PMController : Controller
    {
        private PMRepo pmRepo = new PMRepo();
        private Repo repo = new Repo();

        public ActionResult Index() { return RedirectToAction("Inbox"); }
        // GET: PM
        public ActionResult Inbox()
        {
            string userid = User.Identity.GetUserId();
            var message = pmRepo.GetReceivedMessages(userid);
            return View(message);
        }

        public ActionResult Sent()
        {
            string userid = User.Identity.GetUserId();
            var message = pmRepo.GetSentMessages(userid);
            return View(message);
        }

        // GET: PM/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrivateMessage privateMessage = pmRepo.FindPM(id.Value);
            if (privateMessage == null)
            {
                return HttpNotFound();
            }
            return View(privateMessage);
        }

        // GET: PM/Create
        public ActionResult Compose()
        {
            //ViewBag.userList = db.user.ToList();
            return View();
        }

        // POST: PM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Compose([Bind(Include = "PM,UserName")] PrivateMessageVM privateMessageVM)
        {
            if (ModelState.IsValid)
            {
                privateMessageVM = pmRepo.SetPMProperties(privateMessageVM, User.Identity.GetUserId());
                privateMessageVM.PM.Timestamp = DateTime.Now;
                pmRepo.SaveNewMessageToDatabase(privateMessageVM);
                return RedirectToAction("Index");
            }
            return View(privateMessageVM);
        }





        // GET: PM/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrivateMessage privateMessage = pmRepo.FindPM(id);
            if (privateMessage == null)
            {
                return HttpNotFound();
            }
            return View(privateMessage);
        }

        // POST: PM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrivateMessage privateMessage = pmRepo.FindPM(id);
            pmRepo.DeletePM(privateMessage);
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
