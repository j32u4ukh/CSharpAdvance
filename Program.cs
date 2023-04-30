using System;
namespace CSharpAdvance
{
    public class Demo
    {
        public static void GuessANumber()
        {
            Random random = new Random();
            int min = 1, max = 100;
            int guess = 0 , target = random.Next(min, max + 1);

            while (guess != target)
            {
                Console.Write($"Guess a number({min}-{max}):");

                if(int.TryParse(Console.ReadLine().Trim(), out guess))
                {
                    if(guess < target)
                    {
                        Console.WriteLine("Too low...");
                        min = Math.Max(min, guess);
                    }
                    else if(guess > target)
                    {
                        Console.WriteLine("Too high...");
                        max = Math.Min(max, guess);
                    }
                    else
                    {
                        Console.WriteLine("Correct!!");
                    }
                }
            }

            Console.Write("Press any key to exit...");
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Demo.GuessANumber();
            Console.ReadKey();
        }
    }
}
