using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;

namespace BookManagment.Controllers
{
    public class PostsController : Controller
    {
        // GET: Posts
        public ActionResult Index()
        {
           
            this.ViewData["books"] = this.Session["books"];
            Customer customer = (Customer)this.Session["user"];
            
            if (customer == null)
            {
                return this.RedirectToAction("login", "account");
            }
            List<Posts> allposts = BussinessManager.GetallPosts(customer.customerid);
            List<Likes> like = BussinessManager.GetallLikes(customer.customerid);
            ViewData["likes"] = like;
            return View(allposts);
        }


        [HttpPost]
        public ActionResult Index(string postcontent,int postbooks)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.insertpost(customer.customerid, postcontent, postbooks,customer.customer_name);

            if (status)
            {
                return this.RedirectToAction("index", "posts");
            }
            return View();

        }

        public ActionResult Delete(int id)
        {
            Customer customer = (Customer)this.Session["user"];

            if (BussinessManager.deletepost(id,customer.customerid ))
            {
                return this.RedirectToAction("index", "posts");

            }
            return View();
        }

        [HttpPost]
        public ActionResult like(int id)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.AddLike(customer.customerid, id);
            if (status)
            {
                return RedirectToAction("index", "posts");
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
                return RedirectToAction("index", "posts");
            }
            return View();
        }

        //public ActionResult GetallLikes()
        //{

        //    return View();
        //}

        [HttpPost]
        public ActionResult addcomment(string postcomment,int userid)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.insertcomment(customer.customerid, postcomment, customer.customer_name,userid);

            if (status)
            {
                return this.RedirectToAction("index", "posts");
            }
            return View();

        }
    }
}