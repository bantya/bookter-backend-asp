using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Utils;

namespace Utils
{
    public class PostFormatter
    {
        private string mentionPattern = @"\s\@(.+?)\s";
        private string hashtagPattern = @"\s\#(.+?)\s";

        public string Mentions(string content)
        {
            MatchCollection matches = Regex.Matches(content, mentionPattern);

            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                Console.WriteLine(match);
                content = Strings.ReplaceFirst(content, groups[0].ToString(), $"<a class='post-mention' href='/@{groups[1].ToString()}'>@" + groups[1].ToString() + "</a>");
            }

            content = HttpUtility.HtmlDecode(content);

            return content;
        }

        public string Hashtags(string content)
        {
            MatchCollection matches = Regex.Matches(content, hashtagPattern);

            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                Console.WriteLine(match);
                content = Strings.ReplaceFirst(content, groups[0].ToString(), $"<a class='post-mention' href='/@{groups[1].ToString()}'>@" + groups[1].ToString() + "</a>");
            }

            content = HttpUtility.HtmlDecode(content);

            return content;
        }
    }
}
