﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvance
{
    public class Person
    {
        public string Name { get; private set; }
        public int Age { get; private set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            int[][] arr = new int[][] { new int[]{ 1, 2, 3, 4 }, new int[] { 1, 2, 3, 4 } };
            Console.WriteLine(arr.SequenceEqual(new int[][] { new int[] { 1, 2, 3, 4 }, new int[] { 1, 2, 3, 4 } }));
            Console.ReadKey();
        }
    }
}
