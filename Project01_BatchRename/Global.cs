using System.Text;

namespace Project01_BatchRename
{
    class Global
    {
        public static char toUpper(char c)
        {
            if (c >= 'a' && c <= 'z')
                return (char)(c - 32);
            return c;
        }

        // replace src[pos] with char ch and return
        public static string ReplaceCharAt(string src, int pos, char ch)
        {
            if (pos < 0 || pos >= src.Length) return src;
            string res = src.Substring(0, pos);
            res += ch;
            res += src.Substring(pos + 1);
            return res;
        }

        public static string NormalizeString(string src)
        {
            string res = src;
            // delete ' ' in front
            while (res[0] == ' ')
            {
                res = res.Remove(0, 1);
            }

            // delete ' ' in rear
            while (res[res.Length - 1] == ' ')
            {
                res = res.Remove(res.Length - 1, 1);
            }

            // delete "  " in between
            int pos;
            do
            {
                pos = res.IndexOf("  ");
                if (pos == -1)
                {
                    break;
                }

                res = res.Remove(pos, 1);
            } while (true);

            pos = res.LastIndexOf('.') - 1;
            if (pos > -1)
            {
                if (res[pos] == ' ')
                    res = res.Remove(pos, 1);
            }

            // Capitalize first char of each word
            res = ReplaceCharAt(res, 0, toUpper(res[0]));
            pos = 0;
            do
            {
                pos = res.IndexOf(' ', pos);
                if (pos == -1) break;
                pos++;
                res = ReplaceCharAt(res, pos, toUpper(res[pos]));
            } while (true);

            return res;
        }

        public static string NormalizeString(string src, int n)
        {
            string res = src;
            // delete ' ' in front
            while (res[0] == ' ')
            {
                res = res.Remove(0, 1);
            }

            // delete ' ' in rear
            while (res[res.Length - 1] == ' ')
            {
                res = res.Remove(res.Length - 1, 1);
            }

            // delete "  " in between
            int pos;
            do
            {
                pos = res.IndexOf("  ");
                if (pos == -1)
                {
                    break;
                }

                res = res.Remove(pos, 1);
            } while (true);

            // Capitalize first char of each word
            res = ReplaceCharAt(res, 0, toUpper(res[0]));
            pos = 0;
            do
            {
                pos = res.IndexOf(' ', pos);
                if (pos == -1) break;
                pos++;
                res = ReplaceCharAt(res, pos, toUpper(res[pos]));
            } while (true);

            return res;
        }
    }
}
