using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookManagment.Models
{
    public class PaymentInitiateModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public string address { get; set; }
        public string amount { get; set; }
    }
}