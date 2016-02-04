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
    public class PMController : Controller
    {
        private PersonalSystemContext db = new PersonalSystemContext();

        public ActionResult Index() { return RedirectToAction("Inbox"); }
        // GET: PM
        public ActionResult Inbox()
        {
            string userid = User.Identity.GetUserId();
            var message = db.message.Include(p => p.Receiver).Include(p => p.Sender).Where(p => p.Receiver.Id == userid).ToList();
            return View(message);
        }

        public ActionResult Sent()
        {
            string userid = User.Identity.GetUserId();
            var message = db.message.Include(p => p.Receiver).Include(p => p.Sender).Where(p => p.Sender.Id == userid).ToList();
            return View(message);
        }
        // GET: PM/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrivateMessage privateMessage = db.message.Find(id);
            if (privateMessage == null)
            {
                return HttpNotFound();
            }
            return View(privateMessage);
        }

        // GET: PM/Create
        public ActionResult Compose()
        {
            // Will implement to search by UserName later
            ViewBag.receiverId = new SelectList(db.user, "Id", "UserName");
            ViewBag.senderId = new SelectList(db.user, "Id", "UserName");
            return View();
        }

        // POST: PM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Content,Timestamp,receiverId")] PrivateMessage privateMessage)
        {

            if (ModelState.IsValid)
            {
                privateMessage.senderId = User.Identity.GetUserId();
                privateMessage.Timestamp = DateTime.Now;
                db.message.Add(privateMessage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.receiverId = new SelectList(db.user, "Id", "UserName", privateMessage.receiverId);
            ViewBag.senderId = new SelectList(db.user, "Id", "UserName", privateMessage.senderId);
            return View(privateMessage);
        }

        //// GET: PM/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PrivateMessage privateMessage = db.message.Find(id);
        //    if (privateMessage == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.receiverId = new SelectList(db.user, "Id", "Name", privateMessage.receiverId);
        //    ViewBag.senderId = new SelectList(db.user, "Id", "Name", privateMessage.senderId);
        //    return View(privateMessage);
        //}

        //// POST: PM/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Content,Timestamp,senderId,receiverId")] PrivateMessage privateMessage)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(privateMessage).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.receiverId = new SelectList(db.user, "Id", "Name", privateMessage.receiverId);
        //    ViewBag.senderId = new SelectList(db.user, "Id", "Name", privateMessage.senderId);
        //    return View(privateMessage);
        //}

        // GET: PM/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrivateMessage privateMessage = db.message.Find(id);
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
            PrivateMessage privateMessage = db.message.Find(id);
            db.message.Remove(privateMessage);
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
