using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class Comment
    {
        public int comment_id { get; set; }
        public int user_id { get; set; }
        public string content { get; set; }
        public string user_name { get; set; }
        public int accountuser_id { get; set; }
        public string posted_on { get; set; }
    }
}

