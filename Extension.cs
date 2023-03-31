using System.Collections.Generic;
using System.Text;

namespace CSharpAdvance
{
    public static class Extension
    {
        public static string FormatString<T>(this IList<T> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            int len = list.Count;
            if (len > 0)
            {
                sb.Append($"{list[0]}");
                for (int i = 1; i < len; i++)
                {
                    sb.Append($", {list[i]}");
                }
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
