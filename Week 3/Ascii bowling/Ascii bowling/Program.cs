using System;
using System.Collections.Generic;
using System.Globalization;

namespace Ascii_bowling
{
    class Program
    {
        static void Draw_scoreboard(int first_roll, int second_roll, int final_score)
        {
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
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("   ");
            }
            else
                Console.Write("    ");

            if (pins_standing[1] == true)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("   ");
            }
            else
                Console.Write("    ");

            if (pins_standing[2] == true)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("   ");
            }
            else
                Console.Write("    ");

            if (pins_standing[3] == true)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("   ");
            }
            else
                Console.WriteLine(" ");

            //Second row
            if (pins_standing[4] == true)
            {
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("   ");
            }
            else
                Console.Write("      ");

            if (pins_standing[5] == true)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("   ");
            }
            else
                Console.Write("  ");

            if (pins_standing[6] == true)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("   ");
            }
            else
                Console.WriteLine(" ");

            //Third row
            if (pins_standing[7] == true)
            {
                Console.Write("    ");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("   ");
            }
            else
                Console.Write("     ");

            if (pins_standing[8] == true)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("   ");
            }
            else
                Console.WriteLine("    ");

            //Fourth row
            if (pins_standing[9] == true)
            {
                Console.Write("      ");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("   ");
            }
            else
                Console.WriteLine("    ");
        }
        static void Knock_pin(List<bool> pins_standing, int pin_to_knock)
        {
            pins_standing[pin_to_knock] = false;
        }
        static void Main(string[] args)
        {
            //variables
            int roll_number;
            int final_score = 0;
            var pins_standing = new List<bool>();
            for (int i = 0; i < 10; i++)
                pins_standing.Add(true);

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

                    //Enter where to aim
                    Console.WriteLine("");
                    Console.WriteLine("Enter a number between 1 and 7 to choose where to aim");
                    roll_number = int.Parse(Console.ReadLine());
                    Console.Clear();

                    //Knock pins
                    if (roll_number == 1)
                    {
                        Knock_pin(pins_standing, 0);
                    }
                    if (roll_number == 2)
                    {
                        Knock_pin(pins_standing, 0);
                        Knock_pin(pins_standing, 1);
                        Knock_pin(pins_standing, 4);
                    }
                    if (roll_number == 3)
                    {
                        Knock_pin(pins_standing, 1);
                        Knock_pin(pins_standing, 4);
                        Knock_pin(pins_standing, 5);
                    }
                    if (roll_number == 4)
                    {
                        Knock_pin(pins_standing, 1);
                        Knock_pin(pins_standing, 2);
                        Knock_pin(pins_standing, 7);
                        Knock_pin(pins_standing, 8);
                        Knock_pin(pins_standing, 9);
                        Knock_pin(pins_standing, 5);
                    }
                    if (roll_number == 5)
                    {
                        Knock_pin(pins_standing, 2);
                        Knock_pin(pins_standing, 5);
                        Knock_pin(pins_standing, 6);
                        Knock_pin(pins_standing, 8);
                    }
                    if (roll_number == 6)
                    {
                        Knock_pin(pins_standing, 2);
                        Knock_pin(pins_standing, 3);
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
                    }

                    //Draw stuff
                    Draw_pins(pins_standing);
                    Draw_scoreboard(first_roll, second_roll, final_score);
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
