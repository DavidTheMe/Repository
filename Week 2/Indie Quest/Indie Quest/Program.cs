using System;

namespace Indie_Quest
{
    class Program
    {
        int Main(string[] args)
        {
            var random = new Random();
            int Whp = random.Next(1, 101);
            Console.WriteLine($"Warrior has {Whp} HP");
            Console.WriteLine("");

            Console.WriteLine("A spell is cast!");

                Whp = Whp + 5;
                Console.WriteLine($"Warrior hp = {Whp}");
        }
    }
}
