using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class Posts
    {
        public int post_id { set; get; }
        public int user_id { set; get; }
        public int book_id { set; get; }
        public int booksID { set; get; }
        public string user_name { set; get; }
        public string content { set; get; }
        public string created_at { set; get; }
        public string post_image_1 { set; get; }
        public string post_image_2 { set; get; }
        public string post_image_3 { set; get; }
        public string post_image_4 { set; get; }
        public int likes { set; get; }
        public int dislikes { set; get; }
        public string image { set; get; }
        public string bookname { set; get; }
        public string bookauthor { set; get; }

        public string f_name { set; get; }
        public string l_name { set; get; }
        public bool isLiked { set; get; }
        public bool isDisLiked { set; get; }

        public List<Comment> comments { set; get; }
    }
}

