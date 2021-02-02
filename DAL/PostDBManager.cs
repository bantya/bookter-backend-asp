using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using BOL;
using Utils;
namespace DAL
{
    public class PostDBManager
    {
        public static readonly string connString = string.Empty;

        static PostDBManager()
        {
            connString = ConfigurationManager.ConnectionStrings["dbString"].ConnectionString;
        }

        public static int insertpost(int customerid, string postcontent, int postbooks, string customer_name)
        {
            int post_id = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "INSERT INTO posts (post_id,user_id,content,user_name,post_image_1,post_image_2,post_image_3,post_image_4,book_id,created_at) VALUES(default,@customerid,@postcontent,@username,NULL,NULL,NULL,NULL,@postbooks,default); SELECT LAST_INSERT_ID()";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    //cmd.Parameters.Add(new MySqlParameter("@uid", cust.customerid));
                    cmd.Parameters.Add(new MySqlParameter("@customerid", customerid));
                    cmd.Parameters.Add(new MySqlParameter("@postcontent", postcontent));
                    cmd.Parameters.Add(new MySqlParameter("@postbooks", postbooks));
                    cmd.Parameters.Add(new MySqlParameter("@username", customer_name));

                    post_id = Convert.ToInt32(cmd.ExecuteScalar());

                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
                post_id = -1;
            }

            return post_id;

        }

        public static bool UpdatePostImages(List<string> images, int post_id)
        {
            bool status = false;
            List<string> updates = new List<string>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "Update posts SET "; 
                    
                    for(int i = 0; i < images.Count; i++)
                    {
                        updates.Add($"post_image_{i + 1}=@image_{i + 1}");
                    }

                    query += String.Join(",", updates.ToArray());
                    query += " WHERE post_id = @post";

                    MySqlCommand cmd = new MySqlCommand(query, con);

                    for (int i = 0; i < images.Count; i++)
                    {
                        cmd.Parameters.Add(new MySqlParameter($"@image_{i + 1}", images[i]));
                    }

                    cmd.Parameters.Add(new MySqlParameter("@post", post_id));


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

        public static  Followers CheckFollow(string v, int customerid)
        {
            Followers follow = null;

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "select * from followers where follower_id = @cust and following_name = @v";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@cust", customerid));
                    cmd.Parameters.Add(new MySqlParameter("@v", v));

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        //| follower_id | following_id | following_name |
                        while (reader.Read())
                        {
                            int f_id = int.Parse(reader["follower_id"].ToString());
                            int fo_id = int.Parse(reader["following_id"].ToString());

                            string Name = reader["following_name"].ToString();
                            

                            follow = new Followers { follower_id = f_id, following_id = fo_id, following_name = Name};
                        }
                    }
                    con.Close();


                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }

            return follow;
        }

        public static List<Posts> GetallUsersPosts(int customerid)
        {

            List<Posts> posts = new List<Posts>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    // string query = "select p.post_id ,p.content ,p.post_image_1,p.post_image_2,p.post_image_3,p.post_image_4 ,p.book_id ,p.created_at ,b.bookname , b.bookauthor from posts p join books b on p.book_id = b.booksid where p.user_id = @customerid";
                    string query = "SELECT p.post_id, p.user_id, p.user_name, p.content, p.book_id, p.created_at, p.post_image_1, p.post_image_2, p.post_image_3, p.post_image_4, s.likes, s.dislikes, c.image,c.First_name,c.Last_Name, b.booksID, b.bookname, b.bookauthor FROM posts p LEFT JOIN (SELECT post_id, COUNT(IF(STATUS = 1, 1, NULL)) as likes, COUNT(IF(STATUS = 0, 1, NULL)) as dislikes FROM likes GROUP BY post_id) s ON p.post_id = s.post_id JOIN customers c ON c.customerID = p.user_id JOIN books b ON b.booksID = p.book_id where p.user_id = @custID order by p.created_at desc";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                      cmd.Parameters.Add(new MySqlParameter("@custID", customerid));

                    //Wishlist wlist = new Wishlist();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //bookIds.Add(int.Parse(reader["book_id"].ToString()));
                            Posts post = new Posts();
                            post.post_id = int.Parse(reader["post_id"].ToString());
                            post.user_id = int.Parse(reader["user_id"].ToString());
                            post.book_id = int.Parse(reader["book_id"].ToString());
                            post.booksID = int.Parse(reader["booksID"].ToString());
                            post.user_name = reader["user_name"].ToString();
                            post.content = reader["content"].ToString();
                            post.created_at = reader["created_at"].ToString();
                            post.post_image_1 = reader["post_image_1"].ToString();
                            post.post_image_2 = reader["post_image_2"].ToString();
                            post.post_image_3 = reader["post_image_3"].ToString();
                            post.post_image_4 = reader["post_image_4"].ToString();
                            post.likes = Utils.Utils.ParseNullableInt(reader["likes"].ToString());
                            post.dislikes = Utils.Utils.ParseNullableInt(reader["dislikes"].ToString());
                            
                            post.image = reader["image"].ToString();
                            post.bookname = reader["bookname"].ToString();
                            post.bookauthor = reader["bookauthor"].ToString();
                            post.f_name = reader["First_name"].ToString();
                            post.l_name = reader["Last_name"].ToString();

                            post.comments = GetCommentByPosts(post.post_id);

                            posts.Add(post);
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

        //select * from followers where follower_id = 1 and following_name = "bantya";
        public static List<Posts> GetallPostsByUsername(string username)
        {
            List<Posts> posts = new List<Posts>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    // string query = "select p.post_id ,p.content ,p.post_image_1,p.post_image_2,p.post_image_3,p.post_image_4 ,p.book_id ,p.created_at ,b.bookname , b.bookauthor from posts p join books b on p.book_id = b.booksid where p.user_id = @customerid";
                    string query = "SELECT p.post_id, p.user_id, p.user_name, p.content, p.book_id, p.created_at, p.post_image_1, p.post_image_2, p.post_image_3, p.post_image_4, s.likes, s.dislikes, c.image,c.First_name,c.Last_Name, b.booksID, b.bookname, b.bookauthor FROM posts p LEFT JOIN (SELECT post_id, COUNT(IF(STATUS = 1, 1, NULL)) as likes, COUNT(IF(STATUS = 0, 1, NULL)) as dislikes FROM likes GROUP BY post_id) s ON p.post_id = s.post_id JOIN customers c ON c.customerID = p.user_id JOIN books b ON b.booksID = p.book_id where p.user_name = @username order by p.created_at desc";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                      cmd.Parameters.Add(new MySqlParameter("@username", username));

                    //Wishlist wlist = new Wishlist();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //bookIds.Add(int.Parse(reader["book_id"].ToString()));
                            Posts post = new Posts();
                            post.post_id = int.Parse(reader["post_id"].ToString());
                            post.user_id = int.Parse(reader["user_id"].ToString());
                            post.book_id = int.Parse(reader["book_id"].ToString());
                            post.booksID = int.Parse(reader["booksID"].ToString());
                            post.user_name = reader["user_name"].ToString();
                            post.content = reader["content"].ToString();
                            post.created_at = reader["created_at"].ToString();
                            post.post_image_1 = reader["post_image_1"].ToString();
                            post.post_image_2 = reader["post_image_2"].ToString();
                            post.post_image_3 = reader["post_image_3"].ToString();
                            post.post_image_4 = reader["post_image_4"].ToString();
                            post.likes = Utils.Utils.ParseNullableInt(reader["likes"].ToString());
                            post.dislikes = Utils.Utils.ParseNullableInt(reader["dislikes"].ToString());
                            
                            post.image = reader["image"].ToString();
                            post.bookname = reader["bookname"].ToString();
                            post.bookauthor = reader["bookauthor"].ToString();
                            post.f_name = reader["First_name"].ToString();
                            post.l_name = reader["Last_name"].ToString();

                            post.comments = GetCommentByPosts(post.post_id);

                            posts.Add(post);
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

        public static bool insertcomment(int post_id, int customerid, string postcomment, string customer_name, int userid)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    //| comment_id | content     | user_id | user_name |

                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "insert into comments (comment_id,content,post_id,user_id,user_name,account_id,posted_on) values (default,@comment,@post,@user_id,@user_name,@account_id,default);";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    //cmd.Parameters.Add(new MySqlParameter("@uid", cust.customerid));
                    
                    cmd.Parameters.Add(new MySqlParameter("@comment", postcomment));
                    cmd.Parameters.Add(new MySqlParameter("@user_id", customerid));
                    cmd.Parameters.Add(new MySqlParameter("@post", post_id));
                    cmd.Parameters.Add(new MySqlParameter("@user_name", customer_name));
                    cmd.Parameters.Add(new MySqlParameter("@account_id", userid));



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

                    // string query = "select p.post_id ,p.content ,p.post_image_1,p.post_image_2,p.post_image_3,p.post_image_4 ,p.book_id ,p.created_at ,b.bookname , b.bookauthor from posts p join books b on p.book_id = b.booksid where p.user_id = @customerid";
                    string query = "SELECT p.post_id, p.user_id, p.user_name, p.content, p.book_id, p.created_at, p.post_image_1, p.post_image_2, p.post_image_3, p.post_image_4, s.likes, s.dislikes, c.image,c.First_name,c.Last_Name, b.booksID, b.bookname, b.bookauthor FROM posts p LEFT JOIN ( SELECT post_id, COUNT(IF(STATUS = 1, 1, NULL)) as likes, COUNT(IF(STATUS = 0, 1, NULL)) as dislikes FROM likes GROUP BY post_id) s ON p.post_id = s.post_id JOIN customers c ON c.customerID = p.user_id JOIN books b ON b.booksID = p.book_id WHERE p.user_id IN (SELECT following_id FROM followers WHERE follower_id = @userid) OR p.user_id = @userid ORDER BY p.created_at desc";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@userid", customerid));

                    //Wishlist wlist = new Wishlist();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //bookIds.Add(int.Parse(reader["book_id"].ToString()));
                            Posts post = new Posts();
                            post.post_id = int.Parse(reader["post_id"].ToString());
                            post.user_id = int.Parse(reader["user_id"].ToString());
                            post.book_id = int.Parse(reader["book_id"].ToString());
                            post.booksID = int.Parse(reader["booksID"].ToString());
                            post.user_name = reader["user_name"].ToString();
                            post.content = reader["content"].ToString();
                            post.created_at = reader["created_at"].ToString();
                            post.post_image_1 = reader["post_image_1"].ToString();
                            post.post_image_2 = reader["post_image_2"].ToString();
                            post.post_image_3 = reader["post_image_3"].ToString();
                            post.post_image_4 = reader["post_image_4"].ToString();

                            post.likes = Utils.Utils.ParseNullableInt(reader["likes"].ToString());
                            post.dislikes = Utils.Utils.ParseNullableInt(reader["dislikes"].ToString());

                            post.image = reader["image"].ToString();
                            post.bookname = reader["bookname"].ToString();
                            post.bookauthor = reader["bookauthor"].ToString();
                            post.f_name = reader["First_name"].ToString();
                            post.l_name = reader["Last_name"].ToString();

                            post.comments = GetCommentByPosts(post.post_id);

                            posts.Add(post);
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

        public static List<Comment> GetCommentByPosts(int postid)
        {
            List<Comment> comments = new List<Comment>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "SELECT * FROM comments WHERE post_id = @post ORDER BY posted_on desc";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@post", postid));

                    //Wishlist wlist = new Wishlist();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //bookIds.Add(int.Parse(reader["book_id"].ToString()));
                            Comment comment = new Comment();

                            comment.comment_id = int.Parse(reader["comment_id"].ToString());
                            comment.user_id = int.Parse(reader["user_id"].ToString());
                            comment.content = reader["content"].ToString();
                            comment.user_name = reader["user_name"].ToString();
                            comment.accountuser_id = int.Parse(reader["account_id"].ToString());
                            comment.posted_on =  reader["posted_on"].ToString();

                            comments.Add(comment);
                        }
                    }  
                    con.Close();
                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }
            return comments;
        }
    }
}
