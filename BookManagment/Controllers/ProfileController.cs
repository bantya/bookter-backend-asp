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

        [Route("profile/{id}")]
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
        public ActionResult like(int isliked,string uname)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.AddLike(customer.customerid, isliked);
            if (status)
            {
                return Redirect("/profile/@"+uname);
            }
            return View();
        }

        [HttpPost]
        public ActionResult dislike(int isdisliked,string uname)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.AddDislike(customer.customerid, isdisliked);
            if (status)
            {
                return Redirect("/profile/@" + uname);
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

        [HttpGet]
        [Route("profile/me")]
        public ActionResult Me()
        {
            Customer customer = (Customer)this.Session["user"];

            if (customer == null)
            {
                this.Session["message"] = "Please login first..";
                return RedirectToRoute("account.login");
            }

            List<Posts> post = BussinessManager.GetallUsersPosts(customer.customerid);
            return View(post);
        }
    }
}