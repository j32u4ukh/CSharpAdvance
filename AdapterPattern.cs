using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvance.AdapterPattern
{
    public class BlackMan
    {
        public readonly string name;

        public BlackMan(string name)
        {
            this.name = name;
        }

        public void HelloEnglish()
        {
            Console.WriteLine("yo~ what's up!!");
        }

        public void SelfIntroEnglish()
        {
            Console.WriteLine($"Hello, I living in taipei. My name is {name}.");
        }
    }

    public abstract class People
    {
        readonly string name;

        public People(string name)
        {
            this.name = name;
        }

        public abstract void Hello();

        public abstract void SelfIntro();

        public string getName()
        {
            return name;
        }
    }

    public class BlackmanTranslator : People
    {
        public BlackmanTranslator(string name) : base(name)
        {
        }

        public override void Hello()
        {
            Console.WriteLine(getName() + ":哩甲霸咩～真的假的！？");
        }

        public override void SelfIntro()
        {
            Console.WriteLine($"大家好我是{getName()}，現在台北工作。");
        }
    }

    public class TaiwanMan
    {
        private People people;

        public TaiwanMan(People people)
        {
            this.people = people;
        }

        public void Hello()
        {
            people.Hello();
        }

        public void SelfIntro()
        {
            people.SelfIntro();
        }

        public static void Demo()
        {
            BlackMan blackMan = new BlackMan("black");
            blackMan.HelloEnglish();
            blackMan.SelfIntroEnglish();

            TaiwanMan taiwanMan = new TaiwanMan(new BlackmanTranslator(blackMan.name));
            taiwanMan.Hello();
            taiwanMan.SelfIntro();
        }
    }
}
