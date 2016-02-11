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
using Personalsystem.Models.VM;

namespace Personalsystem.Controllers
{
    public class TransferController : Controller
    {
        private GroupRepo groupRepo = new GroupRepo();
        private UserRepo userRepo = new UserRepo();
        private Repo repo = new Repo();

        // GET: Transfer
        public ActionResult Index()
        {

            var user = userRepo.GetAll().OrderBy(g => g.gId).ToList();
            var group = groupRepo.GetAll();

            return View(user);
        }
        //begin *****************************************************************************

        [HttpPost]
        public ActionResult Index(string search)
        {
            var result = repo.FindUserById(search);
            return View(result);
        }



        [HttpPost]
        public ActionResult Find(string search)
        {
            var re = repo.FindPersonalsBySearchId(search);
       // var re = repo.FindPersonalsBySearchDepId(search);
        return View("Index",re.ToList());
        
        }
        
        //end *****************************************************************************


        // GET: Transfer/Details/5
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

        //// GET: Transfer/Create
        //public ActionResult Create()
        //{
        //    ViewBag.cId = new SelectList(db.company, "Id", "Name");
        //    ViewBag.gId = new SelectList(db.group, "Id", "Name");
        //    return View();
        //}

        //// POST: Transfer/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,Surname,Salary,CVurl,cId,gId,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.user.Add(applicationUser);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(applicationUser);
        //}

        // GET: Transfer/Edit/5
        public ActionResult Edit(string id)
        {
            TransferVM transferVM = new TransferVM();
            ApplicationUser applicationUser = repo.FindUserById(id);

            transferVM.userID = applicationUser.Id;
            transferVM.groupID = applicationUser.gId;

            ViewBag.groupID = groupRepo.GetGroupSelectList();

            //var target = db.group.Select(g => new SelectListItem
            //{
            //    Value = transferVM.groupID.Equals(g.Id).ToString(),
            //    Text = g.Name,
            //    Selected = transferVM.groupID.Equals(g.Id)


            //});

            //transferVM.dbgroupList = new SelectList(target);


            ViewBag.User = applicationUser.UserName + " - UserId:   " + transferVM.userID + " - groupID:  " + transferVM.groupID;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            return View(transferVM);
        }

        // POST: Transfer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID, groupID")] TransferVM transferVM)
        {
            ViewBag.User = "  UserId:   " + transferVM.userID + " - groupID: " + transferVM.groupID + " - Posted";
            var user = repo.FindUserById(transferVM.userID);
            ViewBag.groupID = groupRepo.GetGroupSelectList();
            if (ModelState.IsValid)
            {
                //Ta User ID från viewmodell, sök i User efter användern med ID, ändra användarens grupp till viewmodells grupp.
                repo.FindUserById(transferVM.userID).gId = transferVM.groupID;
                repo.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit");
        }

        // GET: Transfer/Delete/5
        //public ActionResult Delete(string id)
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
        //    return View(applicationUser);
        //}

        //// POST: Transfer/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    ApplicationUser applicationUser = repo.FindUserById(id);
        //    db.user.Remove(applicationUser);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
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
