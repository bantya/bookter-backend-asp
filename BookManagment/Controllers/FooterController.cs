using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookManagment.Controllers
{
    public class FooterController : Controller
    {
        // GET: Footer
        public ActionResult Privacypolicy()
        {
            return View();
        }

        public ActionResult Aboutus()
        {
            return View();
        }
    }
}