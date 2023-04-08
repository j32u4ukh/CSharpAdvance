using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvance
{
    class Program
    {
        static void print()
        {
            int i = 0;
            Console.WriteLine(i++);
        }

        static void Main(string[] args)
        {
            print();
            print();
            Console.ReadKey();
        }
    }
}
