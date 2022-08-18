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
    public class StudentController : BaceController
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!IsLoggedIn()) filterContext.Result = RedirectToAction("Login", "Account");

            base.OnActionExecuting(filterContext);
        }

        private TccDB db = new TccDB();

        // GET: Student
        public ActionResult Index()
        {
            var logged = Session["Logged"] as User;
            var users = db.Subjects.Where(u => u.SpecializationID==logged.SpecializationID);
            return View(users.ToList());
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject user = db.Subjects.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
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
