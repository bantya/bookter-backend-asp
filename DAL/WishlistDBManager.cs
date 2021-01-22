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
                        //DataTable dt = new DataTable();
                        //dt.Load(reader);
                        //int totalRecords = dt.Rows.Count;
                        //foreach (DataRow dr in dt.Rows)
                        //{
                        //    bookIds.Add(int.Parse(dr["book_id"].ToString()));
                        //  //  wlist.timestamp = dr["added_on"].ToString();
                        //}
                    }

                    //MySqlCommand cmd2 = new MySqlCommand();
                    //cmd2.Connection = con;
                    //cmd2.CommandType = CommandType.Text;

                    //List<string> paramsList = new List<string>();

                    //// string ids = string.Join(",", bookIds);
                    //string query2 = "SELECT booksID, bookname, bookauthor, publishdate, bookpublisher, paperprice, rating, image  FROM books where booksID IN({0})";
                    //int index = 0;
                    //foreach (int value in bookIds)
                    //{
                    //    string param = "@in" + index;
                    //    cmd2.Parameters.AddWithValue(param, value);
                    //    paramsList.Add(param);
                    //    index++;
                    //}

                    //cmd2.CommandText = string.Format(query2, string.Join(",", paramsList));

                    //using (MySqlDataReader reader = cmd2.ExecuteReader())
                    //{
                    //    DataTable dt = new DataTable();
                    //    dt.Load(reader);
                    //    int totalRecords = dt.Rows.Count;
                    //    foreach (DataRow dr in dt.Rows)
                    //    {

                    //        Books theproducts = new Books();
                    //        theproducts.booksID = int.Parse(dr["booksID"].ToString());
                    //        theproducts.bookname = dr["bookname"].ToString();
                    //        theproducts.bookauthor = dr["bookauthor"].ToString();

                    //        theproducts.bookdate = dr["publishdate"].ToString();
                    //        theproducts.bookpublisher = dr["bookpublisher"].ToString();
                    //        theproducts.paperprice = double.Parse(dr["paperprice"].ToString());

                    //        theproducts.rating = double.Parse(dr["rating"].ToString());
                    //        theproducts.image = dr["image"].ToString();
                    //        wishlists.Add(theproducts);
                    //    }

                    //}
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
