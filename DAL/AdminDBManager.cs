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
    }
}
