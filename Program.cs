using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpAdvance
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentDemo3();

            Console.WriteLine("Press any to continue...");
            Console.ReadKey();
        }

        static void ParallelDemo()
        {
            int[] nums = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int sum = 0, sumEach = 0;

            // 使用 Parallel.For 方法計算陣列中所有元素的總和
            Parallel.For(0, nums.Length, i =>
            {
                // 在不同的執行緒上進行計算
                sum += nums[i];
            });

            Parallel.ForEach(nums, num =>
            {
                sumEach += num;
            });

            Console.WriteLine($"sum: {sum}, sumEach: {sumEach}");
        }

        static void TaskDemo()
        {
            // 使用 Task.Run 開始一個新的任務
            Task task1 = Task.Run(() => {
                Console.WriteLine("Task 1 is running on thread {0}", Thread.CurrentThread.ManagedThreadId);
            });

            // 使用 Task.Run 傳遞參數給任務
            string message = "Hello from task 2!";
            Task task2 = Task.Run(() => {
                Console.WriteLine(message);
            });

            // 等待兩個任務完成
            Task.WaitAll(task1, task2);

            Console.WriteLine("All tasks are completed.");
        }

        static void PLINQDemo1()
        {

            // 生成一個從 1 到 1000000 的整數序列
            int[] numbers = Enumerable.Range(1, 1000000).ToArray();

            // 使用 AsParallel 方法將數組轉換為 PLINQ 可處理的並行類型
            double average = numbers.AsParallel().Average();

            Console.WriteLine("Average = " + average);
        }

        static void PLINQDemo2()
        {
            int[] numbers = Enumerable.Range(1, 100).ToArray();

            var query = from n in numbers.AsParallel()
                        where n % 2 == 0
                        select n * n;

            foreach (var result in query)
            {
                Console.WriteLine(result);
            }

        }

        static void PLINQDemo3()
        {
            int[] numbers = Enumerable.Range(1, 1000000).ToArray();
            int count = 0;
            //int sum = numbers.AsParallel().Aggregate<int, int>(
            //    (int)0,
            //    (int partialSum, int i) => 
            //    {
            //        partialSum += i;
            //        return partialSum;
            //    },
            //    (int total, int subtotal) => total + subtotal
            //    );
            double result = numbers.Aggregate(
                0,
                (total, next) =>
                {
                    total = total + next;
                    count++;
                    return total;
                },
                total => (double)total / count
            );

            Console.WriteLine("result = " + result);

        }

        static void PLINQDemo4()
        {
            string[] words = { "apple", "ant", "banana", "cherry", "date", "eggplant", "elephant" };

            var groups = words.AsParallel().GroupBy(
                w => w[0],
                w => w);

            foreach (var group in groups)
            {
                Console.WriteLine(group.Key + ":");
                foreach (var word in group)
                {
                    Console.WriteLine("  " + word);
                }
            }
        }

        static void ConcurrentDemo1()
        {
            ConcurrentDictionary<string, int> counter = new ConcurrentDictionary<string, int>();

            // 計數器加 1
            counter.AddOrUpdate("count", 1, (key, oldValue) => oldValue + 1);

            // 輸出計數器的值
            Console.WriteLine($"Count = {counter["count"]}");

        }

        static void ConcurrentDemo2()
        {
            ConcurrentBag<int> numbers = new ConcurrentBag<int>();

            // 添加數字
            Parallel.For(0, 10, i => {
                numbers.Add(i);
            });

            // 輸出數字
            foreach (int num in numbers)
            {
                Console.WriteLine(num);
            }
        }

        static void ConcurrentDemo3()
        {
            ConcurrentQueue<string> queue = new ConcurrentQueue<string>();

            // 添加字串
            queue.Enqueue("Hello");
            queue.Enqueue("World");
            queue.Enqueue("!");

            // 移除字串
            string str;
            while (queue.TryDequeue(out str))
            {
                Console.WriteLine(str);
            }
        }
    }
}
