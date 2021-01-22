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
    public class LikeDBManager
    {
        public static readonly string connString = string.Empty;

        static LikeDBManager()
        {

            connString = ConfigurationManager.ConnectionStrings["dbString"].ConnectionString;
        }
        public static bool AddLike(int customerid, int id)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {


                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    // post_id | user_id | status
                    string query = "INSERT INTO likes (post_id,user_id,status) VALUES(@id , @customerid,1)";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@id", id));
                    cmd.Parameters.Add(new MySqlParameter("@customerid", customerid));
                    //cmd.Parameters.Add(new MySqlParameter("@postcontent", postcontent));
                    



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

        public static List<Likes> GetallLikes(int custid)
        {

            List<Likes> allikes = new List<Likes>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    
                    string query = "SELECT post_id, status FROM likes WHERE user_id = @userid;";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@userid", custid));

                    //Wishlist wlist = new Wishlist();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //bookIds.Add(int.Parse(reader["book_id"].ToString()));
                            Likes like = new Likes();
                            like.post_id = int.Parse(reader["post_id"].ToString());
                            like.status = int.Parse(reader["status"].ToString());

                            allikes.Add(like);
                        }
                       
                    }

                    
                    con.Close();
                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }
            return allikes;

        }

        public static bool AddDislike(int customerid, int id)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {


                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    // post_id | user_id | status
                    string query = "INSERT INTO likes (post_id,user_id,status) VALUES(@id , @customerid,0)";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@id", id));
                    cmd.Parameters.Add(new MySqlParameter("@customerid", customerid));
                    //cmd.Parameters.Add(new MySqlParameter("@postcontent", postcontent));




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
