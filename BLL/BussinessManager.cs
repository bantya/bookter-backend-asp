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

        public static List<Books> GetWishList(int cid)
        {
            return WishlistDBManager.GetWishList(cid);
        }

        public static Books GetBookdetails(int id)
        {
            return BookDBManager.GetByID(id);
        }

        public static bool register(Customer cust)
        {
            return LoginDBManager.register(cust);
        }

        public static bool AddWishlist(int cid, int bid)
        {
            return WishlistDBManager.AddWishlist(cid, bid);
        }

        public static bool AdminLogin(string uname, string upass)
        {
            return AdminDBManager.AdminLogin(uname, upass);
        }

        public static bool deletewishlist(int cid, int bid)
        {
            return WishlistDBManager.deletewishlist(cid, bid);
        }

        public static bool adminregister(Admin newadmin)
        {
            return AdminDBManager.AdminRegistrartion(newadmin);
        }

        public static bool validateforgot(string email)
        {
            return LoginDBManager.validateforgot(email);
        }

        public static bool forgotpasse(forgotpass newfor)
        {
            return LoginDBManager.forgotpasse(newfor);
        }

        public static bool checkToken(string token)
        {
            return LoginDBManager.checkToken(token);
        }

        public static bool Insertbook(Books newbook)
        {
            return BookDBManager.Insertbook(newbook);
        }

        public static bool UpdateBook(Books newbook)
        {
            return AdminDBManager.UpdateBook(newbook);
        }
    }
}
