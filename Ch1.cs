using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
    public class ProductNameComparer1 : IComparer
    {
        public int Compare(object x, object y)
        {
            Product1 first = (Product1)x;
            Product1 second = (Product1)y;
            return first.Name.CompareTo(second.Name);
        }
        public static void SortDemo()
        {
            ArrayList products = Product1.GetSampleProducts();
            products.Sort(new ProductNameComparer1());
            foreach(Product1 product in products)
            {
                Console.WriteLine(product);
            }
        }
        public static void QueryDemo()
        {
            ArrayList products = Product1.GetSampleProducts();
            foreach (Product1 product in products)
            {
                if(product.Price > 10m)
                {
                    Console.WriteLine(product);
                }
            }
        }
    }
    public class ProductNameComparer2 : IComparer<Product2>
    {
        public int Compare(Product2 x, Product2 y)
        {
            return x.Name.CompareTo(y.Name);
        }
        public static void SortDemo1()
        {
            List<Product2> products = Product2.GetSampleProducts();
            products.Sort(new ProductNameComparer2());
            foreach (Product2 product in products)
            {
                Console.WriteLine(product);
            }
        }
        public static void SortDemo2()
        {
            List<Product2> products = Product2.GetSampleProducts();
            products.Sort(delegate (Product2 x, Product2 y) {
                return x.Name.CompareTo(y.Name);
            });
            foreach (Product2 product in products)
            {
                Console.WriteLine(product);
            }
        }
        public static void QueryDemo1()
        {
            List<Product2> products = Product2.GetSampleProducts();
            Predicate<Product2> predicate = delegate (Product2 p) { return p.Price > 10m; };
            List<Product2> matches = products.FindAll(predicate);
            Action<Product2> print = Console.WriteLine;
            matches.ForEach(print);
        }
        public static void QueryDemo2()
        {
            List<Product2> products = Product2.GetSampleProducts();
            Predicate<Product2> predicate = delegate (Product2 p) { return p.Price > 10m; };
            products.
                FindAll(delegate (Product2 p) { return p.Price > 10; }).
                ForEach(Console.WriteLine);
        }
    }
    public class ProductNameComparer3
    {
        public static void SortDemo1()
        {
            List<Product3> products = Product3.GetSampleProducts();
            products.Sort((x, y) => x.Name.CompareTo(y.Name));
            foreach (Product3 product in products)
            {
                Console.WriteLine(product);
            }
        }
        public static void SortDemo2()
        {
            List<Product3> products = Product3.GetSampleProducts();
            foreach (Product3 product in products.OrderBy(p => p.Name))
            {
                Console.WriteLine(product);
            }
        }
        public static void QueryDemo()
        {
            List<Product3> products = Product3.GetSampleProducts();
            foreach (Product3 product in products.Where(p => p.Price > 10))
            {
                Console.WriteLine(product);
            }
        }
    }
}
