using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;

namespace BookManagment.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult Index()
        {
            List<Books> allbook = BussinessManager.Getbook();
            this.ViewData["books"] = allbook;
            return View();
        }

        public ActionResult Details(int id)
        {
            Books thebook = BussinessManager.GetBookdetails(id);
            return View(thebook);
        }

        public ActionResult Insertbook()
        {
            return View();
        } 

        public ActionResult BooksList()
        {
            List<Books> allbook = BussinessManager.Getbook();
            this.ViewData["books"] = allbook;
            return View();
        }

        [HttpPost]
        public ActionResult Insertbook(/*int bid,*/string bname,string bdisc,string aname,string aathor,string bpub,double bpp,double bhp ,double ebp,int bpages,string blang,string bdate,string bdimen,double brat,string bimag,string bimag2,string bimag3)
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
                return this.RedirectToAction("admindashboard","admin");
            }
            return View();
        }
    }


}
