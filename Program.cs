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
        }

        public static void RockPaperScissors()
        {
            Random random = new Random();
            string input;
            int rps, wins = 0, draws = 0, losses = 0;

            do
            {
                // 1: rock, 2: paper, 3: scissors
                Console.Write("Choose [r]ock, [p]aper, [s]cissors, or [e]xit:");
                input = Console.ReadLine().Trim();
                rps = random.Next(1, 4);

                switch (input)
                {
                    case "r":
                        rps += 10;
                        break;
                    case "p":
                        rps += 20;
                        break;
                    case "s":
                        rps += 30;
                        break;
                    case "e":
                        Console.Clear();
                        goto EndOfGame;
                    default:
                        break;
                }

                switch (rps)
                {
                    case 11:
                    case 22:
                    case 33:
                        Console.WriteLine("This game was a draw.");
                        draws++;
                        break;
                    case 12:
                    case 23:
                    case 31:
                        Console.WriteLine("You lose.");
                        losses++;
                        break;
                    case 13:
                    case 32:
                    case 21:
                        Console.WriteLine("You win.");
                        wins++;
                        break;
                    default:
                        break;
                }

                Console.WriteLine($"Score: {wins} wins, {losses} losses, {draws} draws");
                EndOfGame:;
            }
            while (input != "e");
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Demo.RockPaperScissors();

            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
