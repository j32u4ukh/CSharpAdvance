using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvance
{
    public interface IShape
    {
        int Area();
    }

    public class Circle : IShape
    {
        int radius;

        public Circle(int r)
        {
            radius = r;
        }

        public int Area()
        {
            return (int)(radius * radius * Math.PI);
        }

        public override string ToString()
        {
            return $"Circle({radius})";
        }
    }

    public class AreaComparer<T> : IComparer<T> where T : IShape
    {
        public int Compare(IShape x, IShape y)
        {
            return x.Area().CompareTo(y.Area());
        }

        public int Compare(T x, T y)
        {
            return x.Area().CompareTo(y.Area());
        }
    }

    public class AreaComparer : AreaComparer<IShape> { }

    class Program
    {

        static void Main(string[] args)
        {
            IComparer<IShape> comparer = new AreaComparer();
            List<Circle> circles = new List<Circle>();
            circles.Add(new Circle(5));
            circles.Add(new Circle(3));
            circles.Add(new Circle(4));
            circles.Sort(comparer);
            Console.WriteLine(circles.FormatString());
            Console.ReadKey();
        }
    }
}
