using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

namespace list_tutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mission 1:");
            Console.WriteLine();

            //variables and lists

            var rand = new Random();
            int roll1 = 0;
            int roll2 = 0;
            int roll3 = 0;
            int roll4 = 0;

            var scores = new List<string> {};
            int finalscore;
            var rolls = new List<string> {};

            for (int i = 0; i < 10; i++)
            {
                //Add random rolls
                roll1 = rand.Next(1, 7);
                roll2 = rand.Next(1, 7);
                roll3 = rand.Next(1, 7);
                roll4 = rand.Next(1, 7);

                //Add rolls to list
                rolls.Add($"{roll1}");
                rolls.Add($"{roll2}");
                rolls.Add($"{roll3}");
                rolls.Add($"{roll4}");

                Console.WriteLine("You roll:");

                foreach (var roll in rolls)
                {
                    Console.WriteLine(roll.ToUpper());
                }

                //Calculate and add final scores
                finalscore = roll1 + roll2 + roll3 + roll4;
                Console.WriteLine($"Ability score is: {finalscore}");
                scores.Add($"{finalscore}");

                //Remove rolls from list
                rolls.Remove($"{roll1}");
                rolls.Remove($"{roll2}");
                rolls.Remove($"{roll3}");
                rolls.Remove($"{roll4}");
                Console.WriteLine("______________");
                Console.WriteLine();
            }
            Console.WriteLine("Your ability points are:");
            scores.ForEach(Console.WriteLine);
        }
    }
}