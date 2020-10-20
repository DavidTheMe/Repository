using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Ascii_bowling
{
    class Program
    {
        static void Draw_whole_Scoreboard (int[][] score_sheet, int[] total_score)
            {
            for (int i = 0; i < 10; i++)
            {
                if (i == 9)
                {
                    Console.SetCursorPosition(30, i * 4);
                    Console.Write("╠═╦═╬═╗"); Console.SetCursorPosition(30, 1 + (i * 4));
                    Console.Write("║ ║ ║ ║"); Console.SetCursorPosition(30, 2 + (i * 4));
                    Console.Write("╠═╩═╩═╣"); Console.SetCursorPosition(30, 3 + (i * 4));
                    Console.Write("║     ║"); Console.SetCursorPosition(30, 4 + (i * 4));
                    Console.Write("╚═════╝");
                }
                else if (i == 0)
                {
                    Console.SetCursorPosition(30, i * 4);
                    Console.Write("╔═╦═╗"); Console.SetCursorPosition(30, 1 + (i * 4));
                    Console.Write("║ ║ ║"); Console.SetCursorPosition(30, 2 + (i * 4));
                    Console.Write("╠═╩═╣"); Console.SetCursorPosition(30, 3 + (i * 4));
                    Console.Write("║   ║");
                }
                else
                {
                    Console.SetCursorPosition(30, i * 4);
                    Console.Write("╠═╦═╣"); Console.SetCursorPosition(30, 1 + (i * 4));
                    Console.Write("║ ║ ║"); Console.SetCursorPosition(30, 2 + (i * 4));
                    Console.Write("╠═╩═╣"); Console.SetCursorPosition(30, 3 + (i * 4));
                    Console.Write("║   ║");
                }

                int[] frameRolls = score_sheet[i];

                if (frameRolls != null)
                {
                    for (int j = 0; j < frameRolls.Length; j++)
                    {
                        Console.SetCursorPosition(31 + (j * 2),1 + i * 4);
                        Console.Write($"{frameRolls[j]}");
                    }
                    if (i > 0 && frameRolls.Length > 1)
                    {
                        if (total_score[i]! > total_score[i - 1])
                        {
                            Console.SetCursorPosition(31, 3 + i * 4);
                            Console.Write($"{total_score[i]}");
                        }
                    }
                    else if (i == 0 && frameRolls.Length == 2)
                    {
                        Console.SetCursorPosition(31, 3 + i * 4);
                        Console.Write($"{total_score[i]}");
                    }
                }
            }
        }
        static void Draw_pins(List<bool> pins_standing)
        {
            //Draw pins
            //First row
            Console.WriteLine("");

            if (pins_standing[0] == true)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" ");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write("  ");
            }

            if (pins_standing[1] == true)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" ");
            }
            else
                Console.Write("  ");

            if (pins_standing[2] == true)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" ");
            }
            else
                Console.Write("  ");

            if (pins_standing[3] == true)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(" ");
                Console.BackgroundColor = ConsoleColor.Black;
            }

            //Second row
            if (pins_standing[4] == true)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write(" ");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" ");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write("   ");
            }

            if (pins_standing[5] == true)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" ");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write("  ");
            }

            if (pins_standing[6] == true)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" ");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("  ");
            }

            //Third row
            if (pins_standing[7] == true)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" ");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write("    "); 
            }

            if (pins_standing[8] == true)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("  ");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("   ");
                Console.BackgroundColor = ConsoleColor.Black;
            }

            //Fourth row
            if (pins_standing[9] == true)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write("   ");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("   ");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("       ");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
        static void Knock_pin(List<bool> pins_standing, int pin_to_knock)
        {
            pins_standing[pin_to_knock] = false;
            var rand = new Random();

            Console.Clear();
            Draw_pins(pins_standing);
            Thread.Sleep(100);

            if (pin_to_knock == 9)
            {
                if (rand.Next(1, 3) == 1)
                    Knock_pin(pins_standing, pin_to_knock - 1);
                if (rand.Next(1, 3) == 1)
                    Knock_pin(pins_standing, pin_to_knock - 2);
            }
            else if (pin_to_knock == 7 || pin_to_knock == 8)
            {
                if (rand.Next(1, 3) == 1)
                    Knock_pin(pins_standing, pin_to_knock - 2);
                if (rand.Next(1, 3) == 1)
                    Knock_pin(pins_standing, pin_to_knock - 3);
            }
            else if (pin_to_knock > 3 && pin_to_knock < 7)
            {
                if (rand.Next(1, 3) == 1)
                    Knock_pin(pins_standing, pin_to_knock - 3);
                if (rand.Next(1, 3) == 1)
                    Knock_pin(pins_standing, pin_to_knock - 4);
            }
        }
        static void Draw_minigame(int pos)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("       ");
            for (int i = 0; i < pos - 1; i++)
            {
                Console.Write(" ");
            }
            Console.Write("^");
            for (int i = 0; i < 7 - pos; i++)
            {
                Console.Write(" ");
            }
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();
            Console.WriteLine("   .   ");
            Console.WriteLine("  . .  ");
            Console.WriteLine(" .   . ");
            Console.WriteLine(".     .");
            Console.WriteLine("       ");
            Console.WriteLine("       ");
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(0, 11);
            for (int i = 0; i < pos - 1; i++)
            {
                Console.Write("─");
            }
            Console.Write("O");
            for (int i = 0; i < 7 - pos; i++)
            {
                Console.Write("─");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(40, 45);
            Console.SetBufferSize(40, 45);

            //variables
            int roll_number = 0;
            int final_score = 0;
            var pins_standing = new List<bool>();
            for (int i = 0; i < 10; i++)
                pins_standing.Add(true);

            int[][] score_sheet = new int[10][];
            int[] points_gained = new int[10]
            { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
            int[] total_score = new int[10]
            { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};

            var random = new Random();

            //Loop set
            for (int frame_index = 0; frame_index < 10; frame_index++)
            {
                for (int i = 0; i < 10; i++)
                {
                    pins_standing[i] = true;
                }

                //Draw pins
                Draw_pins(pins_standing);
                Draw_whole_Scoreboard(score_sheet, total_score);

                //Do 2 rolls
                for (int roll_index = 0; roll_index < 2; roll_index++)
                {
                    int pins_knocked = 0;

                    //Roll
                    //Enter where to aim
                    /*
                    Console.WriteLine("");
                    Console.WriteLine("Enter a number between 1 and 7 to choose where to aim");
                    roll_number = int.Parse(Console.ReadLine());
                    Console.Clear();
                    */

                    //Choose position mini game
                    int pos = random.Next(1, 7);
                    int dir = 1;

                    for (int n = 0; n < 100; n++)
                    {
                        if (dir == 1)
                        {
                            if (pos + 1 == 8)
                            {
                                dir = 0;
                                Draw_minigame(pos);
                            }
                            else
                            {
                                pos++;
                                Draw_minigame(pos);
                            }
                        }
                        else if (dir == 0)
                        {
                            if (pos - 1 == 0)
                            {
                                dir = 1;
                                Draw_minigame(pos);
                            }
                            else
                            {
                                pos--;
                                Draw_minigame(pos);
                            }
                        }
                        //Sleep
                        int amount_to_sleep = (pos - 4) * 3 * 20;

                        if (amount_to_sleep > 0)
                            Thread.Sleep(amount_to_sleep);
                        else if (amount_to_sleep == 0)
                            Thread.Sleep(30);
                        else if (amount_to_sleep < 0)
                            Thread.Sleep((amount_to_sleep - amount_to_sleep) - amount_to_sleep);

                        //Button pressed
                        if (Console.KeyAvailable)
                        {
                            Console.ReadKey();
                            break;
                        }
                    }
                    roll_number = pos;

                    //Knock pins
                    if (roll_number == 1)
                    {
                        Knock_pin(pins_standing, 0);
                    }
                    if (roll_number == 2)
                    {
                        Knock_pin(pins_standing, 4);
                    }
                    if (roll_number == 3)
                    {
                        if (pins_standing[7] == true)
                            Knock_pin(pins_standing, 7);
                        else if (pins_standing[1] == true)
                            Knock_pin(pins_standing, 1);
                    }
                    if (roll_number == 4)
                    {
                        if (pins_standing[9] == true)
                            Knock_pin(pins_standing, 9);
                        else if (pins_standing[5] == true)
                            Knock_pin(pins_standing, 5);
                    }
                    if (roll_number == 5)
                    {
                        if (pins_standing[8] == true)
                            Knock_pin(pins_standing, 8);
                        else if (pins_standing[2] == true)
                            Knock_pin(pins_standing, 2);
                    }
                    if (roll_number == 6)
                    {
                        Knock_pin(pins_standing, 6);
                    }
                    if (roll_number == 7)
                    {
                        Knock_pin(pins_standing, 3);
                    }

                    //Give score
                    for (int k = 0; k < 10; k++)
                    {
                        if (pins_standing[k] == false)
                            pins_knocked += 1;
                    }

                    if (roll_index == 0)
                    {
                        score_sheet[frame_index] = new int[1] { pins_knocked };
                    }
                    else if (roll_index == 1)
                    {
                        int first_roll = score_sheet[frame_index][0];
                        int second_roll = pins_knocked - first_roll;
                        score_sheet[frame_index] = new int[2] { first_roll, second_roll };
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
                                        if (score_sheet[i + 1].Length > 1)
                                        {
                                            points_gained[i] = 10 + score_sheet[i + 1][0] + score_sheet[i + 1][1];
                                        }
                                    }
                                }
                            }
                            else if (score_sheet[i].Length == 2)
                            {
                                if (score_sheet[i][0] + score_sheet[i][1] == 10)
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
                    }

                    //10th frame
                    if (score_sheet[9] != null)
                    {
                        if (score_sheet[9].Length == 2)
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

                    //Draw stuff
                    Console.Clear();
                    Draw_pins(pins_standing);
                    Draw_whole_Scoreboard(score_sheet, total_score);
                }

                //Press enter text
                Console.WriteLine("");
                Console.WriteLine("Press Enter to reset the pins");
                Console.ReadLine();
                Console.Clear();
            }
            Console.WriteLine($"Your final score was {total_score[9]}!");
        }
    }
}