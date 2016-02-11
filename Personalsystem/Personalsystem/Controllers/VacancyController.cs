using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Personalsystem.Models;
using Personalsystem.DataAccessLayer;
using System.IO;
using Microsoft.AspNet.Identity;
using System.Dynamic;
using System.ComponentModel;
using System.Net;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Personalsystem.Controllers
{
    public class VacancyController : Controller
    {
        private PersonalSystemContext db = new PersonalSystemContext();
        private Vacancy repo = new Vacancy();

        // GET: Vacancy
        public ActionResult Index(int cId)
        {
            TempData["CompanyId"] = cId;
            var companyId = db.company.Find(cId);
            var vacancy = repo.ListVacancies(companyId);
            if (vacancy.Count <= 0)
                ViewBag.Message = "Sorry no vacancies for the company at the moment. Please check back later!";
            return View(vacancy);
        }

        // GET: Apply
        [Authorize(Roles = ("Job Searcher"))]
        public ActionResult Apply(int vId)
        {
            ApplicationUser user = db.user.Find(User.Identity.GetUserId());

            TempData["AppId"] = vId;
            ViewBag.Description = db.vacancy.Find(vId).Description.ToString();
            //Get name of CV
            string applicantId = User.Identity.GetUserId();
            var cv = db.user.Find(applicantId);
            if (cv.CVurl != null)
            {
                string filename = cv.CVurl.ToString();
                string result = Path.GetFileName(filename);
                ViewData.Add("CV", filename);
                ViewData.Add("File", result);
            }
            else
            {
                TempData["Error"] = "You have to load up your CV, before you can make application! Click Hello in the header.";
            }

            return View("CoverLetter");
        }
        // GET: Files
        public ActionResult DownloadCV(string url)
        {
            if (url != null)
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
            TempData["Error"] = "You have to load up your CV! Click Hello in the header.";
            return View("CoverLetter");
        }

        // POST: File/Create
        [HttpPost]
        public ActionResult SendAppl(string letter)
        {
            if (letter.Length == 0)
            {
                TempData["Error"] = "To save, Cover Letter must be given";
                return View("CoverLetter");
            }

            // Update Application and User
            Application app = new Application();
            var user = db.user.Find(User.Identity.GetUserId());
            if (user.CVurl != null)
            {
                app.uId = user.Id;
                app.CoverLetter = letter;
                int test;
                int.TryParse(TempData["AppId"].ToString(), out test);
                app.vId = test;

                db.application.Add(app);
                db.SaveChanges();
                letter = "";
                TempData["Error"] = "Your application has been submitted.";
            }
            else
            {
                TempData["Error"] = "You have to load up your CV! Click Welcome in the header.";
            }

            return View("CoverLetter");
        }
        //GET: Applicants for Company
        [Authorize(Roles = ("Executive"))]
        public ActionResult ShowApplicants()
        {
            ApplicationUser user = db.user.Find(User.Identity.GetUserId());

            int compId = Convert.ToInt32(user.cId);

            TempData["CompanyId"] = compId;
            var emp = (from a in db.application
                       join b in db.vacancy on a.vId equals b.Id
                       where b.cId == compId
                       join c in db.user on a.uId equals c.Id
                       select new { b.Description, c.Name, c.Surname, c.Email, a.CoverLetter, a.Id, c.CVurl });

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
            TempData["AppId"] = id;
            Application applicationUser = db.application.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            string filedata = applicationUser.CoverLetter;
            string contentType = MimeMapping.GetMimeMapping(applicationUser.CoverLetter);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "Cover Letter",
                Inline = true,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);

        }

        public ActionResult Hire(string id)
        {
            //hire person
            TempData["ApplId"] = id;
            int appId = Convert.ToInt32(id);

            var emp = (from a in db.user
                       join b in db.application on a.Id equals b.uId
                       where b.Id == appId
                       select new { a.Id, a.Name, a.Surname });

            foreach (var item in emp)
            {
                TempData["EmpId"] = item.Id;
                TempData["Name"] = item.Name;
                TempData["Surname"] = item.Surname;
            }

            int CId = Convert.ToInt32(TempData["CompanyId"]);

            List<Department> DepartmentList = db.department.Where(r => r.cId == CId).ToList();
            List<Group> GroupList = db.group.Where(r => r.department.cId == CId).ToList();

            List<SelectListItem> Departments = new List<SelectListItem>();
            foreach (var item in DepartmentList)
            {
                Departments.AddRange(new[] { new SelectListItem() { Text = item.Description, Value = ((int)item.Id).ToString() } });
            }
            ViewData.Add("Department", Departments);

            List<SelectListItem> Groups = new List<SelectListItem>();
            foreach (var item in GroupList)
            {
                Groups.AddRange(new[] { new SelectListItem() { Text = item.Description, Value = ((int)item.Id).ToString() } });
            }
            ViewData.Add("Group", Groups);

            return View();
        }
        // POST: File/Create
        [HttpPost]
        public ActionResult Apply(string letter)
        {
            if (letter.Length == 0)
            {
                TempData["Error"] = "To save, Cover Letter must be given";
                return RedirectToAction("Index");
            }

            // Update Application and User
            Application app = new Application();
            var user = db.user.Find(User.Identity.GetUserId());
            app.uId = user.Id;
            app.CoverLetter = letter;
            int test;
            int.TryParse(TempData["AppId"].ToString(), out test);
            app.vId = test;

            db.application.Add(app);
            db.SaveChanges();
            return View("Index");
        }
        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Executive"))]
        public ActionResult Create(int Department, int Group)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser ExUser = db.user.Find(User.Identity.GetUserId());

                int appId = Convert.ToInt32(TempData["ApplId"]);
                string empId = TempData["EmpId"].ToString();

                var emp = db.user.Find(empId);
                emp.cId = ExUser.cId;
                emp.gId = Group;
                db.SaveChanges();

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                UserManager.RemoveFromRole(emp.Id, "Job Searcher");
                UserManager.AddToRole(emp.Id, "Employee");

                //delete other applicants
                var actvac = db.application.Find(appId);

                var del = db.application.Where(a => a.vId == actvac.vId);
                if (del != null)
                {
                    db.application.RemoveRange(del);
                }

                //delete other applications same person applied for
                var del2 = db.application.Where(a => a.uId == actvac.uId);
                if (del2 != null)
                {
                    db.application.RemoveRange(del2);
                }


                //Null vacancy
                var vac = db.vacancy.Find(actvac.vId);
                vac.Active = false;

                db.SaveChanges();
                return RedirectToAction("ShowApplicants", "Vacancy");
            }

            return View();
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