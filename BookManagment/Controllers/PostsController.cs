using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;
using Utils;

namespace BookManagment.Controllers
{
    public class PostsController : Controller
    {
        // GET: Posts
        [Route("posts" , Name = "posts")]
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
        [Route("posts")]
        public ActionResult Index(string postcontent, int postbooks)
        {
            Customer customer = (Customer)this.Session["user"];
            int post_id = BussinessManager.insertpost(customer.customerid, postcontent, postbooks,customer.customer_name);


            if (Request.Files.Count > 0 && Request.Files.Count <= 4)
            {
                string filePath = Server.MapPath("~/Images/Posts/");

                List<string> images = Files.Upload("~/Images/Posts/", 4, Server, Request);

                if (images != null)
                {
                    BussinessManager.UpdatePostImages(images, post_id);
                }
            }

            if (post_id != -1)
            {
                return this.RedirectToRoute("posts");
            }
            return View();

        }

        [HttpPost]
        [Route("post/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Customer customer = (Customer)this.Session["user"];

            if (BussinessManager.deletepost(id,customer.customerid ))
            {
                return this.RedirectToRoute("posts");

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
                return this.RedirectToRoute("posts");
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
                return this.RedirectToRoute("posts");
            }
            return View();
        }

        //public ActionResult GetallLikes()
        //{

        //    return View();
        //}

        [HttpPost]
        [Route("posts/add/comment")]
        public ActionResult addcomment(int postid, string postcommentcontent, int userid)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.insertcomment(postid, customer.customerid, postcommentcontent, customer.customer_name,userid);

            if (status)
            {
                return this.RedirectToRoute("posts");
            }
            return View();

        }
    }
}