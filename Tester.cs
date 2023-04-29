using Newtonsoft.Json;
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
        readonly Type boolean_array = typeof(bool[]);
        readonly Type int32_array = typeof(int[]);
        readonly Type int32_array_2d = typeof(int[][]);
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
            object attribute, result;
            string answerString, resultString;
            int i, len;
            float count, nCorrect;
            bool isEqual;
            DateTime time;

            foreach (MethodInfo method in methodInfos)
            {
                attributes = method.GetCustomAttributes(false);
                len = attributes.Length;
                count = 0;
                nCorrect = 0;
                time = DateTime.Now;

                for (i = 0; i < len; i++)
                {
                    attribute = attributes[i];

                    if (attribute is TestAttribute)
                    {
                        attr = (TestAttribute)attribute;
                        result = method.Invoke(instance, attr.Args);
                        isEqual = false;
                        rt = method.ReturnType;
                        //Console.WriteLine($"ReturnType: {rt.Name}");

                        if (rt == int32_array)
                        {
                            isEqual = IsEquals((int[])result, (int[])attr.Answer);
                            answerString = $"{((int[])attr.Answer).FormatString()}";
                            resultString = $"{((int[])result).FormatString()}";
                        }
                        else if (rt == int32_array_2d)
                        {
                            isEqual = IsEquals2D((int[][])result, (string)attr.Answer);
                            answerString = $"{((int[][])attr.Answer).FormatString()}";
                            resultString = $"{((int[][])result).FormatString()}";
                        }
                        else if (rt == int64_array)
                        {
                            isEqual = IsEquals((long[])result, (long[])attr.Answer);
                            answerString = $"{((long[])attr.Answer).FormatString()}";
                            resultString = $"{((long[])result).FormatString()}";
                        }
                        else if (rt == boolean_array)
                        {
                            isEqual = IsEquals((bool[])result, (bool[])attr.Answer);
                            answerString = $"{((bool[])attr.Answer).FormatString()}";
                            resultString = $"{((bool[])result).FormatString()}";
                        }
                        else
                        {
                            isEqual = result.Equals(attr.Answer);
                            answerString = attr.Answer.ToString();
                            resultString = result.ToString();
                        }

                        if (isEqual)
                        {
                            Console.WriteLine($"{method.Name}({count}) Correct");
                            nCorrect++;
                        }
                        else
                        {
                            Console.WriteLine($"{method.Name}({count}) Failed | answer: {answerString}, result: {resultString}");
                        }

                        count++;
                    }
                }

                if(count > 0)
                {
                    Console.WriteLine($"{method.Name} | Accuracy rate: {nCorrect / count * 100.0f} | Cost time: {DateTime.Now - time}");
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

        public bool IsEquals2D<T>(T[][] a, string b)
        {
            int row, col, ROW = a.Length, COL;
            T[][] bArray = JsonConvert.DeserializeObject<T[][]>(b);

            if (bArray.Length != ROW)
            {
                return false;
            }

            for (row = 0; row < ROW; row++)
            {
                COL = a[row].Length;

                if (bArray[row].Length != COL)
                {
                    return false;
                }

                for (col = 0; col < COL; col++)
                {
                    if(!a[row][col].Equals(bArray[row][col]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void TestLeetcode(int q = -1)
        {
            Solution solution = new Solution();
            (string, int[][])[] questions = new(string, int[][])[] 
            { 
                ("abaca", new int[][]{ new int[]{ 0, 1 }, new int[] { 0, 2 }, new int[] { 2, 3 }, new int[] { 3, 4 } }),
                ("a", new int[][]{ new int[] { 0, 0 } }),
                ("hhqhuqhqff", new int[][]{ 
                    new int[]{0, 1},
                    new int[]{0, 2},
                    new int[]{2, 3},
                    new int[]{3, 4},
                    new int[]{3, 5},
                    new int[]{5, 6},
                    new int[]{2, 7},
                    new int[]{6, 7},
                    new int[]{7, 8},
                    new int[]{3, 8},
                    new int[]{5, 8},
                    new int[]{8, 9},
                    new int[]{3, 9},
                    new int[]{6, 9},
                }),
                ("g", new int[][]{ }),
                ("keitgkggegyktyeytgyigkggktiigigkeyygtgytiygtkg", new int[][]
                {
                    new int[]{0,1},
                    new int[]{1,2},
                    new int[]{2,3},
                    new int[]{1,3},
                    new int[]{3,4},
                    new int[]{4,5},
                    new int[]{5,6},
                    new int[]{3,6},
                    new int[]{5,7},
                    new int[]{6,8},
                    new int[]{5,8},
                    new int[]{7,8},
                    new int[]{8,9},
                    new int[]{7,10},
                    new int[]{8,10},
                    new int[]{9,10},
                    new int[]{10,11},
                    new int[]{9,11},
                    new int[]{7,11},
                    new int[]{5,12},
                    new int[]{11,12},
                    new int[]{11,13},
                    new int[]{13,14},
                    new int[]{12,14},
                    new int[]{12,15},
                    new int[]{10,15},
                    new int[]{14,15},
                    new int[]{7,15},
                    new int[]{9,16},
                    new int[]{13,16},
                    new int[]{12,16},
                    new int[]{15,16},
                    new int[]{11,17},
                    new int[]{14,17},
                    new int[]{16,17},
                    new int[]{15,18},
                    new int[]{14,18},
                    new int[]{17,18},
                    new int[]{18,19},
                    new int[]{14,19},
                    new int[]{13,19},
                    new int[]{14,20},
                    new int[]{15,21},
                    new int[]{12,21},
                    new int[]{20,21},
                    new int[]{19,22},
                    new int[]{20,22},
                    new int[]{21,22},
                    new int[]{22,23},
                    new int[]{19,23},
                    new int[]{11,23},
                    new int[]{18,23},
                    new int[]{13,24},
                    new int[]{23,24},
                    new int[]{21,24},
                    new int[]{24,25},
                    new int[]{13,25},
                    new int[]{23,25},
                    new int[]{15,26},
                    new int[]{23,26},
                    new int[]{25,26},
                    new int[]{24,26},
                    new int[]{26,27},
                    new int[]{25,27},
                    new int[]{26,28},
                    new int[]{27,28},
                    new int[]{20,28},
                    new int[]{23,28},
                    new int[]{11,28},
                    new int[]{23,29},
                    new int[]{29,30},
                    new int[]{25,31},
                    new int[]{26,31},
                    new int[]{15,32},
                    new int[]{30,32},
                    new int[]{31,33},
                    new int[]{27,33},
                    new int[]{30,33},
                    new int[]{28,33},
                    new int[]{29,34},
                    new int[]{32,35},
                    new int[]{33,35},
                    new int[]{34,35},
                    new int[]{35,36},
                    new int[]{13,36},
                    new int[]{34,36},
                    new int[]{30,37},
                    new int[]{36,37},
                    new int[]{35,37},
                    new int[]{24,37},
                    new int[] {35, 38},
                    new int[] {34, 39},
                    new int[] {37, 39},
                    new int[] {37, 40},
                    new int[] {39, 41},
                    new int[] {37, 41},
                    new int[] {41, 42},
                    new int[] {38, 42},
                    new int[] {40, 43},
                    new int[] {43, 44},
                    new int[] {39, 44},
                    new int[] {35, 44},
                    new int[] {38, 45},
                    new int[] {44, 45},
                    new int[] {26, 45}
                }),
            };
            int[] answers = new int[] { 3, -1, 3, 1, 10};
            int i, len = answers.Length;
            (string, int[][]) question;
            int answer, result = 0;
            DateTime start;
            for (i = 0; i < len; i++)
            {
                if(q != -1 && q != i)
                {
                    continue;
                }
                question = questions[i];
                answer = answers[i];
                start = DateTime.Now;
                //result = solution.LargestPathValue(question.Item1, question.Item2);
                if(result != answer)
                {
                    Console.WriteLine($"{i}) Error | answer: {answer}, result: {result}");
                }
                else
                {
                    Console.WriteLine($"{i}) Correct, cost time: {DateTime.Now - start}");
                }
            }
        }
    }
}
