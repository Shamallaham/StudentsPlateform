using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using LINQtoCSV;
using System.Web.Mvc;
using TCC.BackEnd;
using System.IO;

namespace TCC.FrontEnd.Controllers
{
    public class UsersController : BaceController
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!IsLoggedIn()) filterContext.Result = RedirectToAction("Login", "Account");

            base.OnActionExecuting(filterContext);
        }

        private TccDB db = new TccDB();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Specialization);
            return View(users.ToList());
        }
        

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.SpecializationID = new SelectList(db.Specializations, "ID", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Nickname,Mother,Father,BirthDate,SpecializationID,Phone,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Where(c => c.Email == user.Email).SingleOrDefault() != null)
                {
                    ViewBag.erorr = "An account with this Email already exists.";
                    ViewBag.SpecializationID = new SelectList(db.Specializations, "ID", "Name", user.SpecializationID);
                    return View(user);
                }
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpecializationID = new SelectList(db.Specializations, "ID", "Name", user.SpecializationID);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpecializationID = new SelectList(db.Specializations, "ID", "Name", user.SpecializationID);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Nickname,Mother,Father,BirthDate,SpecializationID,Phone,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Where(c => c.Email == user.Email && c.ID!=user.ID).SingleOrDefault() != null)
                {
                    ViewBag.erorr = "An account with this Email already exists.";
                    ViewBag.SpecializationID = new SelectList(db.Specializations, "ID", "Name", user.SpecializationID);
                    return View(user);
                }

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpecializationID = new SelectList(db.Specializations, "ID", "Name", user.SpecializationID);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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




        [HttpGet]
        public ActionResult UploadCsv()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadCsv(HttpPostedFileBase attachmentcsv)
        {
            if(attachmentcsv!=null)
            {
                CsvFileDescription csvFileDescription = new CsvFileDescription
                {
                    SeparatorChar = ',',
                    FirstLineHasColumnNames = true
                };
                CsvContext csvContext = new CsvContext();
                StreamReader streamReader = new StreamReader(attachmentcsv.InputStream);
                IEnumerable<User> list = csvContext.Read<User>(streamReader, csvFileDescription);
                db.Users.AddRange(list);
                db.SaveChanges();
                return Redirect("Index");
            }
            return View();
        }
        
    }
}
