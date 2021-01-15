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
