using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();

            Console.WriteLine("");

            Console.WriteLine("");

            Console.WriteLine("░░░░▓▓▓▓▓");

            Console.WriteLine("░░░▓▒▒▒▒▒▓");

            Console.WriteLine("░░▓▒▓▒▒▒▓▒▓");

            Console.WriteLine("░░▓▒▒▒▒▒▒▒▓");

            Console.WriteLine("░░▓▒▓▒▒▒▓▒▓");

            Console.WriteLine("░░▓▒▒▓▓▓▒▒▓");

            Console.WriteLine("░░░▓▒▒▒▒▒▓");

            Console.WriteLine("░░░░▓▓▓▓▓");

            int roll1 = random.Next(1, 7);
            int roll2 = random.Next(1, 7);
            int roll3 = random.Next(1, 7);
            if (roll1 == 1)
            {
                Console.WriteLine("┌─────┐");
                Console.WriteLine("│     │");
                Console.WriteLine("│     │");
                Console.WriteLine("│  O  │");
                Console.WriteLine("│     │");
                Console.WriteLine("│     │");
                Console.WriteLine("└─────┘");
            }
            if (roll1 == 2)
            {
                Console.WriteLine("┌─────┐");
                Console.WriteLine("│ O   │");
                Console.WriteLine("│     │");
                Console.WriteLine("│     │");
                Console.WriteLine("│     │");
                Console.WriteLine("│   O │");
                Console.WriteLine("└─────┘");
            }
            if (roll1 == 3)
            {
                Console.WriteLine("┌─────┐");
                Console.WriteLine("│ O   │");
                Console.WriteLine("│     │");
                Console.WriteLine("│  O  │");
                Console.WriteLine("│     │");
                Console.WriteLine("│   O │");
                Console.WriteLine("└─────┘");
            }
            if (roll1 == 4)
            {
                Console.WriteLine("┌─────┐");
                Console.WriteLine("│ O O │");
                Console.WriteLine("│     │");
                Console.WriteLine("│     │");
                Console.WriteLine("│     │");
                Console.WriteLine("│ O O │");
                Console.WriteLine("└─────┘");
            }
            if (roll1 == 5)
            {
                Console.WriteLine("┌─────┐");
                Console.WriteLine("│ O O │");
                Console.WriteLine("│     │");
                Console.WriteLine("│  O  │");
                Console.WriteLine("│     │");
                Console.WriteLine("│ O O │");
                Console.WriteLine("└─────┘");
            }
            if (roll1 == 6)
            {
                Console.WriteLine("┌─────┐");
                Console.WriteLine("│ O O │");
                Console.WriteLine("│     │");
                Console.WriteLine("│ O O │");
                Console.WriteLine("│     │");
                Console.WriteLine("│ O O │");
                Console.WriteLine("└─────┘");
            }
            if (roll2 == 1)
            {
                Console.WriteLine("┌─────┐");
                Console.WriteLine("│     │");
                Console.WriteLine("│     │");
                Console.WriteLine("│  O  │");
                Console.WriteLine("│     │");
                Console.WriteLine("│     │");
                Console.WriteLine("└─────┘");
            }
            if (roll2 == 2)
            {
                Console.WriteLine("┌─────┐");
                Console.WriteLine("│ O   │");
                Console.WriteLine("│     │");
                Console.WriteLine("│     │");
                Console.WriteLine("│     │");
                Console.WriteLine("│   O │");
                Console.WriteLine("└─────┘");
            }
            if (roll2 == 3)
            {
                Console.WriteLine("┌─────┐");
                Console.WriteLine("│ O   │");
                Console.WriteLine("│     │");
                Console.WriteLine("│  O  │");
                Console.WriteLine("│     │");
                Console.WriteLine("│   O │");
                Console.WriteLine("└─────┘");
            }
            if (roll2 == 4)
            {
                Console.WriteLine("┌─────┐");
                Console.WriteLine("│ O O │");
                Console.WriteLine("│     │");
                Console.WriteLine("│     │");
                Console.WriteLine("│     │");
                Console.WriteLine("│ O O │");
                Console.WriteLine("└─────┘");
            }
            if (roll2 == 5)
            {
                Console.WriteLine("┌─────┐");
                Console.WriteLine("│ O O │");
                Console.WriteLine("│     │");
                Console.WriteLine("│  O  │");
                Console.WriteLine("│     │");
                Console.WriteLine("│ O O │");
                Console.WriteLine("└─────┘");
            }
            if (roll2 == 6)
            {
                Console.WriteLine("┌─────┐");
                Console.WriteLine("│ O O │");
                Console.WriteLine("│     │");
                Console.WriteLine("│ O O │");
                Console.WriteLine("│     │");
                Console.WriteLine("│ O O │");
                Console.WriteLine("└─────┘");
            }
            if (roll3 == 1)
            {
                Console.WriteLine("┌─────┐");
                Console.WriteLine("│     │");
                Console.WriteLine("│     │");
                Console.WriteLine("│  O  │");
                Console.WriteLine("│     │");
                Console.WriteLine("│     │");
                Console.WriteLine("└─────┘");
            }
            if (roll3 == 2)
            {
                Console.WriteLine("┌─────┐");
                Console.WriteLine("│ O   │");
                Console.WriteLine("│     │");
                Console.WriteLine("│     │");
                Console.WriteLine("│     │");
                Console.WriteLine("│   O │");
                Console.WriteLine("└─────┘");
            }
            if (roll3 == 3)
            {
                Console.WriteLine("┌─────┐");
                Console.WriteLine("│ O   │");
                Console.WriteLine("│     │");
                Console.WriteLine("│  O  │");
                Console.WriteLine("│     │");
                Console.WriteLine("│   O │");
                Console.WriteLine("└─────┘");
            }
            if (roll3 == 4)
            {
                Console.WriteLine("┌─────┐");
                Console.WriteLine("│ O O │");
                Console.WriteLine("│     │");
                Console.WriteLine("│     │");
                Console.WriteLine("│     │");
                Console.WriteLine("│ O O │");
                Console.WriteLine("└─────┘");
            }
            if (roll3 == 5)
            {
                Console.WriteLine("┌─────┐");
                Console.WriteLine("│ O O │");
                Console.WriteLine("│     │");
                Console.WriteLine("│  O  │");
                Console.WriteLine("│     │");
                Console.WriteLine("│ O O │");
                Console.WriteLine("└─────┘");
            }
            if (roll3 == 6)
            {
                Console.WriteLine("┌─────┐");
                Console.WriteLine("│ O O │");
                Console.WriteLine("│     │");
                Console.WriteLine("│ O O │");
                Console.WriteLine("│     │");
                Console.WriteLine("│ O O │");
                Console.WriteLine("└─────┘");
            }

            Console.WriteLine("");
            Console.WriteLine($"Score = {roll1 + roll2 + roll3}");
        }
    }
}