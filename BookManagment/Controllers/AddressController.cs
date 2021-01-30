using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;

namespace BookManagment.Controllers
{
    public class AddressController : Controller
    {
        // GET: Address
        [Route("address")]   
        public ActionResult Index()
        {
            Customer customer = (Customer)this.Session["user"];
            Address add = BussinessManager.findaddress(customer.customerid);

            return View(add);
        }


        [HttpPost]
        public ActionResult changeadd(string flat_no, string build_no, string area, string street, string city, string district, string state, string pincode)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.changeadd(customer.customerid, flat_no,build_no,area,street,city,district,state,pincode);

            if (status)
            {
                return this.RedirectToRoute("cart");
            }
            return View();
        }


        [HttpPost]
        public ActionResult addaddress(string flat_no,string build_no,string area ,string street,string city,string district,string state,string pincode)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.addaddress(customer.customerid,flat_no,build_no,area,street,city,district,state,pincode);

            if (status)
            {
                return this.RedirectToRoute("cart");
            }

            return View();
        }
    }
}