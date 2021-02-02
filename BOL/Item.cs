using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class Item
    {
        public Books book { set; get; }
        public int quantity { set; get; } = 1;
    }
}
