using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using BLL;
using Models;
using Utils;
using Login = Models.Login;

namespace BookManagment.Controllers
{
   
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        [Route("login", Name = "account.login")]
        public ActionResult Login()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public ActionResult Login(Login login)
        {
            TryValidateModel(login);

            
            if (ModelState.IsValid)
            {
                Customer customer = BussinessManager.validatecustomer(login.Username.Trim(), login.Password.Trim());
                if (customer != null)
                {
                    this.Session.Add("user", customer);
                    this.Session.Add("books", BussinessManager.Getbook());
                    string query = Request.QueryString["to"];

                    if ( query != null)
                    {
                        //TODO: Primary Basis
                        return this.RedirectToRoute(Request.Url.Authority + query);
                    }
                    else
                    {
                        return this.RedirectToAction("Index", "Posts");
                    }
                }
            }



            return View();
        }

        [HttpGet]
        [Route("logout")] 
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToRoute("account.login");
        }

        [HttpGet]
        [Route("register")]
        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("register")]
        public ActionResult Register(Register register)
        {
            TryValidateModel(register);


            if (ModelState.IsValid)
            {
                Customer cust = new Customer
                {
                    //customerid = uid,
                    customer_name = register.Username,
                    email = register.Email,
                    password = register.Password

                };
                bool status = BussinessManager.register(cust);

                if (status)
                {
                    Utils.Mailing.Send("we@bookter.in", register.Email, "Welcome " + register.Username + " !", "<!DOCTYPE html><html><head><title>Welcome</title><link rel='stylesheet' href='css/font-awesome.css'><link rel='stylesheet' href='css/style.css'><link href='https://fonts.googleapis.com/css2?family=Miriam+Libre:wght@400;700&display=swap' rel='stylesheet'></head><body><div class='container' style='width: 100%;height: 100vh;min-height: 100vh;'><h1 class='p-y-medium' style='font-family: Nunito;;padding-top: 0.75rem;padding-bottom: 0.75rem;'>Hello " + register.Username + "</h1><p class='p-y-medium' style='font-family: Nunito;;padding-top: 0.75rem;padding-bottom: 0.75rem;'>Wecome user.</p><p class='p-y-medium' style='font-family: Nunito;;padding-top: 0.75rem;padding-bottom: 0.75rem;'>Feel home here.</p><p class='m-y-wide' style='font-family: Nunito;;margin-top: 1rem;margin-bottom: 1rem;'><strong>Have a nice day!</strong></p></div></body></html>");
                    return this.RedirectToAction("login","account"); ;
                }
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
                string url = "http://localhost:7777/restore?token=" + str;
                Utils.Mailing.Send("we@bookter.in", uemail, "Regarding password reset", "<!DOCTYPE html><html><head><title>Password Reset</title><link rel='stylesheet' href='css/font-awesome.css'><link rel='stylesheet' href='css/style.css'><link href='https://fonts.googleapis.com/css2?family=Miriam+Libre:wght@400;700&display=swap' rel='stylesheet'></head><body><div class='container' style='width: 100%;height: 100vh;min-height: 100vh;'><h1 class='p-y-medium' style='font-family: Nunito;;padding-top: 0.75rem;padding-bottom: 0.75rem;'>Hello User</h1><p class='p-y-medium' style='font-family: Nunito;;padding-top: 0.75rem;padding-bottom: 0.75rem;'>You just requested for your password reset.</p><p class='p-y-medium' style='font-family: Nunito;;padding-top: 0.75rem;padding-bottom: 0.75rem;'><a href='" + url +"'>Here's</a> the link if the button did not word.</p><p class='m-y-wide' style='font-family: Nunito;;margin-top: 1rem;margin-bottom: 1rem;'><strong>Thank you!</strong></p></div></body></html>");

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
                string email = BussinessManager.checkToken(token);
                //string token = Request.QueryString["token"];
                if (email != "")
                {
                    this.ViewData["email"] = email;
                }
            }
            return this.RedirectToRoute("account.login");
        }

        [HttpGet]
        [Route("reset", Name = "account.reset")]
        public ActionResult ResetForm()
        {
            //if (Request.QueryString["token"] != null)
            //{
            //    //string token = Request.QueryString["token"];
            //    if (BussinessManager.checkToken(token))
            //    {
            //        return this.RedirectToRoute("setpass");
            //    }
            //}
            return View();
        }

        [HttpPost]
        [Route("reset")]
        public ActionResult Reset(string password, string password2, string email)
        {
            if (password != password2)
            {
                return View();
            }

            Customer customer = (Customer) this.Session["user"];

            bool status = BussinessManager.updatepass(password, email);

            if (status)
            {
                return this.RedirectToAction("login", "account"); ;
            }
            return this.RedirectToRoute("account.login");
        }


    }
}