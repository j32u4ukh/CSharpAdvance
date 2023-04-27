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
            VerbHandler.Init();

            Verb[] verbs = new Verb[]
            {
                VerbHandler.New("かす"),
                VerbHandler.New("かく"),
                VerbHandler.New("かう"),
                VerbHandler.New("よむ"),
                VerbHandler.New("いく"),
                VerbHandler.New("たべる"),
                VerbHandler.New("する"),
                VerbHandler.New("くる"),
            };

            foreach(Verb verb in verbs)
            {
                verb.Show();
            }

            Console.WriteLine("Press any to continue...");
            Console.ReadKey();
        }
    }
}
