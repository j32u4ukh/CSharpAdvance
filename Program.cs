using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace CSharpAdvance
{
    struct Data
    {
        public string Name;
        public int Age;
        public string Address;
        public string City;
        public string State;
        public string Zip;
    }

    class Program
    {
        static int Main(string[] args)
        {
            int returnValue = 0;

            // 構造要輸出的 JSON 數據
            var data = new Data()
            {
                Name = "John Doe",
                Age = 30,
                Address = "123 Main St",
                City = "Anytown",
                State = "CA",
                Zip = "12345"
            };

            // 將 JSON 數據序列化並輸出到標準輸出流
            string json = JsonConvert.SerializeObject(data);
            Console.WriteLine(json);

            return returnValue;
        }

        static void Read()
        {
            Console.WriteLine("Hello world!");
            ProcessStartInfo startInfo = new ProcessStartInfo(@"C:\Users\PC\source\repos\ConsoleApp\CSharpAdvance\bin\Debug\CSharpAdvance.exe");
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                int returnValue = process.ExitCode;

                // 將 JSON 數據解析為對象
                Data data = (Data)JsonConvert.DeserializeObject(output, typeof(Data));

                Console.WriteLine("Name: " + data.Name);
                Console.WriteLine("Age: " + data.Age);
                Console.WriteLine("Address: " + data.Address);
                Console.WriteLine("City: " + data.City);
                Console.WriteLine("State: " + data.State);
                Console.WriteLine("Zip: " + data.Zip);
                Console.WriteLine("Return value: " + returnValue);
            }
        }
    }
}
