using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utils
{
    // https://stackoverflow.com/a/1344255/5490303
    public class Randoms
    {
        public const int HEX = 0;
        public const int CAPITAL_HEX = 1;
        public const int MIXED_HEX = 2;
        public const int SMALLS = 3;
        public const int CAPITALS = 4;
        public const int SMALL_CAPITALS = 5;
        public const int NUMBERS = 6;
        public const int SMALL_NUMS = 7;
        public const int CAPITAL_NUMS = 8;
        public const int MIXED = 9;

        internal static readonly string nums = "1234567890";
        internal static readonly string smallHex = "abcdef";
        internal static readonly string capitalHex = "ABCDEF";
        internal static readonly string smalls = "abcdefghijklmnopqrstuvwxyz";
        internal static readonly string capitals = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string Pattern(string randoms)
        {
            string pattern = @"{(.*?)}";
            MatchCollection matches = Regex.Matches(randoms, pattern);

            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                string random = "";
                string[] splits = groups[1].ToString().Split(':');

                switch (splits[0])
                {
                    case "HEX":
                        random = String(int.Parse(splits[1]), HEX);
                        break;
                    case "CAPITAL_HEX":
                        random = String(int.Parse(splits[1]), CAPITAL_HEX);
                        break;
                    case "MIXED_HEX":
                        random = String(int.Parse(splits[1]), MIXED_HEX);
                        break;
                    case "SMALLS":
                        random = String(int.Parse(splits[1]), SMALLS);
                        break;
                    case "CAPITALS":
                        random = String(int.Parse(splits[1]), CAPITALS);
                        break;
                    case "SMALL_CAPITALS":
                        random = String(int.Parse(splits[1]), SMALL_CAPITALS);
                        break;
                    case "NUMBERS":
                        random = String(int.Parse(splits[1]), NUMBERS);
                        break;
                    case "SMALL_NUMS":
                        random = String(int.Parse(splits[1]), SMALL_NUMS);
                        break;
                    case "CAPITAL_NUMS":
                        random = String(int.Parse(splits[1]), CAPITAL_NUMS);
                        break;
                    case "MIXED":
                        random = String(int.Parse(splits[1]), MIXED);
                        break;
                }

                randoms = Utils.Strings.ReplaceFirst(randoms, groups[0].ToString(), random);
            }

            return randoms;
        }

        public static string String(int size, int mode = 9)
        {
            char[] chars = "".ToCharArray();
            switch(mode)
            {
                case 0:
                    chars = (smallHex + nums).ToCharArray();
                    break;
                case 1:
                    chars = (capitalHex + nums).ToCharArray();
                    break;
                case 2:
                    chars = (smallHex + capitalHex + nums).ToCharArray();
                    break;
                case 3:
                    chars = (smalls).ToCharArray();
                    break;
                case 4:
                    chars = (capitals).ToCharArray();
                    break;
                case 5:
                    chars = (smalls + capitals).ToCharArray();
                    break;
                case 6:
                    chars = (nums).ToCharArray();
                    break;
                case 7:
                    chars = (smalls + nums).ToCharArray();
                    break;
                case 8:
                    chars = (capitals + nums).ToCharArray();
                    break;
                case 9:
                    chars = (smalls + capitals + nums).ToCharArray();
                    break;
            }

            return GenerateRandom(size, chars);
        }

        static string GenerateRandom(int size, char[] chars)
        {
            byte[] data = new byte[4 * size];
            
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            
            StringBuilder result = new StringBuilder(size);
            
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }

        // https://stackoverflow.com/a/1344365/5490303
        public static string String2(int string_length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bit_count = (string_length * 6);
                var byte_count = ((bit_count + 7) / 8); // rounded up
                var bytes = new byte[byte_count];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
