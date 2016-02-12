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
using Personalsystem.Repositories;

namespace Personalsystem.Controllers
{
    public class VacancyController : Controller
    {
        private PersonalSystemContext db = new PersonalSystemContext();
        private VacancyRepo vRepo = new VacancyRepo();
        private ApplicationRepo aRepo = new ApplicationRepo();
        private CompanyRepo cRepo = new CompanyRepo();
        private DepartmentRepo dRepo = new DepartmentRepo();
        private GroupRepo gRepo = new GroupRepo();
        private Repo repo = new Repo();

        // GET: Vacancy
        public ActionResult Index(int cId)
        {
            var companyId = cRepo.Find(cId);
            var vacancy = vRepo.ListVacancies(companyId);
            if (vacancy.Count <= 0)
                ViewBag.Message = "Sorry no vacancies for the company at the moment. Please check back later!";
            ViewBag.CompanyName = companyId.Name;
            return View(vacancy);
        }

        // GET: Apply
        public ActionResult Apply(int vId)
        {

            TempData["AppId"] = vId;
            ViewBag.Description = vRepo.Find(vId).Description.ToString();
            //Get name of CV
            string applicantId = User.Identity.GetUserId();
            var cv = repo.FindUserById(applicantId);
            string filename = cv.CVurl.ToString();
            string result = Path.GetFileName(filename);
            ViewData.Add("CV", filename);
            ViewData.Add("File", result);

            return View("CoverLetter");
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
            var user = repo.FindUserById(User.Identity.GetUserId());
            app.uId = user.Id;
            app.CoverLetter = letter;
            int test;
            int.TryParse(TempData["AppId"].ToString(), out test);
            app.vId = test;

            aRepo.Save(app);
            return RedirectToAction("Index");
        }
        //GET: Applicants for Company
        [Authorize(Roles="Admin,Executive")]
        public ActionResult ShowApplicants()
        {

            ApplicationUser user = repo.FindUserById(User.Identity.GetUserId());

            int compId = Convert.ToInt32(user.cId);
            

            TempData["CompanyId"] = compId;
            var emp = (from a in db.application
                       join b in db.vacancy on a.vId equals b.Id
                       where b.cId == compId
                       join c in db.user on a.uId equals c.Id
                       select new { b.Description, c.Name, c.Surname, c.Email, a.CoverLetter, a.Id, c.CVurl });
            
            
            
            //(from a in db.application
            // join b in db.vacancy on a.vId equals b.Id
            // where b.cId == compId
            // join c in db.user on a.uId equals c.Id
            // select new { b.Description, c.Name, c.Surname, c.Email, a.CoverLetter, a.Id, c.CVurl });



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
            Application applicationUser = aRepo.Find(id.Value);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);

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

            //int CId = Convert.ToInt32(TempData["CompanyId"]);
            int CId = 1;

            List<Department> DepartmentList = dRepo.GetAll().Where(r => r.cId == CId).ToList();
            List<Group> GroupList = gRepo.GetAll().Where(r => r.department.cId == CId).ToList();

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
            if (letter.Length == 0 )
            {
                TempData["Error"] = "To save, Cover Letter must be given";
                return RedirectToAction("Index");
            }
 
            // Update Application and User
            Application app = new Application();
            var user = repo.FindUserById(User.Identity.GetUserId());
            app.uId = user.Id;
            app.CoverLetter = letter;
            int test;
            int.TryParse(TempData["AppId"].ToString(), out test);
            app.vId = test;

            aRepo.Save(app);
            return View("Index");
        }
        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = (""))]
        public ActionResult Create(int Department, int Group)
        {
            if (ModelState.IsValid)
            {
                int appId = Convert.ToInt32(TempData["ApplId"]);
                string empId = TempData["EmpId"].ToString();

                var emp = repo.FindUserById(empId);
                emp.cId = Convert.ToInt32(TempData["CompanyId"]);
                emp.gId = Group;

                //delete other applicants
                var actvac = aRepo.Find(appId);

                var del = aRepo.GetAll().Where(a => a.vId == actvac.vId);
                if (del != null)
                {
                    aRepo.RemoveRange(del);
                }

                //Null vacancy
                var vac = vRepo.Find(actvac.vId);
                vac.Active = false;

                repo.SaveChanges();
                return RedirectToAction("ShowApplicants", "Vacancy");
            }

            return View();
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