using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
   // post_id | content    | post_image | book_id | created_at    | bookname    | bookauthor
    public class Posts
    {
        public int post_id { get; set; }

        public string user_name { get; set; }

        public string content { get; set; }

        public string post_image1 { get; set; }

        public string post_image2 { get; set; }
        public string post_image3 { get; set; }
        public string post_image4 { get; set; }

        public int book_id { get; set; }

        public string created_at { get; set; }

        public string bookname { get; set; }
        public string bookauthor { get; set; }

      
    }
}

