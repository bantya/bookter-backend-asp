using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Utils
    {
        public static int ParseNullableInt(string value)
        {
            int intValue;
            if (int.TryParse(value, out intValue))
            {
                return intValue;
            }
            return 0;
        }
    }
}
