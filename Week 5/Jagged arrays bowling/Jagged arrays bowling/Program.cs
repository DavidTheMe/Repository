using System;

namespace Jagged_arrays_bowling
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(40, 45);
            Console.SetBufferSize(40, 45);

            //Variables
            var rand = new Random();

            int first_roll;
            int second_roll;

            int[][] score_sheet = new int[10][];
            int[] points_gained = new int[10]
            { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
            int[] total_score = new int[10]
            { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};

            //Add scores
            for (int current_frame = 0; current_frame < 10; current_frame++)
            {
                first_roll = rand.Next(0, 11);
                if (current_frame < 9)
                {
                    if (first_roll == 10)
                    {
                        score_sheet[current_frame] = new int[1] { first_roll };
                    }
                    else
                    {
                        second_roll = rand.Next(0, 11 - first_roll);
                        score_sheet[current_frame] = new int[2] { first_roll, second_roll };
                    }
                }
                else
                {
                    int third_roll;
                    if (first_roll == 10)
                    {
                        second_roll = rand.Next(0, 11);
                        if (second_roll == 10)
                        {
                            third_roll = rand.Next(0, 11);
                        }
                        else
                        {
                            third_roll = rand.Next(0, 11 - second_roll);
                        }
                        score_sheet[current_frame] = new int[3] { first_roll, second_roll, third_roll };
                    }
                    else
                    {
                        second_roll = rand.Next(0, 11 - first_roll);
                        if (first_roll + second_roll == 10)
                        {
                            third_roll = rand.Next(0, 11);
                            score_sheet[current_frame] = new int[3] { first_roll, second_roll, third_roll };
                        }
                        else
                        {
                            score_sheet[current_frame] = new int[2] { first_roll, second_roll }; 
                        }
                    }
                }

                //Calculate points gained
                for (int i = 0; i < 9; i++)
                {
                    if (score_sheet[i] != null)
                    {
                        if (score_sheet[i][0] == 10)
                        {
                            //Strike
                            if (score_sheet[i + 1] != null)
                            {
                                if (score_sheet[i + 1][0] == 10 && i < 8)
                                {
                                    //2 strikes in a row
                                    if (score_sheet[i + 2] != null)
                                    {
                                        points_gained[i] = 20 + score_sheet[i + 2][0];
                                    }
                                }
                                else
                                {
                                    //1 strike
                                    points_gained[i] = 10 + score_sheet[i + 1][0] + score_sheet[i + 1][1];
                                }
                            }
                        }
                        else if (score_sheet[i][0] + score_sheet[i][1] == 10)
                        {
                            //Spare
                            if (score_sheet[i + 1] != null)
                            {
                                points_gained[i] = 10 + score_sheet[i + 1][0];
                            }
                        }
                        else
                        {
                            //Normal
                            points_gained[i] = score_sheet[i][0] + score_sheet[i][1];
                        }
                    }
                }

                //10th frame
                if (score_sheet[9] != null)
                {
                    points_gained[9] = score_sheet[9][0] + score_sheet[9][1];

                    if (score_sheet[9].Length == 3)
                    {
                        points_gained[9] += score_sheet[9][2];
                    }
                }
                //Calculate total score
                for (int i = 0; i < 10; i++)
                {
                        total_score[i] = points_gained[i];
                        if (i != 0)
                            total_score[i] += total_score[i - 1];
                }

                //Draw the score
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write($"Frame {current_frame + 1}:");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();

                for (int j = 0; j < score_sheet[current_frame].Length; j++)
                {
                    Console.WriteLine($"Roll {j + 1}: {score_sheet[current_frame][j]}");
                }

                if (points_gained[current_frame] != -1)
                {
                    Console.WriteLine($"Score gained: {points_gained[current_frame]}");
                    Console.WriteLine();
                    Console.WriteLine($"Total score: {total_score[current_frame]}");
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
                        Console.Write("▓ ▓ ▓ ▓"); Console.SetCursorPosition(30, 4 + (i * 4));
                        Console.Write("▓▓▓▓▓▓▓"); Console.SetCursorPosition(30, 5 + (i * 4));
                        Console.Write("▓     ▓"); Console.SetCursorPosition(30, 6 + (i * 4));
                        Console.Write("▓▓▓▓▓▓▓");
                    }
                    else
                    {
                        Console.SetCursorPosition(30, 2 + (i * 4));
                        Console.Write("▓▓▓▓▓"); Console.SetCursorPosition(30, 3 + (i * 4));
                        Console.Write("▓ ▓ ▓"); Console.SetCursorPosition(30, 4 + (i * 4));
                        Console.Write("▓▓▓▓▓"); Console.SetCursorPosition(30, 5 + (i * 4));
                        Console.Write("▓   ▓");
                    }

                    if (score_sheet[i] != null)
                    {
                        for (int j = 0; j < score_sheet[i].Length; j++)
                        {
                            Console.SetCursorPosition(31 + (j * 2), 3 + (i * 4));
                            if (score_sheet[i][0] == 10)
                            {
                                Console.Write($"S");
                                Console.SetCursorPosition(31 + (1 * 2), 3 + (i * 4));
                                Console.Write(" ");
                            }
                            else if (score_sheet[i][0] + score_sheet[i][1] == 10 && j == 1)
                            {
                                Console.Write($"/");
                            }
                            else
                            {
                                Console.Write($"{score_sheet[i][j]}");
                            }
                        }
                    }
                    if (points_gained[i] != -1)
                    {
                        if (total_score[i] < 10)
                            Console.SetCursorPosition(32, 5 + (i * 4));
                        else if (total_score[i] > 9)
                            Console.SetCursorPosition(31, 5 + (i * 4));
                        Console.Write(total_score[i]);
                    }
                }
                Console.ReadKey(false);
            }
        }
    }
}
