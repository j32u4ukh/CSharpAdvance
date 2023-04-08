using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvance
{
    class Program
    {
        delegate void Print(string content);

        static void print1(string content)
        {
            Console.WriteLine($"print1 | content: {content}");
        }

        static void print2(string content)
        {
            Console.WriteLine($"print2 | content: {content}");
        }

        static void Main(string[] args)
        {
            int? a = 5;
            int b = a ?? 6;
            a = null;
            int c = a ?? 7;
            Console.WriteLine("b: {0}, c: {1}", b, c);
            Console.ReadKey();
        }
    }
}
