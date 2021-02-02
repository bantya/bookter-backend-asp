using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class Order
    {
        public int order_id {set;get;}
        public int user_id {set;get; }
        public string order_token { set; get; }
        public double amount { set; get; }
        public string added_on { set; get; }
        public int total_items { set; get; }
        public int book_id { set; get; }
        public string book_name { set; get; }
        public string book_author { set; get; }
        public string image { set; get; }
    }
}
