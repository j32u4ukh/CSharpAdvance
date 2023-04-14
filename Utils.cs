using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvance
{
    public class Utils
    {

        public static string LogMessage([CallerMemberName] string method = null, [CallerFilePath] string path = null, [CallerLineNumber] int line = 0)
        {
            return $"[{method}] {path} ({line})";
        }
    }
}
