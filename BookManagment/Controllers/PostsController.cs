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
            List<Posts> allposts = BussinessManager.GetallPosts(customer.customerid);
            return View(allposts);
        }


        [HttpPost]
        public ActionResult Index(string postcontent,int postbooks)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.insertpost(customer.customerid, postcontent, postbooks);

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
    }
}