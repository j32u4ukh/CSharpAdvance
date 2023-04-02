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
        readonly Type int32_array = typeof(int[]);
        readonly Type int64_array = typeof(long[]);

        public Tester(){}

        public Tester(Type type, object instance)
        {
            this.type = type;
            this.instance = instance;
        }

        public void Test()
        {
            MethodInfo[] methodInfos = type.GetMethods();
            TestAttribute attr;
            Type rt;
            object[] attributes;
            object attribute;
            object result;
            int i, len;
            float count, nCorrect;
            bool isEqual;

            foreach (MethodInfo method in methodInfos)
            {
                attributes = method.GetCustomAttributes(false);
                len = attributes.Length;
                count = 0;
                nCorrect = 0;

                for (i = 0; i < len; i++)
                {
                    attribute = attributes[i];

                    if (attribute is TestAttribute)
                    {
                        attr = (TestAttribute)attribute;
                        result = method.Invoke(instance, attr.Args);
                        isEqual = false;
                        rt = method.ReturnType;

                        if (rt == int32_array)
                        {
                            isEqual = IsEquals((int[])result, (int[])attr.Answer);
                        }
                        else if (rt == int64_array)
                        {
                            isEqual = IsEquals((long[])result, (long[])attr.Answer);
                        }
                        else
                        {
                            isEqual = result.Equals(attr.Answer);
                        }

                        if (isEqual)
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

        public bool IsEquals<T>(T[] a, T[] b)
        {
            int i, len = a.Length;

            if(b.Length != len)
            {
                return false;
            }

            for(i = 0; i < len; i++)
            {
                if (!a[i].Equals(b[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
