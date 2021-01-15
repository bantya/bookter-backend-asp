using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;

namespace BookManagment.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            return View();

        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(string uname, string upass)
        {

            //Credentials newcredential = new Credentials
            //{
            //    Username = username,
            //    Password = password
            //};

            Customer newcus = new Customer
            {
                email=uname,
                password=upass
            };
            bool status = BussinessManager.validatecustomer(uname, upass);

            if (status)
            {
                return this.RedirectToRoute("");
            }

            return View();
        }

        [HttpGet]
        [Route("register")]
        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [Route("register")]
        public ActionResult Register(/*int uid ,*/ string uname ,string uemail ,string upass)
        {
            Customer cust = new Customer
            {
                //customerid = uid,
                customer_name = uname,
                email = uemail,
                password = upass

            };
            bool status = BussinessManager.register(cust);

            if (status)
            {
                return this.RedirectToRoute("login");
            }
            return View();
        }

        [HttpGet]
        [Route("forgot")]
        public ActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        [Route("forgot")]
        public ActionResult Forgot(string uemail)
        {
            Customer newcust = new Customer
            {
                email = uemail
            };

            bool status = BussinessManager.validateforgot(uemail);

            if (status)
            {


                int number = 10;
                string str = GenerateRandomAlphanumericString(number);

                string GenerateRandomAlphanumericString(int length)
                {
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

                    var random = new Random();
                    var randomString = new string(Enumerable.Repeat(chars, length)
                                                            .Select(s => s[random.Next(s.Length)]).ToArray());
                    return randomString;
                }

                forgotpass newfor = new forgotpass
                {
                    email = uemail,
                    token = str
                };

                bool status1 = BussinessManager.forgotpasse(newfor);
                //return this.RedirectToRoute("index", "home");
                if (status1)
                {
                    return this.RedirectToRoute("");
                }
            }

            return View();
        }

        [HttpGet]
        [Route("restore")]
        public ActionResult Restore(string token)
        {
            if (Request.QueryString["token"] != null)
            {
                //string token = Request.QueryString["token"];
                if (BussinessManager.checkToken(token))
                {
                    return this.RedirectToRoute("setpass");
                }
            }
            return this.RedirectToRoute("login");
        }


    }
}