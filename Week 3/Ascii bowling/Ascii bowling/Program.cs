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
            for (int current_box = 0; current_box < 10; current_box++)
            {
                if (current_box == 9)
                {
                    Console.SetCursorPosition(30, current_box * 4);
                    Console.Write("╠═╦═╬═╗"); Console.SetCursorPosition(30, 1 + (current_box * 4));
                    Console.Write("║ ║ ║ ║"); Console.SetCursorPosition(30, 2 + (current_box * 4));
                    Console.Write("╠═╩═╩═╣"); Console.SetCursorPosition(30, 3 + (current_box * 4));
                    Console.Write("║     ║"); Console.SetCursorPosition(30, 4 + (current_box * 4));
                    Console.Write("╚═════╝");
                }
                else if (current_box == 0)
                {
                    Console.SetCursorPosition(30, current_box * 4);
                    Console.Write("╔═╦═╗"); Console.SetCursorPosition(30, 1 + (current_box * 4));
                    Console.Write("║ ║ ║"); Console.SetCursorPosition(30, 2 + (current_box * 4));
                    Console.Write("╠═╩═╣"); Console.SetCursorPosition(30, 3 + (current_box * 4));
                    Console.Write("║   ║");
                }
                else
                {
                    Console.SetCursorPosition(30, current_box * 4);
                    Console.Write("╠═╦═╣"); Console.SetCursorPosition(30, 1 + (current_box * 4));
                    Console.Write("║ ║ ║"); Console.SetCursorPosition(30, 2 + (current_box * 4));
                    Console.Write("╠═╩═╣"); Console.SetCursorPosition(30, 3 + (current_box * 4));
                    Console.Write("║   ║");
                }

                int[] frameRolls = score_sheet[current_box];

                if (frameRolls != null)
                {
                    for (int j = 0; j < frameRolls.Length; j++)
                    {
                        int roll = frameRolls[j];
                        string symbol = roll.ToString();

                        if (roll == 0)
                        {
                            symbol = "-";
                        }

                        //first roll
                        if (j == 0)
                        {
                            if (roll == 10)
                            {
                                symbol = "X";
                            }
                        }
                        //second roll
                        else if (j == 1)
                        {
                            if (current_box == 9)
                            {
                                if (frameRolls[0] == 10 && roll == 10)
                                {
                                    symbol = "X";
                                }
                                else if (frameRolls[0] + frameRolls[1] == 10 && frameRolls[0] != 10)
                                {
                                    symbol = "/";
                                }
                            }
                            else 
                            {
                                
                                if (frameRolls[0] + frameRolls[1] == 10)
                                {
                                    symbol = "/";
                                }

                            }
                        }
                        //third roll
                        else
                        {
                            if (roll == 10)
                            {
                                bool spare = frameRolls[0] != 10 && frameRolls[0] + frameRolls[1] == 10;
                                bool twoStrikes = frameRolls[0] == 10 && frameRolls[1] == 10;

                                if (spare || twoStrikes)
                                {
                                    symbol = "X";
                                }
                            }

                            if (frameRolls[0] == 10 && frameRolls[1] + frameRolls[2] == 10)
                            {
                                symbol = "/";
                            }
                        }

                        Console.SetCursorPosition(31 + (j * 2),1 + current_box * 4);
                        Console.Write($"{symbol}");
                    }
                    
                    if (current_box > 0)
                    {
                        if (total_score[current_box] !> total_score[current_box - 1])
                        {
                            Console.SetCursorPosition(31, 3 + current_box * 4);
                            Console.Write($"{total_score[current_box]}");
                        }
                    }
                    else if (current_box == 0 && frameRolls.Length == 2 && total_score[current_box] != -1)
                    {
                        Console.SetCursorPosition(31, 3 + current_box * 4);
                        Console.Write($"{total_score[current_box]}");
                    }
                }
            }
        }
        static void Draw_pins(List<bool> pins_standing)
        {
            //Draw pins
            //First row

            Console.SetCursorPosition(0, 1);

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
        static void Knock_pin(List<bool> pins_standing, int pin_to_knock, int strength)
        {
            pins_standing[pin_to_knock] = false;
            var rand = new Random();

            double chance = Math.Pow((7 - strength) / 6.0, 2.0);

            Console.Clear();
            Draw_pins(pins_standing);
            Thread.Sleep(100);

            if (pin_to_knock == 9)
            {
                if (rand.NextDouble() < chance)
                    Knock_pin(pins_standing, pin_to_knock - 1, strength);
                if (rand.NextDouble() < chance)
                    Knock_pin(pins_standing, pin_to_knock - 2, strength);
            }
            else if (pin_to_knock == 7 || pin_to_knock == 8)
            {
                if (rand.NextDouble() < chance)
                    Knock_pin(pins_standing, pin_to_knock - 2, strength);
                if (rand.NextDouble() < chance)
                    Knock_pin(pins_standing, pin_to_knock - 3, strength);
            }
            else if (pin_to_knock > 3 && pin_to_knock < 7)
            {
                if (rand.NextDouble() < chance)
                    Knock_pin(pins_standing, pin_to_knock - 3, strength);
                if (rand.NextDouble() < chance)
                    Knock_pin(pins_standing, pin_to_knock - 4, strength);
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
        static void Do_strength_minigame(out int strength)
        {
            var random = new Random();
            
            //Choose position mini game
            strength = random.Next(1, 7);
            int dir = random.Next(0, 2);

            for (int n = 0; n < 100; n++)
            {
                if (dir == 1)
                {
                    if (strength + 1 == 8)
                    {
                        dir = 0;
                        Draw_strength_minigame(strength);
                    }
                    else
                    {
                        strength++;
                        Draw_strength_minigame(strength);
                    }
                }
                else if (dir == 0)
                {
                    if (strength - 1 == 0)
                    {
                        dir = 1;
                        Draw_strength_minigame(strength);
                    }
                    else
                    {
                        strength--;
                        Draw_strength_minigame(strength);
                    }
                }
                //Sleep
                int amount_to_sleep = strength * 2 * 17;

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
        }
        static void Draw_strength_minigame(int strength)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("       ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("       ");
            Console.WriteLine("       ");
            Console.WriteLine("   .   ");
            Console.WriteLine("  . .  ");
            Console.WriteLine(" .   . ");
            Console.WriteLine(".     .");
            Console.WriteLine("       ");
            Console.WriteLine("       ");
            Console.ForegroundColor = ConsoleColor.White;

            Console.BackgroundColor = ConsoleColor.Red;
            for (int i = 1; i < 8; i++)
            {
                Console.SetCursorPosition(9, i);
                if (i == strength)
                    Console.Write("██");
                else
                    Console.Write("░░");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        static void Do_roll(List<bool> pins_standing, int[][] score_sheet, int[] total_score, int[] points_gained, int frame_index, int roll_index)
        {
            int strength;
            var random = new Random();
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
            int dir = random.Next(0, 2);

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
                    Thread.Sleep(60);
                else if (amount_to_sleep < 0)
                    Thread.Sleep((amount_to_sleep - amount_to_sleep) - amount_to_sleep);

                //Button pressed
                if (Console.KeyAvailable)
                {
                    Console.ReadKey();
                    break;
                }

            }
            int roll_number = pos;

            Do_strength_minigame(out strength);

            //Knock pins
            if (roll_number == 1)
            {
                Knock_pin(pins_standing, 0, strength);
            }
            if (roll_number == 2)
            {
                Knock_pin(pins_standing, 4, strength);
            }
            if (roll_number == 3)
            {
                if (pins_standing[7] == true)
                    Knock_pin(pins_standing, 7, strength);
                else if (pins_standing[1] == true)
                    Knock_pin(pins_standing, 1, strength);
            }
            if (roll_number == 4)
            {
                if (pins_standing[9] == true)
                    Knock_pin(pins_standing, 9, strength);
                else if (pins_standing[5] == true)
                    Knock_pin(pins_standing, 5, strength);
            }
            if (roll_number == 5)
            {
                if (pins_standing[8] == true)
                    Knock_pin(pins_standing, 8, strength);
                else if (pins_standing[2] == true)
                    Knock_pin(pins_standing, 2, strength);
            }
            if (roll_number == 6)
            {
                Knock_pin(pins_standing, 6, strength);
            }
            if (roll_number == 7)
            {
                Knock_pin(pins_standing, 3, strength);
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

                int second_roll = pins_knocked;

                if (first_roll != 10)
                {
                    second_roll -= first_roll;
                }
                
                score_sheet[frame_index] = new int[2] { first_roll, second_roll };
            }
            else
            {
                int first_roll = score_sheet[frame_index][0];

                int second_roll = score_sheet[frame_index][1];

                int third_roll = pins_knocked;

                if (first_roll == 10 && second_roll != 10)
                {
                    third_roll -= second_roll;
                }

                score_sheet[frame_index] = new int[3] { first_roll, second_roll, third_roll };
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
        static void Reset_pins(List<bool> pins_standing)
        {
            for (int i = 0; i < 10; i++)
            {
                pins_standing[i] = true;
            }
        }
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(40, 45);
            Console.SetBufferSize(40, 45);

            //variables
            var pins_standing = new List<bool>();
            for (int i = 0; i < 10; i++)
                pins_standing.Add(true);

            int[][] score_sheet = new int[10][];
            int[] points_gained = new int[10]
            { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
            int[] total_score = new int[10]
            { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};

            //Loop set
            for (int frame_index = 0; frame_index < 10; frame_index++)
            {
                Reset_pins(pins_standing);

                //Draw pins
                Draw_pins(pins_standing);
                Draw_whole_Scoreboard(score_sheet, total_score);

                //Do rolls
                int roll_index = 0;
                while (true)
                {
                    Do_roll(pins_standing, score_sheet, total_score, points_gained, frame_index, roll_index);
                    
                    if (frame_index == 9)
                    {
                        if (score_sheet[frame_index].Length == 1)
                        {
                            if (score_sheet[frame_index][0] == 10)
                            {
                                Reset_pins(pins_standing);
                                Draw_pins(pins_standing);
                            }
                        }
                        else if (score_sheet[frame_index].Length == 2)
                        {
                            if (score_sheet[frame_index][0] + score_sheet[frame_index][1] < 10)
                            {
                                break;
                            }
                            else
                            {
                                if(score_sheet[frame_index][0] + score_sheet[frame_index][1] == 10 || score_sheet[frame_index][1] == 10)
                                {
                                    Reset_pins(pins_standing);
                                    Draw_pins(pins_standing);
                                }
                            }
                        }
                        else if (score_sheet[frame_index].Length == 3)
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (score_sheet[frame_index][0] == 10 || score_sheet[frame_index].Length == 2) break;
                    }

                    roll_index++;
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