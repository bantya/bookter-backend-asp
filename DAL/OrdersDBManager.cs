using BOL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrdersDBManager
    {
        public static readonly string connString = string.Empty;

        static OrdersDBManager()
        {
            connString = ConfigurationManager.ConnectionStrings["dbString"].ConnectionString;
        }
        public static void createOrder(int user_id, int book_id, int qtt, double total, string token)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {


                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "INSERT INTO orders (order_id,user_id,featured,items, amount, order_token, added_on) VALUES(default,@user,@book,@items,@total,@token,default)";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@user", user_id));
                    cmd.Parameters.Add(new MySqlParameter("@book", book_id));
                    cmd.Parameters.Add(new MySqlParameter("@items", qtt));
                    cmd.Parameters.Add(new MySqlParameter("@total", total));
                    cmd.Parameters.Add(new MySqlParameter("@token", token));


                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }
        }

        public static List<OrderBooks> getOrderBooks(string token, int customerid)
        {     List<OrderBooks> books = new List<OrderBooks>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = " SELECT o.order_id,b.booksID,b.bookname,b.bookauthor,b.image1, b.rating, b.paperprice,o.quantity,os.added_on FROM orderr o JOIN orders os ON os.order_id = o.order_id JOIN books b ON b.booksID = o.book_id WHERE os.order_token = @order and os.user_id = @user";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@order", token));
                    cmd.Parameters.Add(new MySqlParameter("@user", customerid));

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderBooks book = new OrderBooks();

                            book.order_id = int.Parse(reader["order_id"].ToString());
                            book.booksID = int.Parse(reader["booksID"].ToString());
                            book.book_name = reader["bookname"].ToString();
                            book.book_author = reader["bookauthor"].ToString();
                            book.image = reader["image1"].ToString();
                            book.rating = double.Parse(reader["rating"].ToString());
                            book.price = double.Parse(reader["paperprice"].ToString());
                            book.quantity = int.Parse(reader["quantity"].ToString());
                            book.added_on = reader["added_on"].ToString();

                            books.Add(book);
                        }
                    }
                    con.Close();
                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }
            return books;
        }

        public static List<Order> getOrders(int cus_id)
        {
            List<Order> orders = new List<Order>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "SELECT o.order_id, o.user_id, o.items, o.order_token, o.amount, o.added_on, b.booksID, b.bookname, b.bookauthor, b.image1 FROM orders o JOIN books b ON o.featured = b.booksID WHERE user_id = @user order by added_on desc";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@user", cus_id));

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order order = new Order();

                            order.order_id = int.Parse(reader["order_id"].ToString());
                            order.user_id = int.Parse(reader["user_id"].ToString());
                            order.total_items = int.Parse(reader["items"].ToString());
                            order.order_token = reader["order_token"].ToString();
                            order.added_on = reader["added_on"].ToString();
                            order.amount = double.Parse(reader["amount"].ToString());
                            order.book_id = int.Parse(reader["booksID"].ToString());
                            order.book_name = reader["bookname"].ToString();
                            order.book_author = reader["bookauthor"].ToString();
                            order.image = reader["image1"].ToString();

                            orders.Add(order);
                        }
                    }
                    con.Close();
                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }
            return orders;
        }

        public static int getLatestOrderId(int customerid)
        {
            int order = 0;
            IDbConnection conn = new MySqlConnection();
            conn.ConnectionString = connString;
            string query = "select order_id from orders where user_id= " + customerid + " order BY added_on desc LIMIT 1";
            IDbCommand cmd = new MySqlCommand();
            cmd.CommandText = query;
            cmd.Connection = conn;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd as MySqlCommand);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                DataRowCollection rows = ds.Tables[0].Rows;
                foreach (DataRow row in rows)
                {
                    order = int.Parse(row["order_id"].ToString());


                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }
            // implementation 
            return order;
        }

        public static bool addBook(int ord_i, object book_id, object quan)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "INSERT INTO orderr (order_id,book_id,quantity) VALUES (@oid,@bid,@quan)";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@oid", ord_i));
                    cmd.Parameters.Add(new MySqlParameter("@bid", book_id));
                    cmd.Parameters.Add(new MySqlParameter("@quan", quan));


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

    }
}
