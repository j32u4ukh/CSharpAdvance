using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvance.Ch1
{
    public class Product
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public Product() { }
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
        public static List<Product> GetSampleProducts()
        {
            return new List<Product>() 
            {
                new Product("West Side Story", 9.99m),
                new Product("Assassins", 14.99m),
                new Product("Frogs", 13.99m),
                new Product("Sweeney Todd", 10.99m),
            };
        }
        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, Price);
        }
    }
}
