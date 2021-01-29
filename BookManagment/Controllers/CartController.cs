using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;

namespace BookManagment.Controllers
{
    public class CartController : Controller
    {
       [Route("cart")]
        public ActionResult Index()
        {
            Cart existingCart = (Cart)this.Session["carts"];
            return View(existingCart);
        }
        // GET: Cart

        [HttpGet]
        [Route("cart/add/{id}")]
        public ActionResult AddtoCart(int id)
        {
            Books thebook = BussinessManager.GetBookdetails(id);
            Cart existingCart = (Cart)this.Session["carts"];
            existingCart.items.Add(thebook);
            return this.RedirectToAction("index", "cart");
        }
    }
}