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
    public class SettingDBManager
    {
        public static readonly string connString = string.Empty;

        static SettingDBManager()
        {
            connString = ConfigurationManager.ConnectionStrings["dbString"].ConnectionString;
        }

        public static bool changename(string firstname, string lastname, int customerid)
        {
            bool status = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    //address_id | user_id | flat_no | Area  | Building_no | Street_name | City | District | State       | Pincode |

                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "update customers set First_name = @fname ,Last_name = @lname where customerID = @cid";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@fname", firstname));
                    cmd.Parameters.Add(new MySqlParameter("@lname", lastname));

                    cmd.Parameters.Add(new MySqlParameter("@cid", customerid));


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

        public static bool changeemail(string email, int customerid)
        {
            bool status = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    //address_id | user_id | flat_no | Area  | Building_no | Street_name | City | District | State       | Pincode |

                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "update customers set email = @em where customerID = @cid";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@em", email));
                    cmd.Parameters.Add(new MySqlParameter("@cid", customerid));


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

        public static bool updateAddress(int customerid, string flat_no, string build_no, string area, string street, string city, string district, string state, string pincode)
        {
            bool status = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    //address_id | user_id | flat_no | Area  | Building_no | Street_name | City | District | State       | Pincode |

                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "update address set  flat_no = @flat ,Area = @area ,Building_no = @build ,Street_name = @street , City = @city , District = @dis ,State = @state , Pincode = @pin  where user_id = @uid";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@flat", flat_no));
                    cmd.Parameters.Add(new MySqlParameter("@area", area));
                    cmd.Parameters.Add(new MySqlParameter("@build", build_no));
                    cmd.Parameters.Add(new MySqlParameter("@street", street));
                    cmd.Parameters.Add(new MySqlParameter("@city", city));
                    cmd.Parameters.Add(new MySqlParameter("@dis", district));
                    cmd.Parameters.Add(new MySqlParameter("@state", state));
                    cmd.Parameters.Add(new MySqlParameter("@pin", pincode));

                    cmd.Parameters.Add(new MySqlParameter("@uid", customerid));


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

        public static bool addNewaddress(int customerid, string flat_no, string build_no, string area, string street, string city, string district, string state, string pincode)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {


                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "INSERT INTO address (address_id,user_id,flat_no,Area,Building_no,Street_name,City,District,State,Pincode) VALUES" +
                                                        "(default,@uid,@flat,@area,@build,@street,@city,@district,@state,@pincode)";
                    //  @bid,@bname,@aname,@bpages,@blang,@bpub,@bdate,@bdimen,@bpp,@bhp,@ebp,@bdisc,@aathor,@brat,@bimag,@bimag2,@bimag3
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    //cmd.Parameters.Add(new MySqlParameter("@bid", newbook.booksID));
                    cmd.Parameters.Add(new MySqlParameter("@uid", customerid));
                    cmd.Parameters.Add(new MySqlParameter("@flat", flat_no));
                    cmd.Parameters.Add(new MySqlParameter("@area", build_no));
                    cmd.Parameters.Add(new MySqlParameter("@build", build_no));
                    cmd.Parameters.Add(new MySqlParameter("@street", street));
                    cmd.Parameters.Add(new MySqlParameter("@city", city));
                    cmd.Parameters.Add(new MySqlParameter("@district", district));
                    cmd.Parameters.Add(new MySqlParameter("@state", state));
                    cmd.Parameters.Add(new MySqlParameter("@pincode", pincode));


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

        public static bool changepassword(string currentpassword, string newpassword, int customerid)
        {
            bool status = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    //address_id | user_id | flat_no | Area  | Building_no | Street_name | City | District | State       | Pincode |

                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "update customers set password = @passnew where customerID = @cid and password = @pass";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.Add(new MySqlParameter("@passnew", newpassword));
                    cmd.Parameters.Add(new MySqlParameter("@pass", currentpassword));

                    cmd.Parameters.Add(new MySqlParameter("@cid", customerid));


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
