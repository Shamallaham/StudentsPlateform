using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TCC.BackEnd;

namespace TCC.FrontEnd.Controllers
{
    public class SubjectsController : BaceController
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!IsLoggedIn()) filterContext.Result = RedirectToAction("Login", "Account");

            base.OnActionExecuting(filterContext);
        }

        private TccDB db = new TccDB();

        // GET: Subjects
        public ActionResult Index()
        {
            var subjects = db.Subjects.Include(s => s.Specialization);
            return View(subjects.ToList());
        }
        
        // GET: Subjects/Create
        public ActionResult Create()
        {
            ViewBag.SpecializationID = new SelectList(db.Specializations, "ID", "Name");
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Teacher,SpecializationID")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                if (db.Subjects.Where(c => c.Name == subject.Name).SingleOrDefault() != null)
                {
                    ViewBag.erorr = "An subject with this Name already exists";
                    ViewBag.SpecializationID = new SelectList(db.Specializations, "ID", "Name", subject.SpecializationID);
                    return View(subject);
                }
                if (subject.SpecializationID==1)
                {
                    ViewBag.erorr = "It is not possible to add articles on the specialty of the admin";
                    ViewBag.SpecializationID = new SelectList(db.Specializations, "ID", "Name", subject.SpecializationID);
                    return View(subject);
                }
                db.Subjects.Add(subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpecializationID = new SelectList(db.Specializations, "ID", "Name", subject.SpecializationID);
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpecializationID = new SelectList(db.Specializations, "ID", "Name", subject.SpecializationID);
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Teacher,SpecializationID")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                if (db.Subjects.Where(c => c.Name == subject.Name&&c.ID!=subject.ID).SingleOrDefault() != null)
                {
                    ViewBag.erorr = "An subject with this Name already exists.";
                    ViewBag.SpecializationID = new SelectList(db.Specializations, "ID", "Name", subject.SpecializationID);
                    return View(subject);
                }

                if (subject.SpecializationID == 1)
                {
                    ViewBag.erorr = "It is not possible to add articles on the specialty of the admin";
                    ViewBag.SpecializationID = new SelectList(db.Specializations, "ID", "Name", subject.SpecializationID);
                    return View(subject);
                }
                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpecializationID = new SelectList(db.Specializations, "ID", "Name", subject.SpecializationID);
            return View(subject);
        }

        // GET: Subjects/Delete/
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subject subject = db.Subjects.Find(id);
            db.Subjects.Remove(subject);
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
