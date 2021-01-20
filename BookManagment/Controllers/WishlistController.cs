using BLL;
using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookManagment.Controllers
{
    public class WishlistController : Controller
    {
        // GET: Wishlist
        public ActionResult AddWishlist(int id)
        {
            Books thebook = BussinessManager.GetBookdetails(id);


            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            Customer customer = (Customer)this.Session["user"];

            List<Books> allbook = BussinessManager.GetWishList(customer.customerid);
            this.ViewData["books"] = allbook;
            return View();
        }

        [HttpPost]
        public ActionResult Add(int cid, int bid)
        {


            if (BussinessManager.AddWishlist(cid, bid))
            {
                return this.RedirectToAction("bookslist", "books");

            }
            return View();
        }

        [HttpPost]
        public ActionResult delete(int cid, int bid)
        {
            if (BussinessManager.deletewishlist(cid,bid))
            {
                return this.RedirectToAction("Index", "Wishlist");
            }
            return View();
        }
    }
}