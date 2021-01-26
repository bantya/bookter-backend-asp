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
                Customer customer = (Customer)this.Session["user"];
                Customer user = BussinessManager.getUserByUsername(username.Substring(1));
                this.ViewData["profile"] = user;
                List<Posts> allposts = BussinessManager.GetallPostsByUsername(username.Substring(1));
                if (customer != null && user != null)
                {
                    this.ViewData["follower"] = BussinessManager.CheckFollow(username.Substring(1), customer.customerid);
                }
                return View(allposts);

            }

            return RedirectToAction("index", "home");
        }

        [HttpPost]
        public ActionResult like(int id)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.AddLike(customer.customerid, id);
            if (status)
            {
                return Redirect("profile/index/@"+customer.customer_name);
            }
            return View();
        }

        [HttpPost]
        public ActionResult dislike(int id)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.AddDislike(customer.customerid, id);
            if (status)
            {
                return RedirectToAction("index", "profile");
            }
            return View();
        }

        [HttpPost]
        public ActionResult follow(int id,string fname)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.AddFollow(customer.customerid, id,fname);
            if (status)
            {
                return Redirect("/profile/index/@" + fname);
            }
            return View();
        }

        [HttpPost]
        public ActionResult unfollow(int id, string fname)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.unFollow(customer.customerid, id, fname);
            if (status)
            {
                return Redirect("/profile/index/@" + fname);
            }
            return View();
        }
    }
}