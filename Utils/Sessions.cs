using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utils
{
    // https://gist.github.com/johndavedecano/8725034
    // https://stackoverflow.com/a/1918300/5490303
    public class Sessions
    {
        public static void put(string key, string message = "")
        {
            HttpContext context = HttpContext.Current;
            context.Session[key] = message;
        }

        public static string get(string key)
        {
            HttpContext context = HttpContext.Current;

            if (context.Session[key] == null)
            {
                return "";
            }
            else
            {
                return context.Session[key].ToString();
            }
        }

        public static string flash(string key)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session[key] == null)
            {
                return "";
            }
            else
            {
                string message = context.Session[key].ToString();
                context.Session.Remove(key);
                return message;
            }
        }

        public static bool has(string key)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session[key] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
