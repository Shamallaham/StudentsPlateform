using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TCC.BackEnd;

namespace TCC.FrontEnd
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            //TccDB db = new TccDB();
            //db.Database.Delete();
            //db.Database.CreateIfNotExists();
            
            //Specialization s1 = new Specialization
            //{
            //    Name = "admin"
            //};
            //Specialization s2 = new Specialization
            //{
            //    Name = "Software"
            //};
            //Specialization s3 = new Specialization
            //{
            //    Name = "Computers"
            //};
            //Specialization s4 = new Specialization
            //{
            //    Name = "Network"
            //};
            //User u1 = new User
            //{
            //    Name = "saker",
            //    Nickname = "dakak",
            //    Father = "fff",
            //    Mother = "mmm",
            //    BirthDate = Convert.ToDateTime("22-05-2001"),
            //    Phone = "0934030517",
            //    SpecializationID = 1,
            //    Email = "saker@admin.com",
            //    Password = "123456789"
            //};
            //User u2 = new User
            //{
            //    Name = "sham",
            //    Nickname = "allaham",
            //    Father = "fff",
            //    Mother = "mmm",
            //    BirthDate = Convert.ToDateTime("01-01-2001"),
            //    Phone = "0900000000",
            //    SpecializationID = 1,
            //    Email = "sham@admin.com",
            //    Password = "123456789"
            //}; User u3 = new User
            //{
            //    Name = "blal",
            //    Nickname = "tllou",
            //    Father = "fff",
            //    Mother = "mmm",
            //    BirthDate = Convert.ToDateTime("01-01-2001"),
            //    Phone = "0900000000",
            //    SpecializationID = 2,
            //    Email = "blal@tcc.com",
            //    Password = "123456789"
            //};

            //db.Specializations.Add(s1);
            //db.Specializations.Add(s2);
            //db.Specializations.Add(s3);
            //db.Specializations.Add(s4);
            //db.Users.Add(u1);
            //db.Users.Add(u2);
            //db.Users.Add(u3);
            //db.SaveChanges();



            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}



//@Html.ValidationSummary(true, "", new { @class = "text-danger" })
//@Html.ValidationMessageFor(model => model.Name, "", new { @class = "text-danger" })