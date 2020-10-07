using System;
using System.Collections.Generic;
using System.Threading;

namespace Help_a_newcomer_learn_C_sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new List<int>();

            var random = new Random();

            for (int i = 0; i < 70; i++)
            {
                data.Add(random.Next(20));
                DisplayData(data);
            }
        }

        static void DisplayData(List<int> data)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            for (int y = 20; y >= 0; y--)
            {
                if (y % 5 == 0)
                {
                    Console.Write($"{y,3} |");
                }
                else
                {
                    Console.Write("    |");
                }

                for (int x = 0; x < data.Count; x++)
                {
                    if (y == 0)
                    {
                        Console.Write("-");
                        continue;
                    }

                    Console.Write(y <= data[x] ? "\u2592" : " ");
                }

                Console.WriteLine();
            }

            Thread.Sleep(10);
        }
    }
}
