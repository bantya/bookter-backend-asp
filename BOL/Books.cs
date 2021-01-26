using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class Books
    {
        public int booksID { get; set; }

        public string bookname { get; set; }
        public string bookauthor { get; set; }

        public int bookspage { get; set; }

        public string booklang { get; set; }
        public string bookpublisher { get; set; }

        public string bookdate { get; set; }

        public string bookdimension { get; set; }

        public double paperprice { get; set; }
        public double hardprice { get; set; }
        public double ebookprice { get; set; }

        public string bookdisc { get; set; }

        public string aboutauthor { get; set; }

        public double rating { get; set; }

        public string image { get; set; }

        public string image2 { get; set; }

        public string image3 { get; set; }

        public int status { get; set; }



        public Books()
        {

        }

        public Books(int book_id,string book_name,string book_author,string publish_date,string publisher,double pbp,string imag)
        {
            booksID = book_id;
            bookname = book_name;
            bookauthor = book_author;
            bookdate = publish_date;
            bookpublisher = publisher;
            paperprice = pbp;
            image = imag;
        }
    }
}
