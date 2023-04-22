
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvance
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo1.Demo demo = new Demo1.Demo(program: Demo1.Demo.program);
            demo.Excute();

            Console.WriteLine("Press any to end...");
            Console.ReadKey();
        }
    }
}
