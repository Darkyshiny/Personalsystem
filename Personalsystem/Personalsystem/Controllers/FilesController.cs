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
using System.Web.Hosting;
using Microsoft.AspNet.Identity;
using System.Dynamic;
using System.ComponentModel;
using System.IO;
using System.Web.Configuration;
using System.Net.Mail;

namespace Personalsystem.Controllers
{
    public class FilesController : Controller
    {
        private PersonalSystemContext db = new PersonalSystemContext();

        // GET: Files
        public ActionResult Index(int? id)
        {
            if (id != null)
            {
                TempData["AppId"] = id;
                ViewBag.Description = db.vacancy.Find(id).Description.ToString();
            }
            return View();
        }

        //GET: Vacancies for Company
        public ActionResult ShowApplicants()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            ApplicationUser user = db.user.Find(User.Identity.GetUserId());
            if (!User.IsInRole("Executive"))
                return RedirectToAction("Login", "Account");

            int compId = Convert.ToInt32(user.cId);

            var emp = (from a in db.application
                                    join b in db.vacancy on a.vId equals b.Id where b.cId == compId
                                    join c in db.user on a.uId equals c.Id
                                    select new{c.Name, c.Surname, c.Email, a.CoverLetter, a.Id, c.CVurl});

            //Creates a temporary Class
            List<ExpandoObject> Appl = new List<ExpandoObject>();

            foreach (var item in emp)
            {
                IDictionary<string, object> itemExpando = new ExpandoObject();
                foreach (PropertyDescriptor property
                         in
                         TypeDescriptor.GetProperties(item.GetType()))
                {
                    itemExpando.Add(property.Name, property.GetValue(item));
                }
                Appl.Add(itemExpando as ExpandoObject);
            }
            ViewBag.appl = Appl;
            return View();
        }

        // GET: Files
        public ActionResult CoverLetter(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application applicationUser = db.application.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
            
        }
        // GET: Files
        public ActionResult DownloadCV(string url)
        {

            byte[] filedata = System.IO.File.ReadAllBytes(url);
            string contentType = MimeMapping.GetMimeMapping(url);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = url,
                Inline = true,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);
        }
       

        // GET: Files/Create
        public ActionResult Create()
        {
            ViewBag.cId = new SelectList(db.company, "Id", "Name");
            ViewBag.gId = new SelectList(db.group, "Id", "Name");
            
            return View();
        }
        // POST: File/Create
        [HttpPost]
        public ActionResult UploadCV(string letter, HttpPostedFileBase upload)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            if (letter.Length == 0 || upload.FileName == null)
            {
                TempData["Error"] = "To save application both Cover Letter and CV must be given";
                return RedirectToAction("Account", "Login" );
            }

            string folderName = HostingEnvironment.ApplicationPhysicalPath;
            string appId = User.Identity.GetUserId();
            string pathString = System.IO.Path.Combine(folderName, "Users", appId);
            System.IO.Directory.CreateDirectory(pathString);
            string fileName = upload.FileName;
          
            pathString = System.IO.Path.Combine(pathString, fileName);
            if (System.IO.File.Exists(pathString))
            {
                System.IO.File.Delete(pathString);
            }
            upload.SaveAs(pathString);

            // Update Application and User
            Application app = new Application();
  
            var user = db.user.Find(User.Identity.GetUserId());
            user.CVurl = pathString;
            //db.SaveChanges();

            app.uId = user.Id;
            app.CoverLetter = letter;
            int test;
            int.TryParse(TempData["AppId"].ToString(), out test);
            app.vId = test;

            db.application.Add(app);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Files/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname,Salary,CVurl,cId,gId,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.user.Add(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cId = new SelectList(db.company, "Id", "Name", applicationUser.cId);
            ViewBag.gId = new SelectList(db.group, "Id", "Name", applicationUser.gId);
            return View(applicationUser);
        }

        // GET: Files/Edit/5
        public ActionResult Edit(string id)
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
            ViewBag.cId = new SelectList(db.company, "Id", "Name", applicationUser.cId);
            ViewBag.gId = new SelectList(db.group, "Id", "Name", applicationUser.gId);
            return View(applicationUser);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,Salary,CVurl,cId,gId,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cId = new SelectList(db.company, "Id", "Name", applicationUser.cId);
            ViewBag.gId = new SelectList(db.group, "Id", "Name", applicationUser.gId);
            return View(applicationUser);
        }

        // GET: Files/Delete/5
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

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.user.Find(id);
            db.user.Remove(applicationUser);
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
