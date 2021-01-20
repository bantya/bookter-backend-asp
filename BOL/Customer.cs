using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class Customer
    {
        public int customerid { get; set; }
        public string customer_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public string image { get; set; }
        public Customer() { }

        public Customer(int custId, string custName, string custtEmail,string imag)
        {
            this.customerid = custId;
            this.customer_name = custName;
            this.email = custtEmail;
            this.image = imag;
        }


    }
}

