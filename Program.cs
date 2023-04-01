using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvance
{
    public class Person
    {
        string name;

        public Person(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();
            Tester tester = new Tester(typeof(Solution), solution);
            tester.Test();

            Console.WriteLine("Press any to end...");
            Console.ReadKey();
        }
    }
}
