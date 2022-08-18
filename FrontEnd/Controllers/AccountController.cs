using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC.BackEnd;
using TCC.FrontEnd.Models;

namespace TCC.FrontEnd.Controllers
{
    public class BaceController : Controller
    {
        public User LoggedInUser()
        {
            if(IsLoggedIn()) return Session["Logged"] as User;
            return null;
        }

        public bool IsLoggedIn()
        {
            User Logged = Session["Logged"] as User;
            if (Logged != null) return true;
            return false;
        }
    }

    public class AccountController : BaceController
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            
           if(ModelState.IsValid)
            {
                TccDB db = new TccDB();
                var check = db.Users.Where(c => c.Email == loginModel.Email && c.Password == loginModel.Password).SingleOrDefault();
                if(check!=null)
                {
                    Session["Logged"] = check;

                    if (check.SpecializationID==1) return RedirectToAction("ControlPanel");

                    return RedirectToAction("Index","Student");
                }
                else
                {
                    ViewBag.Message = "Email or Password is wrong!";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index","Home");
        }

        public ActionResult ControlPanel()
        {
            if(IsLoggedIn() && LoggedInUser().SpecializationID==1)
            {
                ViewBag.name = LoggedInUser().Name;
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Student()
        {
            if (IsLoggedIn())
            {
                ViewBag.name = LoggedInUser().Name;
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

    }
}