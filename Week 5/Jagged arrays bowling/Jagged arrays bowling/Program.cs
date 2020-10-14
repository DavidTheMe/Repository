using System;

namespace Jagged_arrays_bowling
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variables
            var rand = new Random();

            int first_roll = 0;
            int second_roll = 0;

            int[][] score_sheet = new int[10][];
            int[] points_gained = new int[10];
            int[] total_score = new int[10];

            //Add scores
            for (int i = 0; i < 10; i++)
            {
                first_roll = 0;
                second_roll = 0;

                first_roll = rand.Next(0, 11);

                if (first_roll == 10)
                { 
                    score_sheet[i] = new int[1] { first_roll }; 
                }
                else
                {
                    second_roll = rand.Next(0, 11 - first_roll);
                    score_sheet[i] = new int[2] { first_roll, second_roll };
                }
                if (i == 10 && first_roll + second_roll == 10)
                {
                    int third_roll = rand.Next(0, 11 - first_roll);
                    score_sheet[i] = new int[3] { first_roll, second_roll, third_roll };
                    points_gained[i] = first_roll + second_roll + third_roll;
                }
                else
                    points_gained[i] = first_roll + second_roll;
                
                if (i == 0)
                {
                    total_score[i] = points_gained[i];
                }
                else
                {
                    total_score[i] = total_score[i - 1] + points_gained[i];
                }
            }

            //Draw the score
            for (int i = 0; i < 10; i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write($"Frame {i + 1}:");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
                for (int j = 0; j < score_sheet[i].Length; j++)
                {
                    Console.WriteLine($"Roll {j + 1}: {score_sheet[i][j]}");
                }
                Console.WriteLine($"Score gained: {points_gained[i]}");
                Console.WriteLine();
                Console.WriteLine($"Total score: {total_score[i]}");
                Console.WriteLine();
                Console.WriteLine();
            }

            //Draw the scoreboard
            for (int i = 0; i < 10; i++)
            {
                if (i == 9)
                {
                    Console.SetCursorPosition(30, 2 + (i * 4));
                    Console.Write("▓▓▓▓▓▓▓"); Console.SetCursorPosition(30, 3 + (i * 4));
                    Console.Write("▓-▓-▓-▓"); Console.SetCursorPosition(30, 4 + (i * 4));
                    Console.Write("▓▓▓▓▓▓▓"); Console.SetCursorPosition(30, 5 + (i * 4));
                    Console.Write("▓     ▓"); Console.SetCursorPosition(30, 6 + (i * 4));
                    Console.Write("▓▓▓▓▓▓▓");
                }
                else
                {
                    Console.SetCursorPosition(30, 2 + (i * 4));
                    Console.Write("▓▓▓▓▓"); Console.SetCursorPosition(30, 3 + (i * 4));
                    Console.Write("▓-▓-▓"); Console.SetCursorPosition(30, 4 + (i * 4));
                    Console.Write("▓▓▓▓▓"); Console.SetCursorPosition(30, 5 + (i * 4));
                    Console.Write("▓   ▓");
                }

                for (int j = 0; j <  score_sheet[i].Length; j++)
                {
                    Console.SetCursorPosition(31 + (j * 2), 3 + (i * 4));
                    Console.Write($"{score_sheet[i][j]}");
                }
                if (total_score[i] < 10)
                    Console.SetCursorPosition(32, 5 + (i * 4));
                else if (total_score[i] > 9)
                    Console.SetCursorPosition(31, 5 + (i * 4));
                Console.Write(total_score[i]);
            }
            Console.SetCursorPosition(0, 100);
        }
    }
}
