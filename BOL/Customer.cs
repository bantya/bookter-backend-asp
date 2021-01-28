using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;



namespace BOL
{
    public class Customer
    {
        public int customerid { get; set; }
        
        [Required]
        public string customer_name { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        public string banner { get; set; }

        public string f_name { get; set; }
        public string l_name { get; set; }

        public string image { get; set; }
        public Customer() { }

        public Customer(int custId, string custName, string custtEmail,string imag,string ban)
        {
            this.customerid = custId;
            this.customer_name = custName;
            this.email = custtEmail;
            this.image = imag;
            this.banner = ban;
        }


    }
}

