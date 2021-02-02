using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;
namespace BookManagment.Controllers
{
    [RoutePrefix("settings")]
    public class SettingsController : Controller
    {
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            Customer customer = (Customer)this.Session["user"];
            Address add = BussinessManager.findaddress(customer.customerid);
            return View(add);
        }

        [HttpPost]
        [Route("update/name")]
        public ActionResult changename(string firstname, string lastname)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.changename(firstname, lastname, customer.customerid);

            if (status)
            {
                return this.RedirectToAction("index", "home");
            }
            return View();
        }

        [HttpPost]
        [Route("update/email")]
        public ActionResult changeemail(string email)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.changeemail(email, customer.customerid);

            if (status)
            {
                return this.RedirectToAction("index", "home");
            }
            return View();

        }

        [HttpPost]
        [Route("update/password")]
        public ActionResult changepassword(string currentpassword, string newpassword)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.changepassword(currentpassword, newpassword, customer.customerid);

            if (status)
            {
                return this.RedirectToAction("index", "home");
            }
            return View();

        }
        [HttpPost]
        [Route("update/address")]
        public ActionResult changeadd(string flat_no, string build_no, string area, string street, string city, string district, string state, string pincode)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.updateAddress(customer.customerid, flat_no, build_no, area, street, city, district, state, pincode);

            if (status)
            {
                return this.RedirectToRoute("cart");
            }
            return View();
        }


        [HttpPost]
        [Route("add/address")]
        public ActionResult addaddress(string flat_no, string build_no, string area, string street, string city, string district, string state, string pincode)
        {
            Customer customer = (Customer)this.Session["user"];
            bool status = BussinessManager.addNewaddress(customer.customerid, flat_no, build_no, area, street, city, district, state, pincode);

            if (status)
            {
                return this.RedirectToRoute("cart");
            }

            return View();
        }
    }
}