using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;

namespace BookManagment.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        [Route("checkout")]
        public ActionResult Index()
        {
            Customer customer = (Customer)this.Session["user"];
            
            Cart existingCart = (Cart)this.Session["carts"];
            Address add = BussinessManager.findaddress(customer.customerid);
            this.ViewData["address"] = add;
            return View(existingCart);
        }
    }
}