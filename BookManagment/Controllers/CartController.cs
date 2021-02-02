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
       [Route("cart", Name = "cart")]
        public ActionResult Index()
        {
            Cart cart = (Cart)this.Session["carts"];
            return View(cart);
        }
        // GET: Cart

        [HttpPost]
        [Route("cart/{id}/add")]
        public ActionResult AddtoCart(int id)
        {
            Books book = BussinessManager.GetBookdetails(id);
            Cart cart = (Cart)this.Session["carts"];
            cart.items.Add(new Item { book = book });
            return this.RedirectToAction("index", "cart");
        }

        [HttpPost]
        [Route("cart/{id}/delete")]
        public ActionResult DeleteFromCart(int id)
        {
            Books book = BussinessManager.GetBookdetails(id);
            Cart cart = (Cart)this.Session["carts"];
            Cart cartCopy = cart;

            foreach(Item item in cartCopy.items)
            {
                if (item.book.booksID == id)
                {
                    cartCopy.items.Remove(item);
                    break;
                }
            }

            this.Session["cart"] = cartCopy;

            return this.RedirectToAction("index", "cart");
        }

        [HttpPost]
        [Route("cart/{id}/quantity")] 
        public ActionResult UpdateQuantity(int id, int qtt)
        {
            Cart cart = (Cart)this.Session["carts"];

            foreach(Item item in cart.items)
            {
                if (item.book.booksID == id)
                {
                    item.quantity = qtt;
                }
            }

            return null;
        }
    }
}