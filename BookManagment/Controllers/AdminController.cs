using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;

namespace BookManagment.Controllers
{
    [RoutePrefix("admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public ActionResult Login(Models.AdminLogin login)
        {
            TryValidateModel(login);

            if (ModelState.IsValid)
            {
                bool status = BussinessManager.AdminLogin(login.Username, login.Password);

                if (status)
                {
                    return this.RedirectToRoute("admin.dashboard");
                    //return this.RedirectToAction("dashboard","admin");
                }

            }
            return View();
        }

        [Route("dashboard", Name = "admin.dashboard")]
        public ActionResult dasboard()
        {
            return View();
        }

        [Route("update")]
        public ActionResult Update()
        {
            List<Books> allbook = BussinessManager.Getbook();
            this.ViewData["books"] = allbook;
            return View();
        }


        

        [Route("register")]
        public ActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("register")]
        public ActionResult Registration(Models.AdminRegistration adminRegist)
        {
            TryValidateModel(adminRegist);

            if (ModelState.IsValid)
            {
                Admin newadmin = new Admin
                {
                    //adminid = id,
                    fname = adminRegist.First_Name,
                    lname = adminRegist.Last_Name,
                    email = adminRegist.Email,
                    contact = adminRegist.Contact,
                    password = adminRegist.Password
                };

                bool status = BussinessManager.adminregister(newadmin);

                if (status)
                {
                    return this.RedirectToAction("Login", "Admin");
                }
            }
            return View();

        }

        [HttpGet]
        [Route("book/update/{id}")]
        public ActionResult Updatebook(int id)
        {
            Books getbook = BussinessManager.GetBookdetails(id);
            return View(getbook);
        }

        [HttpPost]
        [Route("book/update")]
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

        [HttpGet]
        [Route("book/updateprice/{id}")]
        public ActionResult UpdateByPrice(int id)
        {
            Books getbook = BussinessManager.GetBookdetails(id);
            return View(getbook);
        }

        [HttpPost]
        [Route("book/updateprice")]
        public ActionResult UpdateByPrice(int bid, double bpp, double bhp, double ebp)
        {
            bool status = BussinessManager.UpdateBookbyPrice(bid, bpp, bhp, ebp);

            if (status)
            {
                return this.RedirectToAction("dasboard", "admin");
            }
            // TO AVOID NULL ERROR 
            Books getbook = BussinessManager.GetBookdetails(bid);
            return View(getbook);
        }

        [HttpGet]
        [Route("book/archive/{id}")]
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

        [HttpGet]
        [Route("book/insert")]
        public ActionResult Insertbook()
        {
            return View();
        }

        [HttpPost]
        [Route("book/insert")]
        public ActionResult Insertbook(/*int bid,*/string bname, string bdisc, string aname, string aathor, string bpub, double bpp, double bhp, double ebp, int bpages, string blang, string bdate, string bdimen, double brat, string bimag, string bimag2, string bimag3)
        {
            Books newbook = new Books
            {
                //booksID = bid,
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

            bool status = BussinessManager.Insertbook(newbook);
            if (status)
            {
                return this.RedirectToAction("dashboard", "admin");
            }
            return View();
        }

    }
}