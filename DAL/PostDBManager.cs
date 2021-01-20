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
    public class PostDBManager
    {
        public static readonly string connString = string.Empty;

        static PostDBManager()
        {
            connString = ConfigurationManager.ConnectionStrings["dbString"].ConnectionString;
        }

        public static bool insertpost(int customerid, string postcontent, int postbooks)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {


                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "INSERT INTO posts (post_id,user_id,content,post_image,book_id,created_at) VALUES(default,@customerid,@postcontent,NULL,@postbooks,default)";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    //cmd.Parameters.Add(new MySqlParameter("@uid", cust.customerid));
                    cmd.Parameters.Add(new MySqlParameter("@customerid", customerid));
                    cmd.Parameters.Add(new MySqlParameter("@postcontent", postcontent));
                    cmd.Parameters.Add(new MySqlParameter("@postbooks", postbooks));



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

        public static List<Posts> GetallPostsByUsername(string username)
        {
            List<Posts> posts = new List<Posts>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "select p.post_id ,p.content ,p.post_image_1,p.post_image_2,p.post_image_3,p.post_image_4 ,p.book_id ,p.created_at ,b.bookname , b.bookauthor from posts p join books b on p.book_id = b.booksid where p.user_name = @username";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@username", username));

                    //Wishlist wlist = new Wishlist();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //bookIds.Add(int.Parse(reader["book_id"].ToString()));
                            Posts theproducts = new Posts();
                            theproducts.post_id = int.Parse(reader["post_id"].ToString());
                            theproducts.content = reader["content"].ToString();
                            theproducts.post_image1 = reader["post_image_1"].ToString();
                            theproducts.post_image2 = reader["post_image_2"].ToString();
                            theproducts.post_image3 = reader["post_image_3"].ToString();
                            theproducts.post_image4 = reader["post_image_4"].ToString();


                            theproducts.book_id = int.Parse(reader["book_id"].ToString());
                            theproducts.created_at = reader["created_at"].ToString();
                            theproducts.bookname = reader["bookname"].ToString();
                            theproducts.bookauthor = reader["bookauthor"].ToString();

                            posts.Add(theproducts);
                        }
                    }
                    con.Close();
                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }
            return posts;
        }

        public static bool deleteposts(int id, int customerid)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "DELETE from posts WHERE post_id = @id and user_id = @customerid";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@id", id));
                    cmd.Parameters.Add(new MySqlParameter("@customerid", customerid));

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

        public static List<Posts> GetallPosts(int customerid)
        {
            List<Posts> posts = new List<Posts>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    
                    string query = "select p.post_id ,p.content ,p.post_image_1,p.post_image_2,p.post_image_3,p.post_image_4 ,p.book_id ,p.created_at ,b.bookname , b.bookauthor from posts p join books b on p.book_id = b.booksid where p.user_id = @customerid";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@customerid", customerid));

                    //Wishlist wlist = new Wishlist();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //bookIds.Add(int.Parse(reader["book_id"].ToString()));
                            Posts theproducts = new Posts();
                            theproducts.post_id = int.Parse(reader["post_id"].ToString());
                            theproducts.content = reader["content"].ToString();
                            theproducts.post_image1 = reader["post_image_1"].ToString();
                            theproducts.post_image2 = reader["post_image_2"].ToString();
                            theproducts.post_image3 = reader["post_image_3"].ToString();
                            theproducts.post_image4 = reader["post_image_4"].ToString();


                            theproducts.book_id = int.Parse(reader["book_id"].ToString());
                            theproducts.created_at = reader["created_at"].ToString();
                            theproducts.bookname = reader["bookname"].ToString();
                            theproducts.bookauthor = reader["bookauthor"].ToString();

                            posts.Add(theproducts);
                        }
                    }  
                    con.Close();
                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }
            return posts;
        }
    }
}
