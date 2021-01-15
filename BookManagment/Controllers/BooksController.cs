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


    }


}
