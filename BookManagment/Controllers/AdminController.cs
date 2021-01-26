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

        public ActionResult dasboard()
        {

           
            return View();
        }

        public ActionResult Update()
        {
            List<Books> allbook = BussinessManager.Getbook();
            this.ViewData["books"] = allbook;
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
                return this.RedirectToAction("dasboard", "admin");
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
                return this.RedirectToAction("dasboard", "admin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult UpdateByPrice(int bid,double bpp, double bhp, double ebp)
        {
            bool status = BussinessManager.UpdateBookbyPrice(bid,bpp,bhp,ebp);

            if (status)
            {
                return this.RedirectToAction("dasboard", "admin");
            }
            // TO AVOID NULL ERROR 
            Books getbook = BussinessManager.GetBookdetails(bid);
            return View(getbook);
        }

        
        public ActionResult UpdateByPrice(int id)
        {
            Books getbook = BussinessManager.GetBookdetails(id);
            return View(getbook);
        }

        public ActionResult ArchiveBook(int id)
        {
            Books getbook = BussinessManager.GetBookdetails(id);
            
            if (getbook.status == 1)
            {
                bool status = BussinessManager.ArchiveBook(getbook.booksID);
            }
            else
            {
                bool status = BussinessManager.CancelArchive(getbook.booksID);
            }

            return this.RedirectToAction("dasboard", "admin");

            
        }

    }
}