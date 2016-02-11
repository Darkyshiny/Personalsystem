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
    [Authorize(Roles = "Admin,Executive")]
    public class BlogPostsController : Controller
    {
        private Repo repo = new Repo();
        private BlogPostRepo blogRepo = new BlogPostRepo();

        // GET: BlogPosts
        [Authorize(Roles = "Admin,Executive,Employee")]
        public ActionResult Index()
        {
            return View(blogRepo.GetAll());
        }

        // GET: BlogPosts/Details/5
        [Authorize(Roles = "Admin,Executive,Employee")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = blogRepo.Find(id.Value);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Create
        
        public ActionResult Create()
        {
            //ViewBag.userList = db.user.ToList();
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,publicPost,Content")] BlogPost blogPost)
        {
            string userid = User.Identity.GetUserId();
            ApplicationUser currentUser = repo.FindUserById(userid);

            if (ModelState.IsValid)
            {
                blogPost.cId = currentUser.cId.Value;
                blogPost.postedBy = currentUser.UserName;
                blogPost.Timestamp = DateTime.Now;
                blogRepo.Save(blogPost);
                return RedirectToAction("Index");
            }

            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = blogRepo.Find(id.Value);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,cId,Content,Timestamp")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                blogRepo.Edit(blogPost);
                return RedirectToAction("Index");
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = blogRepo.Find(id.Value);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = blogRepo.Find(id);
            blogRepo.Delete(blogPost);
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
