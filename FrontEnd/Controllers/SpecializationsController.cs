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
    public class SpecializationsController : BaceController
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!IsLoggedIn()) filterContext.Result = RedirectToAction("Login", "Account");

            base.OnActionExecuting(filterContext);
        }

        private TccDB db = new TccDB();

        // GET: Specializations
        public ActionResult Index()
        {
            return View(db.Specializations.ToList());
        }

        // GET: Specializations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Specializations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Specialization specialization)
        {
            if (ModelState.IsValid)
            {
                if (db.Specializations.Where(c => c.Name == specialization.Name).SingleOrDefault() != null)
                {
                    ViewBag.erorr = "An specialization with this Name already exists.";
                    return View(specialization);
                }

                db.Specializations.Add(specialization);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(specialization);
        }

        // GET: Specializations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialization specialization = db.Specializations.Find(id);
            if (specialization == null)
            {
                return HttpNotFound();
            }
            return View(specialization);
        }

        // POST: Specializations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Specialization specialization)
        {
            if (ModelState.IsValid)
            {
                if (db.Specializations.Where(c => c.Name == specialization.Name).SingleOrDefault() != null)
                {
                    ViewBag.erorr = "An specialization with this Name already exists.";
                    return View(specialization);
                }

                db.Entry(specialization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(specialization);
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
