using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    // p.post_id, p.user_id, p.user_name, p.content, p.book_id, p.created_at, p.post_image_1, p.post_image_2, p.post_image_3,
    //p.post_image_4, s.likes, s.dislikes, c.image, b.booksID, b.bookname, b.bookauthor 
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


    }
}

