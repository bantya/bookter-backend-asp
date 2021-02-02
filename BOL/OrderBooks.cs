using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class OrderBooks
    {
        public int order_id { set; get; }
        public int booksID { set; get; }
        public string book_name { set; get; }
        public string book_author { set; get; }
        public string image { set; get; }
        public double rating { set; get; }
        public double price { set; get; }
        public int quantity { set; get; }
        public string added_on { set; get; }
    }
}


