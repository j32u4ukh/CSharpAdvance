using System.Collections.Generic;
using System;

namespace CSharpAdvance
{
    public enum Dan
    {
        None, A, I, U, E, O, Other,
    }

    public class VerbHandler
    {
        // [1: 一般, 2: 濁音, 3: 半濁音][][][1: あ段, 2: い段, 3: う段, 4: え段, 5: お段]
        public static readonly Dictionary<char, int> gojuon = new Dictionary<char, int>()
        {
            {'あ', 1001}, {'い', 1002}, {'う', 1003}, {'え', 1004}, {'お', 1005},
            {'か', 1011}, {'き', 1012}, {'く', 1013}, {'け', 1014}, {'こ', 1015},
            {'さ', 1021}, {'し', 1022}, {'す', 1023}, {'せ', 1024}, {'そ', 1025},
            {'た', 1031}, {'ち', 1032}, {'つ', 1033}, {'て', 1034}, {'と', 1035},
            {'な', 1041}, {'に', 1042}, {'ぬ', 1043}, {'ね', 1044}, {'の', 1045},
            {'は', 1051}, {'ひ', 1052}, {'ふ', 1053}, {'へ', 1054}, {'ほ', 1055},
            {'ま', 1061}, {'み', 1062}, {'む', 1063}, {'め', 1064}, {'も', 1065},
            {'や', 1071},               {'ゆ', 1073},               {'よ', 1075},
            {'ら', 1081}, {'り', 1082}, {'る', 1083}, {'れ', 1084}, {'ろ', 1085},
            {'わ', 1091},                                           {'を', 1095},
            {'ん', 1106},
            {'が', 2011}, {'ぎ', 2012}, {'ぐ', 2013}, {'げ', 2014}, {'ご', 2015},
            {'ざ', 2021}, {'じ', 2022}, {'ず', 2023}, {'ぜ', 2024}, {'ぞ', 2025},
            {'だ', 2031}, {'ぢ', 2032}, {'づ', 2033}, {'で', 2034}, {'ど', 2035},
            {'ば', 2051}, {'び', 2052}, {'ぶ', 2053}, {'べ', 2054}, {'ぼ', 2055},
            {'ぱ', 3051}, {'ぴ', 3052}, {'ぷ', 3053}, {'ぺ', 3054}, {'ぽ', 3055},
        };

        public static Dictionary<int, char> ongoju;

        public static void Init()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ongoju = new Dictionary<int, char>();
            foreach (KeyValuePair<char, int> pair in gojuon)
            {
                ongoju.Add(pair.Value, pair.Key);
            }
        }

        public static Verb New(string verb)
        {
            if (verb == "する" || verb == "くる")
            {
                return new Verb3(verb);
            }

            int len = verb.Length;

            if (verb[len - 1] != 'る')
            {
                return new Verb1(verb);
            }
            else if (Verb1.special_case.Contains(verb))
            {
                return new Verb1(verb);
            }
            else if (Verb2.special_case.Contains(verb))
            {
                return new Verb2(verb);
            }
            else
            {
                Dan dan = GetDan(verb[len - 2]);

                if (dan == Dan.I || dan == Dan.E)
                {
                    return new Verb2(verb);
                }
                else
                {
                    return new Verb1(verb);
                }
            }
        }

        public static Dan GetDan(char c)
        {
            int code = gojuon[c];

            if (code / 1000 > 0)
            {
                code %= 10;

                switch (code)
                {
                    case 1:
                        return Dan.A;
                    case 2:
                        return Dan.I;
                    case 3:
                        return Dan.U;
                    case 4:
                        return Dan.E;
                    case 5:
                        return Dan.O;
                    default:
                        return Dan.Other;
                }
            }

            return Dan.None;
        }
    }


    // Present
    // Negative
    // Past
    // Past negative
    // Te form
    // Volitional 邀請型
    // Imperative 命令型
    // Passive 被動
    // Causative 使役型
    // Potential 可能型
    public abstract class Verb
    {
        public int Type { get; private set; }
        public string Present { get; private set; }
        public string Negative { get; protected set; }
        public string TeForm { get; protected set; }
        public string Past { get; protected set; }

        // 使役
        public string Causative { get; protected set; }

        // 被動
        public string Passive { get; protected set; }

        // 使役被動
        public string CausativePassive { get; protected set; }

        // 命令
        public string Imperative { get; protected set; }

        // 可能
        public string Potential { get; protected set; }

        // 邀請
        public string Volitional { get; protected set; }

        // ます
        public string KeigoPresent { get; protected set; }

        protected int len;

        public Verb(string verb, int type)
        {
            Present = verb;
            Type = type;
            len = Present.Length;
            SetNegative();
            SetTeForm();
            SetPast();
            SetKeigoPresent();
            SetCausative();
            SetPassive();
            SetCausativePassive();
            SetImperative();
            SetPotential();
            SetVolitional();
        }

        protected abstract void SetNegative();
        protected abstract void SetTeForm();
        protected abstract void SetKeigoPresent();
        protected abstract void SetCausative();
        protected abstract void SetPassive();
        protected abstract void SetCausativePassive();
        protected abstract void SetImperative();
        protected abstract void SetPotential();
        protected abstract void SetVolitional();

        public void Show()
        {
            Console.WriteLine($"Present(原型): {Present}");
            Console.WriteLine($"Negative(否定): {Negative}");
            Console.WriteLine($"TeForm(て型): {TeForm}");
            Console.WriteLine($"Past(過去): {Past}");
            Console.WriteLine($"Causative(使役): {Causative}");
            Console.WriteLine($"Passive(被動): {Passive}");
            Console.WriteLine($"CausativePassive(使役被動): {CausativePassive}");
            Console.WriteLine($"Imperative(命令): {Imperative}");
            Console.WriteLine($"Potential(可能): {Potential}");
            Console.WriteLine($"Volitional(邀請): {Volitional}");
            Console.WriteLine($"KeigoPresent(ます): {KeigoPresent}");
            Console.WriteLine();
        }

        protected void SetPast()
        {
            if (TeForm.EndsWith("て"))
            {
                Past = TeForm.Substring(0, TeForm.Length - 1) + "た";
            }
            else
            {
                Past = TeForm.Substring(0, TeForm.Length - 1) + "だ";
            }
        }
    }

    public class Verb1 : Verb
    {
        public static HashSet<string> special_case = new HashSet<string>()
        {
            "かえる",
            "きる",
            "はしる",
        };

        public Verb1(string present) : base(present, 1)
        {

        }

        protected override void SetTeForm()
        {
            if (Present == "いく")
            {
                TeForm = "いって";
            }
            else
            {
                TeForm = Present.Substring(0, len - 1);
                char c = Present[len - 1];

                switch (c)
                {
                    case 'す':
                        TeForm += "して";
                        break;
                    case 'く':
                        TeForm += "いて";
                        break;
                    case 'ぐ':
                        TeForm += "いで";
                        break;
                    case 'う':
                    case 'つ':
                    case 'る':
                        TeForm += "って";
                        break;
                    case 'ぶ':
                    case 'ぬ':
                    case 'む':
                        TeForm += "んで";
                        break;
                    default:
                        throw new Exception();
                }
            }
        }


        protected override void SetKeigoPresent()
        {
            char c = Present[len - 1];

            // u -> i
            int code = VerbHandler.gojuon[c] - 1;

            KeigoPresent = Present.Substring(0, len - 1) + VerbHandler.ongoju[code] + "ます";
        }

        protected override void SetNegative()
        {
            char c = Present[len - 1];
            int code = VerbHandler.gojuon[c];

            // う -> わ
            if (code == 1003)
            {
                code = 1091;
            }

            // u -> a
            else
            {
                code -= 2;
            }

            Negative = Present.Substring(0, len - 1) + VerbHandler.ongoju[code] + "ない";
        }

        protected override void SetCausative()
        {
            char c = Present[len - 1];
            int code = VerbHandler.gojuon[c];

            // う -> わ
            if (code == 1003)
            {
                code = 1091;
            }

            // u -> a
            else
            {
                code -= 2;
            }

            Causative = Present.Substring(0, len - 1) + VerbHandler.ongoju[code] + "せる";
        }

        protected override void SetPassive()
        {
            char c = Present[len - 1];
            int code = VerbHandler.gojuon[c];

            // う -> わ
            if (code == 1003)
            {
                code = 1091;
            }

            // u -> a
            else
            {
                code -= 2;
            }

            Passive = Present.Substring(0, len - 1) + VerbHandler.ongoju[code] + "れる";
        }

        protected override void SetCausativePassive()
        {
            char c = Present[len - 1];
            int code = VerbHandler.gojuon[c];

            // う -> わ
            if (code == 1003)
            {
                code = 1091;
            }

            // u -> a
            else
            {
                code -= 2;
            }

            CausativePassive = Present.Substring(0, len - 1) + VerbHandler.ongoju[code] + "される";
        }

        protected override void SetImperative()
        {
            char c = Present[len - 1];

            // u -> e
            int code = VerbHandler.gojuon[c] + 1;

            Imperative = Present.Substring(0, len - 1) + VerbHandler.ongoju[code];
        }

        protected override void SetPotential()
        {
            char c = Present[len - 1];

            // u -> e
            int code = VerbHandler.gojuon[c] + 1;

            Potential = Present.Substring(0, len - 1) + VerbHandler.ongoju[code] + "る";
        }

        protected override void SetVolitional()
        {
            char c = Present[len - 1];

            // u -> e
            int code = VerbHandler.gojuon[c] + 2;

            Volitional = Present.Substring(0, len - 1) + VerbHandler.ongoju[code] + "う";
        }
    }

    public class Verb2 : Verb
    {
        public static HashSet<string> special_case = new HashSet<string>();

        public Verb2(string present) : base(present, 2)
        {

        }

        protected override void SetTeForm()
        {
            TeForm = Present.Substring(0, len - 1) + "て";
        }

        protected override void SetKeigoPresent()
        {
            KeigoPresent = Present.Substring(0, len - 1) + "ます";
        }

        protected override void SetNegative()
        {
            Negative = Present.Substring(0, len - 1) + "ない";
        }

        protected override void SetCausative()
        {
            Causative = Present.Substring(0, len - 1) + "させる";
        }

        protected override void SetPassive()
        {
            Passive = Present.Substring(0, len - 1) + "られる";
        }

        protected override void SetCausativePassive()
        {
            CausativePassive = Present.Substring(0, len - 1) + "させられる";
        }

        protected override void SetImperative()
        {
            Imperative = Present.Substring(0, len - 1) + "ろ";
        }

        protected override void SetPotential()
        {
            Potential = Present.Substring(0, len - 1) + "られる";
        }

        protected override void SetVolitional()
        {
            Volitional = Present.Substring(0, len - 1) + "よう";
        }
    }

    public class Verb3 : Verb
    {
        public Verb3(string present) : base(present, 3)
        {

        }

        protected override void SetTeForm()
        {
            if (Present == "する")
            {
                TeForm = "して";
            }
            else
            {
                TeForm = "きて";
            }
        }

        protected override void SetKeigoPresent()
        {
            if (Present == "する")
            {
                KeigoPresent = "します";
            }
            else
            {
                KeigoPresent = "きます";
            }
        }

        protected override void SetNegative()
        {
            if (Present == "する")
            {
                Negative = "しない";
            }
            else
            {
                Negative = "こない";
            }
        }

        protected override void SetCausative()
        {
            if (Present == "する")
            {
                Causative = "させる";
            }
            else
            {
                Causative = "こさせる";
            }
        }

        protected override void SetPassive()
        {
            if (Present == "する")
            {
                Passive = "される";
            }
            else
            {
                Passive = "こられる";
            }
        }

        protected override void SetCausativePassive()
        {
            if (Present == "する")
            {
                CausativePassive = "させられる";
            }
            else
            {
                CausativePassive = "こさせられる";
            }
        }

        protected override void SetImperative()
        {
            if (Present == "する")
            {
                Imperative = "しろ";
            }
            else
            {
                Imperative = "こい";
            }
        }

        protected override void SetPotential()
        {
            if (Present == "する")
            {
                Potential = "できる";
            }
            else
            {
                Potential = "こられる";
            }
        }

        protected override void SetVolitional()
        {
            if (Present == "する")
            {
                Volitional = "しよう";
            }
            else
            {
                Volitional = "こよう";
            }
        }
    }
}
