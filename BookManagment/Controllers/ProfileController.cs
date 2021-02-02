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

        [Route("@{id}")]
        public ActionResult Index()
        {
            string username = RouteData.Values["id"].ToString();

            //if (username.Substring(0,1) ==  "@")
           // {
            Customer customer = (Customer)this.Session["user"];
            Customer user = BussinessManager.getUserByUsername(username);
            List<Books> books = BussinessManager.getPurchasedBooks(user.customerid);

            this.ViewData["books"] = books;

            this.ViewData["profile"] = user;
            List<Posts> allposts = BussinessManager.GetallPostsByUsername(username);
            if (customer != null && user != null)
            {
                this.ViewData["follower"] = BussinessManager.CheckFollow(username, customer.customerid);
            }
            return View(allposts);

           // }

            //return RedirectToAction("index", "home");
        }

        [HttpPost]
        [Route("post/{id}/like")]
        public ActionResult like(int id,string uname)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.AddLike(customer.customerid, id);
            if (status)
            {
                return Redirect("/@"+uname);
            }
            return View();
        }

        [HttpPost]
        [Route("post/{id}/dislike")]
        public ActionResult dislike(int id,string uname)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.AddDislike(customer.customerid, id);
            if (status)
            {
                return Redirect("/@" + uname);
            }
            return View();
        }

        [HttpPost]
        [Route("user/{id}/follow")]
        public ActionResult follow(int id,string fname)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.AddFollow(customer.customerid, id,fname);
            if (status)
            {
                return Redirect("/@" + fname);
            }
            return View();
        }

        [HttpPost]
        [Route("user/{id}/unfollow")]
        public ActionResult unfollow(int id, string fname)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.unFollow(customer.customerid, id, fname);
            if (status)
            {
                return Redirect("/@" + fname);
            }
            return View();
        }

        [HttpGet]
        [Route("me")]
        public ActionResult Me()
        {
            Customer customer = (Customer)this.Session["user"];

            if (customer == null)
            {
                this.Session["message"] = "Please login first..";
                return RedirectToRoute("account.login");
            }
            List<Books> books = BussinessManager.getPurchasedBooks(customer.customerid);

            this.ViewData["books"] = books;

            List<Posts> post = BussinessManager.GetallUsersPosts(customer.customerid);
            return View(post);
        }

        //[HttpGet]
        //[Route("settings")]
        //public ActionResult Settings()
        //{
        //    return View();
        //}
    }
}