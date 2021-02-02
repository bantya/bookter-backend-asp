using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utils;

namespace BookManagment.Controllers
{
    public class PayController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        [Route("pay/now/")]
        public ActionResult Now(Models.PaymentInitiateModel requestData)
        {
            string transactionId = Randoms.Pattern("trans-{HEX:15}");

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_DmK2S2Y4ATkGZz", "glz95rTDL8OE4kV3RsUo4UAj");

            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", int.Parse(requestData.amount) * 100);
            options.Add("receipt", transactionId);
            options.Add("currency", "INR");
            options.Add("payment_capture", "0");
            //options.Add("notes", "Good to go");

            Razorpay.Api.Order orderResponse = client.Order.Create(options);
            string orderId = orderResponse["id"].ToString();

            // Create order model for return on view
            OrderModel orderModel = new OrderModel
            {
                orderId = orderResponse.Attributes["id"],
                razorpayKey = "rzp_test_DmK2S2Y4ATkGZz",
                amount = int.Parse(requestData.amount) * 100,
                currency = "INR",
                name = requestData.name,
                email = requestData.email,
                contactNumber = requestData.contact,
                address = requestData.address,
                description = "Testing description"
            };

            // Return on PaymentPage with Order data
            return View("Payment", orderModel);
        }

        [HttpPost]
        public ActionResult Complete()
        {
            // Payment data comes in url so we have to get it from url

            // This id is razorpay unique payment id which can be use to get the payment details from razorpay server
            string paymentId = Request.Params["rzp_paymentid"];

            // This is orderId
            string orderId = Request.Params["rzp_orderid"];

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_DmK2S2Y4ATkGZz", "glz95rTDL8OE4kV3RsUo4UAj");

            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);

            // This code is for capture the payment 
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
            Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];

            //// Check payment made successfully

            if (paymentCaptured.Attributes["status"] == "captured")
            {
                // Create these action method
                return RedirectToAction("Success");
            }
            else
            {
                return RedirectToAction("Failed");
            }
        }

        [HttpPost]
        public ActionResult Success()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Failed()
        {
            return View();
        }

        public class OrderModel
        {
            public string orderId { get; set; }
            public string razorpayKey { get; set; }
            public int amount { get; set; }
            public string currency { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string contactNumber { get; set; }
            public string address { get; set; }
            public string description { get; set; }
        }
    }
}