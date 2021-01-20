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
                return this.RedirectToAction("Insertbook", "Books");
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

        public ActionResult Updatebook(int id)
        {
            Books getbook = BussinessManager.GetBookdetails(id);
            return View(getbook);
        }

        [HttpPost]
        public ActionResult Updatebook(int bid,string bname, string bdisc, string aname, string aathor, string bpub, double bpp, double bhp, double ebp, int bpages, string blang, string bdate, string bdimen, double brat, string bimag, string bimag2, string bimag3)
        {
            Books newbook = new Books
            {
                booksID = bid,
                bookname = bname,
                bookdisc = bdisc,
                bookauthor = aname,
                aboutauthor = aathor,
                bookpublisher = bpub,
                paperprice = bpp,
                hardprice = bhp,
                ebookprice = ebp,
                bookspage = bpages,
                booklang = blang,
                bookdate = bdate,
                rating = brat,
                bookdimension = bdimen,
                image = bimag,
                image2 = bimag2,
                image3 = bimag3
            };
            bool status = BussinessManager.UpdateBook(newbook);

            if (status)
            {
                return this.RedirectToAction("index","home");
            }
            return View();
        }

    }
}