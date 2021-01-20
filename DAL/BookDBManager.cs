﻿using System;
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
    public class BookDBManager
    {
        public static readonly string connString = string.Empty;

        static BookDBManager()
        {
            connString = ConfigurationManager.ConnectionStrings["dbString"].ConnectionString;
        }

        public static List<Books> Getbooks()
        {
            List<Books> allProducts = new List<Books>();

            //logic to fetch all records from database
            IDbConnection conn = new MySqlConnection();
            conn.ConnectionString = connString;
            string query = "select * from books";
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
                    Books theproducts = new Books();
                    theproducts.booksID = int.Parse(row["booksID"].ToString());
                    theproducts.bookname = row["bookname"].ToString();
                    theproducts.bookauthor = row["bookauthor"].ToString();
                    theproducts.bookspage = int.Parse(row["bookpage"].ToString());
                    theproducts.booklang = row["booklang"].ToString();
                    theproducts.bookdate = row["publishdate"].ToString();
                    theproducts.bookpublisher = row["bookpublisher"].ToString();
                    theproducts.bookdimension = row["bookdimension"].ToString();
                    theproducts.paperprice = double.Parse(row["paperprice"].ToString());
                    theproducts.hardprice = double.Parse(row["hardprice"].ToString());
                    theproducts.ebookprice = double.Parse(row["ebookprice"].ToString());

                    theproducts.bookdisc = row["bookdisc"].ToString();
                    theproducts.aboutauthor = row["aboutauthor"].ToString();
                    theproducts.rating = double.Parse(row["rating"].ToString());
                    theproducts.image = row["image"].ToString();
                    theproducts.image2 = row["image2"].ToString();
                    theproducts.image3 = row["image3"].ToString();


                    allProducts.Add(theproducts);
                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }


            return allProducts;

        }

        public static bool Insertbook(Books newbook)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {


                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    string query = "INSERT INTO books (booksID,bookname,bookauthor,bookpage,booklang,bookpublisher,publishdate,bookdimension,paperprice,hardprice,ebookprice,bookdisc,aboutauthor,rating,image,image2,image3) VALUES" +
                                                        "(default,@bname,@aname,@bpages,@blang,@bpub,@bdate,@bdimen,@bpp,@bhp,@ebp,@bdisc,@aathor,@brat,@bimag,@bimag2,@bimag3)";
                  //  @bid,@bname,@aname,@bpages,@blang,@bpub,@bdate,@bdimen,@bpp,@bhp,@ebp,@bdisc,@aathor,@brat,@bimag,@bimag2,@bimag3
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    //cmd.Parameters.Add(new MySqlParameter("@bid", newbook.booksID));
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

        public static Books GetByID(int id)
        {
            Books theproducts = new Books();

            IDbConnection conn = new MySqlConnection();
            conn.ConnectionString = connString;
            string query = "Select * from books WHERE booksID=" + id;
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
                    theproducts.booksID = int.Parse(row["booksID"].ToString());
                    theproducts.bookname = row["bookname"].ToString();
                    theproducts.bookauthor = row["bookauthor"].ToString();
                    theproducts.bookspage = int.Parse(row["bookpage"].ToString());
                    theproducts.booklang = row["booklang"].ToString();
                    theproducts.bookdate = row["publishdate"].ToString();
                    theproducts.bookpublisher = row["bookpublisher"].ToString();
                    theproducts.bookdimension = row["bookdimension"].ToString();
                    theproducts.paperprice = double.Parse(row["paperprice"].ToString());
                    theproducts.hardprice = double.Parse(row["hardprice"].ToString());
                    theproducts.ebookprice = double.Parse(row["ebookprice"].ToString());

                    theproducts.bookdisc = row["bookdisc"].ToString();
                    theproducts.aboutauthor = row["aboutauthor"].ToString();
                    theproducts.rating = double.Parse(row["rating"].ToString());
                    theproducts.image = row["image"].ToString();
                    theproducts.image2 = row["image2"].ToString();
                    theproducts.image3 = row["image3"].ToString();

                }
            }
            catch (MySqlException e)
            {
                string message = e.Message;
            }
            // implementation 
            return theproducts;
        }
    }
}
