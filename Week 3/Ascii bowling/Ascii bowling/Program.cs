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
                    Console.Write("╟─╥─╠═╗"); Console.SetCursorPosition(30, 1 + (i * 4));
                    Console.Write("║ ║ ║ ║"); Console.SetCursorPosition(30, 2 + (i * 4));
                    Console.Write("╟─╨─╨─╢"); Console.SetCursorPosition(30, 3 + (i * 4));
                    Console.Write("║     ║"); Console.SetCursorPosition(30, 4 + (i * 4));
                    Console.Write("╚═════╝");
                }
                else
                {
                    Console.SetCursorPosition(30, i * 4);
                    Console.Write("╟─╥─╢"); Console.SetCursorPosition(30, 1 + (i * 4));
                    Console.Write("║ ║ ║"); Console.SetCursorPosition(30, 2 + (i * 4));
                    Console.Write("╟─╨─╢"); Console.SetCursorPosition(30, 3 + (i * 4));
                    Console.Write("║   ║");
                }

                for (int j = 0; j < score_sheet[i].Length; j++)
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
        }
        static void Draw_scoreboard(int first_roll, int second_roll, int final_score)
        {
            for (int i = 0; i < 10; i++)
                Console.WriteLine();

            //After first roll
            if (second_roll == -1 && first_roll < 10)
            {
            Console.WriteLine($"▄▄▄▄▄");
            Console.WriteLine($"▌{first_roll}█ ▐");
            Console.WriteLine($"█▀▀▀█");
            Console.WriteLine($"█   █");
            Console.WriteLine($"▀▀▀▀▀");
            }
            //Strike
            else if (first_roll == 10)
            {
                Console.WriteLine($"▄▄▄▄▄");
                Console.WriteLine($"STRIKE!");
                Console.WriteLine($"█▀▀▀█");
                Console.WriteLine($"█{final_score} █");
                Console.WriteLine($"▀▀▀▀▀");
            }
            //Spare
            else if (first_roll + second_roll == 10)
            {
                Console.WriteLine($"▄▄▄▄▄");
                Console.WriteLine($"▌{first_roll}█/▐");
                Console.WriteLine($"█▀▀▀█");
                Console.WriteLine($"█{final_score} █");
                Console.WriteLine($"▀▀▀▀▀");
            }
            //Both rolls
            else if (final_score < 10)
            {
                Console.WriteLine($"▄▄▄▄▄");
                Console.WriteLine($"▌{first_roll}█{second_roll}▐");
                Console.WriteLine($"█▀▀▀█");
                Console.WriteLine($"█ {final_score} █");
                Console.WriteLine($"▀▀▀▀▀");
            }
            else if (final_score < 101)
            {
                Console.WriteLine($"▄▄▄▄▄");
                Console.WriteLine($"▌{first_roll}█{second_roll}▐");
                Console.WriteLine($"█▀▀▀█");
                Console.WriteLine($"█{final_score} █");
                Console.WriteLine($"▀▀▀▀▀");
            }
            else if (final_score > 100)
            {
                Console.WriteLine($"▄▄▄▄▄");
                Console.WriteLine($"▌{first_roll}█{second_roll}▐");
                Console.WriteLine($"█▀▀▀█");
                Console.WriteLine($"█{final_score}█");
                Console.WriteLine($"▀▀▀▀▀");
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
            //variables

            Console.CursorVisible = false;
            int roll_number = -1;
            int final_score = 0;
            var pins_standing = new List<bool>();
            for (int i = 0; i < 10; i++)
                pins_standing.Add(true);

            int[][] score_sheet = new int[10][];
            int[] total_score = new int[10];

            var random = new Random();

            //Loop set
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    pins_standing[j] = true;
                }

                int first_roll = -1;
                int second_roll = -1;

                //Draw pins
                Draw_pins(pins_standing);

                //Do 2 rolls
                for (int j = 0; j < 2; j++)
                {
                    if (j == 0)
                        first_roll = 0;
                    if (j == 1)
                        second_roll = 0;

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
                        if (pins_standing[k] == false && j == 0)
                            first_roll += 1;
                        if (pins_standing[k] == false && j == 1)
                            second_roll += 1;
                    }

                    if (j == 1)
                    {
                        second_roll -= first_roll;
                        final_score += second_roll + first_roll;
                        score_sheet[i] = new int[2] { first_roll, second_roll };
                        if (i == 0)
                        {
                            total_score[i] = final_score;
                        }
                        else
                        {
                            total_score[i] = total_score[i - 1] + final_score;
                        }
                    }

                    //Draw stuff
                    Console.Clear();
                    Draw_pins(pins_standing);
                    Draw_scoreboard(first_roll, second_roll, final_score);
                    Draw_whole_Scoreboard(score_sheet, total_score);
                }

                //Press enter text
                Console.WriteLine("");
                Console.WriteLine("Press Enter to reset the pins");
                Console.ReadLine();
                Console.Clear();
            }
            Console.WriteLine($"Your final score was {final_score}!");
        }
    }
}
