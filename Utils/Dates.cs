using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Dates
    {
        private CultureInfo culture = CultureInfo.CreateSpecificCulture("en-IN");

        public string Readable(string dateString, string format)
        {
            DateTime dateValue = DateTime.Parse(dateString, culture);
            return dateValue.ToString(format, culture);
        }

        public DateTime ToDateTime(string dateString)
        {
            return DateTime.Parse(dateString, culture); ;
        }

        public int Compare(string dateString, string format)
        {
            return DateTime.Compare(ToDateTime(dateString), DateTime.Now);
        }
    }
}
