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

        public static string FormatString<T>(this T[][] list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            int len = list.Length;
            if (len > 0)
            {
                sb.Append($"{list[0].FormatString()}");
                for (int i = 1; i < len; i++)
                {
                    sb.Append($", {list[i].FormatString()}");
                }
            }
            sb.Append("]");
            return sb.ToString();
        }

        // TODO: 若 T 為引用，則也需對 T 做深層複製
        public static List<T> DeepClone<T>(this List<T> list)
        {
            List<T> clone = new List<T>();
            foreach(T t in list)
            {
                clone.Add(t);
            }
            return clone;
        }
    }
}
