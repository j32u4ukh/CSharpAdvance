using System;
using System.Reflection;

namespace CSharpAdvance
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class TestAttribute : Attribute
    {
        private readonly object[] args;
        private readonly object answer;

        public TestAttribute(params object[] args)
        {
            int i, len = args.Length - 1;
            this.args = new object[len];
            for(i = 0; i < len; i++)
            {
                this.args[i] = args[i];
            }
            answer = args[len];
        }

        public object[] Args
        {
            get { return args; }
        }

        public object Answer
        {
            get { return answer; }
        }
    }

    public class Tester
    {
        Type type;
        object instance;

        public Tester(){}

        public Tester(Type type, object instance)
        {
            this.type = type;
            this.instance = instance;
        }

        public void Test()
        {
            MethodInfo[] methodInfos = type.GetMethods();
            object[] attributes;
            object attribute;
            object result;
            TestAttribute attr;
            int i, len;
            float count, nCorrect;

            foreach (MethodInfo method in methodInfos)
            {
                attributes = method.GetCustomAttributes(false);
                len = attributes.Length;
                //Console.WriteLine($"Start test method: {method.Name}, #attribute: {len}");
                count = 0;
                nCorrect = 0;

                for (i = 0; i < len; i++)
                {
                    attribute = attributes[i];

                    if (attribute is TestAttribute)
                    {
                        attr = (TestAttribute)attribute;
                        result = method.Invoke(instance, attr.Args);

                        if (result.Equals(attr.Answer))
                        {
                            Console.WriteLine($"{method.Name}({count}) Correct");
                            nCorrect++;
                        }
                        else
                        {
                            Console.WriteLine($"{method.Name}({count}) Failed | answer: {attr.Answer}, result: {result}");
                        }

                        count++;
                    }
                }

                if(count > 0)
                {
                    Console.WriteLine($"{method.Name} | Accuracy rate: {nCorrect / count * 100.0f}");
                }
            }
        }

        public void Test<T1, T2, TResult>(Func<T1, T2, TResult> func)
        {

        }

        // string Convert(string s, int numRows)
        public void TestZigzagConversion(Func<string, int, string> func, int q = -1)
        {
            (string, int)[] questions = new (string, int)[]
            { 
                ("PAYPALISHIRING", 3),
                ("PAYPALISHIRING", 4),
                ("A", 1),
                ("AB", 1),
                ("ABC", 1),
                ("ABCDEF", 1),
                ("ABCD", 2),
                ("ABCDEF", 2),
            };
            string[] answers = new string[] 
            {
                "PAHNAPLSIIGYIR",
                "PINALSIGYAHRPI",
                "A",
                "AB",
                "ABC",
                "ABCDEF",
                "ACBD",
                "ACEBDF",
            };
            int i, len = answers.Length;
            string result;
            for(i = 0; i < len; i++)
            {
                if ((q != -1) && (q != i))
                {
                    continue;
                }
                result = func(questions[i].Item1, questions[i].Item2);
                if(result == answers[i])
                {
                    Console.WriteLine($"{i}) Correct");
                }
                else
                {
                    Console.WriteLine($"{i}) Error | answer: {answers[i]}, result: {result}");
                }
            }
        }

    }
}
