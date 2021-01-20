using BOL;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookManagment.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            string username = RouteData.Values["id"].ToString();

            if (username.Substring(0,1) ==  "@")
            {
                this.ViewData["profile"] = BussinessManager.getUserByUsername(username.Substring(1));
                List<Posts> allposts = BussinessManager.GetallPostsByUsername(username.Substring(1));
                return View(allposts);

            }

            return RedirectToAction("index", "home");
        }
    }
}