using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using BOL;

namespace DAL
{
    public class AdminDBManager
    {
        public static readonly string connString = string.Empty;

        static AdminDBManager()
        {
            connString = ConfigurationManager.ConnectionStrings["dbString"].ConnectionString;
        }
        public static bool AdminLogin(string uname, string upass)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "SELECT email , password FROM admin where email = @uname and password = @upass";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@uname", uname));
                    cmd.Parameters.Add(new MySqlParameter("@upass", upass));

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        status = true;
                    }
                    con.Close();


                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }

            return status;
        }

        public static bool AdminRegistrartion(Admin newadmin)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {


                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "INSERT INTO admin (adminid,fname,lname,email,contact,password) VALUES(default,@fname,@lname,@email,@contact,@password)";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    //cmd.Parameters.Add(new MySqlParameter("@adminid", newadmin.adminid));
                    cmd.Parameters.Add(new MySqlParameter("@fname", newadmin.fname));
                    cmd.Parameters.Add(new MySqlParameter("@lname", newadmin.lname));
                    cmd.Parameters.Add(new MySqlParameter("@email", newadmin.email));
                    cmd.Parameters.Add(new MySqlParameter("@contact", newadmin.contact));

                    cmd.Parameters.Add(new MySqlParameter("@password", newadmin.password));


                    cmd.ExecuteNonQuery();
                    con.Close();
                    status = true;
                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }

            return status;

        }

        public static bool UpdateBook(Books newbook)
        {
            bool status = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {


                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    //(booksID, bookname, bookauthor, bookpage, booklang, bookpublisher, publishdate, bookdimension, paperprice, hardprice, ebookprice, bookdisc, aboutauthor, rating, image, image2, image3) VALUES" +
                    //                                    "(default,@bname,@aname,@bpages,@blang,@bpub,@bdate,@bdimen,@bpp,@bhp,@ebp,@bdisc,@aathor,@brat,@bimag,@bimag2,@bimag3)";

                    string query = "update books set booksID = @bid, bookname = @bname , bookauthor = @aname , bookpage = @bpages , booklang = @blang ,bookpublisher = @bpub ,publishdate = @bdate, bookdimension = @bdimen , paperprice = @bpp , hardprice = @bhp , ebookprice = @ebp ,bookdisc = @bdisc ,aboutauthor = @aathor , rating = @brat , image = @bimag ,image2 = @bimag2 ,image3 = @bimag3 where booksID=@bid";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@bid", newbook.booksID));
                    cmd.Parameters.Add(new MySqlParameter("@bname", newbook.bookname));
                    cmd.Parameters.Add(new MySqlParameter("@aname", newbook.bookauthor));
                    cmd.Parameters.Add(new MySqlParameter("@bpages", newbook.bookspage));
                    cmd.Parameters.Add(new MySqlParameter("@blang", newbook.booklang));
                    cmd.Parameters.Add(new MySqlParameter("@bpub", newbook.bookpublisher));
                    cmd.Parameters.Add(new MySqlParameter("@bdate", newbook.bookdate));
                    cmd.Parameters.Add(new MySqlParameter("@bdimen", newbook.bookdimension));
                    cmd.Parameters.Add(new MySqlParameter("@bpp", newbook.paperprice));
                    cmd.Parameters.Add(new MySqlParameter("@bhp", newbook.hardprice));
                    cmd.Parameters.Add(new MySqlParameter("@ebp", newbook.ebookprice));
                    cmd.Parameters.Add(new MySqlParameter("@bdisc", newbook.bookdisc));
                    cmd.Parameters.Add(new MySqlParameter("@aathor", newbook.aboutauthor));
                    cmd.Parameters.Add(new MySqlParameter("@brat", newbook.rating));
                    cmd.Parameters.Add(new MySqlParameter("@bimag", newbook.image));
                    cmd.Parameters.Add(new MySqlParameter("@bimag2", newbook.image2));
                    cmd.Parameters.Add(new MySqlParameter("@bimag3", newbook.image3));

                    cmd.ExecuteNonQuery();
                    con.Close();
                    status = true;
                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }

            return status;

        }

        public static bool CancelArchive(int booksID)
        {
            bool status = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {


                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    //(booksID, bookname, bookauthor, bookpage, booklang, bookpublisher, publishdate, bookdimension, paperprice, hardprice, ebookprice, bookdisc, aboutauthor, rating, image, image2, image3) VALUES" +
                    //                                    "(default,@bname,@aname,@bpages,@blang,@bpub,@bdate,@bdimen,@bpp,@bhp,@ebp,@bdisc,@aathor,@brat,@bimag,@bimag2,@bimag3)";

                    string query = "update books set  status = 1  where booksID=@bid";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@bid", booksID));




                    cmd.ExecuteNonQuery();
                    con.Close();
                    status = true;
                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }

            return status;
        }

        public static bool ArchiveBook(int booksID)
        {

            bool status = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {


                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    //(booksID, bookname, bookauthor, bookpage, booklang, bookpublisher, publishdate, bookdimension, paperprice, hardprice, ebookprice, bookdisc, aboutauthor, rating, image, image2, image3) VALUES" +
                    //                                    "(default,@bname,@aname,@bpages,@blang,@bpub,@bdate,@bdimen,@bpp,@bhp,@ebp,@bdisc,@aathor,@brat,@bimag,@bimag2,@bimag3)";

                    string query = "update books set  status = 0  where booksID=@bid";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@bid", booksID));

                 


                    cmd.ExecuteNonQuery();
                    con.Close();
                    status = true;
                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }

            return status;

        }

        public static bool UpdateBookbyPrice(int bid, double bpp, double bhp, double ebp)
        {
            bool status = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {


                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    //(booksID, bookname, bookauthor, bookpage, booklang, bookpublisher, publishdate, bookdimension, paperprice, hardprice, ebookprice, bookdisc, aboutauthor, rating, image, image2, image3) VALUES" +
                    //                                    "(default,@bname,@aname,@bpages,@blang,@bpub,@bdate,@bdimen,@bpp,@bhp,@ebp,@bdisc,@aathor,@brat,@bimag,@bimag2,@bimag3)";

                    string query = "update books set  paperprice = @bpp , hardprice = @bhp , ebookprice = @ebp where booksID=@bid";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@bid", bid));
                    
                    cmd.Parameters.Add(new MySqlParameter("@bpp", bpp));
                    cmd.Parameters.Add(new MySqlParameter("@bhp", bhp));
                    cmd.Parameters.Add(new MySqlParameter("@ebp", ebp));
           

                    cmd.ExecuteNonQuery();
                    con.Close();
                    status = true;
                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }

            return status;

        }

        public static bool UpdateBookImages(List<string> images, int book_id)
        {
            bool status = false;
            List<string> updates = new List<string>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "Update books SET ";

                    for (int i = 0; i < images.Count; i++)
                    {
                        updates.Add($"image{i + 1}=@image{i + 1}");
                    }

                    query += String.Join(",", updates.ToArray());
                    query += " WHERE booksID = @book";

                    MySqlCommand cmd = new MySqlCommand(query, con);

                    for (int i = 0; i < images.Count; i++)
                    {
                        cmd.Parameters.Add(new MySqlParameter($"@image{i + 1}", images[i]));
                    }

                    cmd.Parameters.Add(new MySqlParameter("@book", book_id));


                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        status = true;
                    }
                    con.Close();


                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }

            return status;
        }
    }
}
