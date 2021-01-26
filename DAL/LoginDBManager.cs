using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using BOL;

namespace DAL
{
    public class LoginDBManager
    {
        public static readonly string connString = string.Empty;

        static LoginDBManager()
        {
            connString = ConfigurationManager.ConnectionStrings["dbString"].ConnectionString;
        }

        public static bool register(Customer cust)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {


                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "INSERT INTO customers (customerid,customer_name,email,password) VALUES(default,@uname,@email,@password)";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    //cmd.Parameters.Add(new MySqlParameter("@uid", cust.customerid));
                    cmd.Parameters.Add(new MySqlParameter("@uname", cust.customer_name));
                    cmd.Parameters.Add(new MySqlParameter("@email", cust.email));
                    cmd.Parameters.Add(new MySqlParameter("@password", cust.password));
                  

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

        public static bool checkToken(string token)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "SELECT token FROM forgotpass where token = @token";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@token", token));


                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        status = true;

                        // CHECK if the token is time valid
                            // IF NO then throw exception
                            // IF YES then go ahead

                        // DELETE The token from forgotpass table.
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

        public static bool forgotpasse(forgotpass newfor)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {


                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "INSERT INTO forgotpass (email,token,created_on) VALUES(@email,@token,default)";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@email", newfor.email));
                    cmd.Parameters.Add(new MySqlParameter("@token", newfor.token));
                  


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

        public static bool validateforgot(string email)
        {

            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "SELECT email FROM customers where email = @uname ";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@uname", email));
                    

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

        public static Customer validatelogin(string uname, string upass)
        {
            Customer customer = null;
            
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "select * from customers where password = @upass and email = @uname or customer_name = @uname";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@uname", uname));
                    cmd.Parameters.Add(new MySqlParameter("@upass", upass));

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        while(reader.Read())
                        {
                            int id = int.Parse(reader["customerID"].ToString());
                            string Name = reader["customer_name"].ToString();
                            string Email = reader["email"].ToString();
                            string Image = reader["image"].ToString();
                            string banner = reader["banner"].ToString();
                            string f_name = reader["First_name"].ToString();
                            string l_name = reader["Last_name"].ToString();


                            customer = new Customer { email = Email, customerid = id, customer_name = Name , image = Image ,banner = banner,f_name = f_name,l_name = l_name};
                        }
                    }
                    con.Close();


                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }

            return customer;

        }

        public static Customer getUserByUsername(string uname)
        {
            Customer customer = new Customer { customer_name = "error404", f_name = "Not", l_name = "Found", banner = "/Images/Users/banner-e.png", image = "/Images/Users/error.jpg" };

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "select * from customers where customer_name = @uname";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@uname", uname));

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            int id = int.Parse(reader["customerID"].ToString());
                            string Name = reader["customer_name"].ToString();
                            string Email = reader["email"].ToString();
                            string Image = reader["image"].ToString();
                            string banner = reader["banner"].ToString();
                            string f_name = reader["First_name"].ToString();
                            string l_name = reader["Last_name"].ToString();

                            customer = new Customer { email = Email, customerid = id, customer_name = Name, image = Image, banner = banner ,f_name=f_name,l_name=l_name};
                        }
                    }
                    con.Close();


                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }

            return customer;

        }

    }
}
