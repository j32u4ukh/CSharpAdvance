﻿using System;

namespace CSharpAdvance
{
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
