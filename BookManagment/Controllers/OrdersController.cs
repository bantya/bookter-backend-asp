using BLL;
using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utils;

namespace BookManagment.Controllers
{
    public class OrdersController : Controller
    {
        [HttpGet]
        [Route("orders", Name = "orders.all")]
        public ActionResult Index()
        {
            Customer customer = (Customer)this.Session["user"];

            if (customer == null)
            {
                return RedirectToRoute("account.login");
            }

            List<Order> orders = BussinessManager.getOrders(customer.customerid);
            this.ViewData["orders"] = orders;
            return View();
        }

        [HttpGet]
        [Route("order/{token}")]
        public ActionResult Order(string token)
        {
            Customer customer = (Customer)this.Session["user"];
            List<OrderBooks> books = BussinessManager.getOrderBooks(token, customer.customerid);
            Address address = BussinessManager.findaddress(customer.customerid);
            this.ViewData["order_books"] = books;
            this.ViewData["address"] = address;
            this.ViewData["token"] = token;
            return View();
        }

        [HttpPost]
        [Route("order/create")]
        public ActionResult Create()
        {
            Customer customer = (Customer)this.Session["user"];
            Cart existingCart = (Cart)this.Session["carts"];
            string token = Randoms.Pattern("ord-{HEX:6}-{HEX:2}-{HEX:6}");
            double total = existingCart.items.Sum(x => Convert.ToInt32(x.book.paperprice));
            int book_id = existingCart.items[0].book.booksID;
            int qtt = existingCart.items.Count;

            BussinessManager.createOrder(customer.customerid, book_id, qtt, total, token);
            int ord_i = BussinessManager.getLatestOrderId(customer.customerid);


            foreach(Item item in existingCart.items)
            {
                BussinessManager.addBook(ord_i, item.book.booksID, item.quantity);
            }

            this.Session["carts"] = new Cart();

            return RedirectToRoute("orders.all");
            
            //return View();
        }

        //[HttpPost]
        //[Route("orders/invoice/{id}")]
        //public ActionResult GetInvoice(int id)
        //{
        //    Customer customer = (Customer)this.Session["user"];
        //    int ord_i = BussinessManager.getLatestOrderId(customer.customerid);
        //    return View();
        //}

    }
}