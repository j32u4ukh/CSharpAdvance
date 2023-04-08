using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvance.Ch1
{
    public class Product1
    {
        string name;
        public string Name { get { return name; } }
        decimal price;
        public decimal Price { get { return price; } }
        public Product1(string name, decimal price)
        {
            this.name = name;
            this.price = price;
        }
        public static ArrayList GetSampleProducts()
        {
            ArrayList list = new ArrayList();
            list.Add(new Product1("West Side Story", 9.99m));
            list.Add(new Product1("Assassins", 14.99m));
            list.Add(new Product1("Frogs", 13.99m));
            list.Add(new Product1("Sweeney Todd", 10.99m));
            return list;
        }
        public override string ToString()
        {
            return string.Format("{0}: {1}", name, price);
        }
    }
    public class Product2
    {
        string name;
        public string Name 
        { 
            get { return name; } 
            private set { name = value; }
        }
        decimal price;
        public decimal Price 
        { 
            get { return price; } 
            private set { price = value; }
        }
        public Product2(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
        public static List<Product2> GetSampleProducts()
        {
            List<Product2> list = new List<Product2>();
            list.Add(new Product2("West Side Story", 9.99m));
            list.Add(new Product2("Assassins", 14.99m));
            list.Add(new Product2("Frogs", 13.99m));
            list.Add(new Product2("Sweeney Todd", 10.99m));
            return list;
        }
        public override string ToString()
        {
            return string.Format("{0}: {1}", name, price);
        }
    }
    public class Product3
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public Product3(){}
        public Product3(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
        public static List<Product3> GetSampleProducts()
        {
            return new List<Product3>()
            {
                new Product3{Name= "West Side Story", Price= 9.99m },
                new Product3{Name= "Assassins", Price= 14.99m },
                new Product3{Name= "Frogs", Price= 13.99m },
                new Product3{Name= "Sweeney Todd", Price= 10.99m },
            };
        }
        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, Price);
        }
    }
    public class Product4
    {
        readonly string name;
        public string Name { get { return name; } }
        readonly decimal price;
        public decimal Price { get { return price; } }
        public Product4(string name, decimal price)
        {
            this.name = name;
            this.price = price;
        }
        public static List<Product4> GetSampleProducts()
        {
            return new List<Product4>()
            {
                new Product4(name: "West Side Story", price: 9.99m),
                new Product4(name: "Assassins", price: 14.99m),
                new Product4(name: "Frogs", price: 13.99m),
                new Product4(name: "Sweeney Todd", price: 10.99m),
            };
        }
        public override string ToString()
        {
            return string.Format("{0}: {1}", name, price);
        }
    }
}
