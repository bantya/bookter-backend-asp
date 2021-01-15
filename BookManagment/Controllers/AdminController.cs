using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;

namespace BookManagment.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string uname , string upass)
        {
            Admin newadmin = new Admin
            {
                email = uname,
                password = upass
            };
            bool status  = BussinessManager.AdminLogin(uname, upass);

            if (status)
            {
                return this.RedirectToAction("index", "home");
            }
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(/*int id,*/string fname , string lname , string email,string cont , string pass)
        {
            Admin newadmin = new Admin
            {
                //adminid = id,
                fname = fname,
                lname = lname,
                email = email,
                contact = cont,
                password = pass
            };

            bool status = BussinessManager.adminregister(newadmin);

            if (status)
            {
                return this.RedirectToAction("Login", "Admin");
            }
            return View();

        }

    }
}