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
    public class WishlistDBManager
    {
        public static readonly string connString = string.Empty;

        static WishlistDBManager()
        {
            connString = ConfigurationManager.ConnectionStrings["dbString"].ConnectionString;
        }

        public static bool AddWishlist(int cid, int bid)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {


                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "INSERT INTO wishlist (customer_id,book_id,added_on) VALUES(@cid,@bid,default)";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    //cmd.Parameters.Add(new MySqlParameter("@uid", cust.customerid));
                    cmd.Parameters.Add(new MySqlParameter("@cid", cid));
                    cmd.Parameters.Add(new MySqlParameter("@bid", bid));
                    


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

        public static bool deletewishlist(int cid, int bid)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "DELETE from wishlist WHERE customer_id = @cid and book_id = @bid";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@cid", cid));
                    cmd.Parameters.Add(new MySqlParameter("@bid", bid));

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

        public static List<Books> GetWishList(int cid)
        {
            List<Books> wishlists = new List<Books>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    List<int> bookIds = new List<int>();
                    string query = "select booksID, bookname, bookauthor, publishdate, bookpublisher, paperprice, rating, image from books where booksID in (SELECT book_id FROM wishlist where customer_id = @cid)";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@cid", cid));

                    //Wishlist wlist = new Wishlist();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //bookIds.Add(int.Parse(reader["book_id"].ToString()));
                            Books theproducts = new Books();
                            theproducts.booksID = int.Parse(reader["booksID"].ToString());
                            theproducts.bookname = reader["bookname"].ToString();
                            theproducts.bookauthor = reader["bookauthor"].ToString();

                            theproducts.bookdate = reader["publishdate"].ToString();
                            theproducts.bookpublisher = reader["bookpublisher"].ToString();
                            theproducts.paperprice = double.Parse(reader["paperprice"].ToString());

                            theproducts.rating = double.Parse(reader["rating"].ToString());
                            theproducts.image = reader["image"].ToString();
                            wishlists.Add(theproducts);
                        }
                    }

                    con.Close();
                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }
            return wishlists;
        }
    }
}
