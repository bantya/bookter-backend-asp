using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BOL;

namespace BLL
{
    public class BussinessManager
    {
        public static Customer validatecustomer(string uname, string upass)
        {
            return LoginDBManager.validatelogin(uname, upass);
        }

        public static List<Books> Getbook()
        {
            List<Books> allbooks = new List<Books>();
            allbooks = BookDBManager.Getbooks();

            return allbooks;
        }

        public static List<Order> getOrders(int cust_id)
        {
            return OrdersDBManager.getOrders(cust_id);
        }

        public static List<Books> getPurchasedBooks(int custid)
        {
            return BookDBManager.getAllBooks(custid);
        }

        public static bool changename(string firstname, string lastname, int customerid)
        {
            return SettingDBManager.changename(firstname, lastname, customerid);
        }

        public static List<OrderBooks> getOrderBooks(string token, int customerid)
        {
            return OrdersDBManager.getOrderBooks(token, customerid);
        }

        public static void createOrder(int user_id, int book_id, int qtt, double total, string token)
        {
            OrdersDBManager.createOrder(user_id, book_id, qtt, total, token);
        }

        public static int getLatestOrderId(int customerid)
        {
            return OrdersDBManager.getLatestOrderId(customerid);
        }

        //public static Address getaddress(int customerid)
        //{
        //    return BookDBManager.findaddress(customerid);
        //}

        public static Address findaddress(int customerid)
        {
            return BookDBManager.findaddress(customerid);
        }


        public static Customer getUserByUsername(string username)
        {
            return LoginDBManager.getUserByUsername(username);
        }

        public static bool addBook(int ord_i, object book_id, object quan)
        {
            return OrdersDBManager.addBook(ord_i, book_id, quan);
        }

        public static bool changeemail(string email, int customerid)
        {
            return SettingDBManager.changeemail(email, customerid);
        }

        public static bool changeadd(int customerid, string flat_no, string build_no, string area, string street, string city, string district, string state, string pincode)
        {
            return BookDBManager.changeaddress(customerid,flat_no,build_no,area,street,city,district,state,pincode);        
        
        }

        public static bool changepassword(string currentpassword, string newpassword, int customerid)
        {
            return SettingDBManager.changepassword(currentpassword, newpassword, customerid);
        }

        public static Followers CheckFollow(string v, int customerid)
        {
            return PostDBManager.CheckFollow(v,customerid);
        }

        public static bool updateAddress(int customerid, string flat_no, string build_no, string area, string street, string city, string district, string state, string pincode)
        {
            return SettingDBManager.updateAddress(customerid, flat_no, build_no, area, street, city, district, state, pincode);
        }

        public static List<Posts> GetallPostsByUsername(string v)
        {
            return PostDBManager.GetallPostsByUsername(v);
        }

        public static bool UpdatePostImages(List<string> images, int post_id)
        {
            return PostDBManager.UpdatePostImages(images, post_id);
        }

        public static bool addNewaddress(int customerid, string flat_no, string build_no, string area, string street, string city, string district, string state, string pincode)
        {
            return SettingDBManager.addNewaddress(customerid, flat_no, build_no, area, street, city, district, state, pincode);
        }

        public static bool addaddress(int customerid, string flat_no, string build_no, string area, string street, string city, string district, string state, string pincode)
        {
            return BookDBManager.addaddress(customerid,flat_no,build_no,area,street,city,district,state,pincode);
        }

        public static List<Posts> GetallPosts(int customerid)
        {
            return PostDBManager.GetallPosts(customerid);
        }

        public static bool AddLike(int customerid, int id)
        {
            return LikeDBManager.AddLike(customerid, id);
        }

        public static List<Likes> GetallLikes(int custid)
        {
            return LikeDBManager.GetallLikes(custid);
        }

        public static List<Books> GetWishList(int cid)
        {
            return WishlistDBManager.GetWishList(cid);
        }

        public static bool validatecustomer(Login login)
        {
            return LoginDBManager.validatelogin(login);
        }

        public static int insertpost(int customerid, string postcontent, int postbooks, string customer_name)
        {
            return PostDBManager.insertpost(customerid, postcontent, postbooks,customer_name);
        }

        public static bool AddDislike(int customerid, int id)
        {
            return LikeDBManager.AddDislike(customerid, id);
        }

        public static bool AddFollow(int customerid, int id, string fname)
        {
            return LikeDBManager.AddFollow(customerid, id,fname);
        }

        public static Books GetBookdetails(int id)
        {
            return BookDBManager.GetByID(id);
        }

        public static bool deletepost(int id, int customerid)
        {
            return PostDBManager.deleteposts(id,customerid);
        }

        public static bool unFollow(int customerid, int id, string fname)
        {
            return LikeDBManager.unFollow(customerid, id, fname);
        }

        public static bool register(Customer cust)
        {
            return LoginDBManager.register(cust);
        }

        public static bool AddWishlist(int cid, int bid)
        {
            return WishlistDBManager.AddWishlist(cid, bid);
        }

        public static bool UpdateBookImages(List<string> images, int book_id)
        {
            return AdminDBManager.UpdateBookImages(images, book_id);
        }

        public static List<Posts> GetallUsersPosts(int customerid)
        {
            return PostDBManager.GetallUsersPosts(customerid);
        }

        public static bool AdminLogin(string uname, string upass)
        {
            return AdminDBManager.AdminLogin(uname, upass);
        }

        public static bool insertcomment(int postid, int customerid, string postcomment, string customer_name, int userid)
        {
            return PostDBManager.insertcomment(postid, customerid,postcomment,customer_name,userid);
        }

        public static bool UpdateBookbyPrice(int bid, double bpp, double bhp, double ebp)
        {
            return AdminDBManager.UpdateBookbyPrice(bid, bpp, bhp, ebp);
        }

        
        public static bool deletewishlist(int cid, int bid)
        {
            return WishlistDBManager.deletewishlist(cid, bid);
        }

        public static bool adminregister(Admin newadmin)
        {
            return AdminDBManager.AdminRegistrartion(newadmin);
        }

        public static bool ArchiveBook(int booksID)
        {
            return AdminDBManager.ArchiveBook(booksID);
        }

        public static bool CancelArchive(int booksID)
        {
            return AdminDBManager.CancelArchive(booksID);
        }


        public static bool validateforgot(string email)
        {
            return LoginDBManager.validateforgot(email);
        }

        public static bool forgotpasse(forgotpass newfor)
        {
            return LoginDBManager.forgotpasse(newfor);
        }

        public static string checkToken(string token)
        {
            return LoginDBManager.checkToken(token);
        }

        public static int Insertbook(Books newbook)
        {
            return BookDBManager.Insertbook(newbook);
        }

        public static bool UpdateBook(Books newbook)
        {
            return AdminDBManager.UpdateBook(newbook);
        }

        public static bool updatepass(string password, string custid)
        {
            return LoginDBManager.updatepass(password, custid);
        }
    }
}
