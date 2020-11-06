using System;
using System.Collections.Generic;
using System.Threading;

namespace Ascii_stuff
{
    // Enumerations

    enum crew_type
    {
        player,
        lava_alien_pet
    }
    enum item_type
    {
        //Tools
        pickaxe,
        shovel,

        //Suits
        lava_suit,
        toxic_slime_suit
        
    }

    // Classes
    
    class choice
    {
        public string option_text;
        public string outcome_text;
    }
    class situation
    {
        public string intro_text;
        public List<choice> choices = new List<choice>();
    }

    class Program
    {
        //Game state
        static List<item_type> inventory;        
        static List<crew_type> crew;

        static int food;
        static int fuel;
        static int money;
        static int morale;
        static string choice;
        static List<string> game_text;

        static situation test_situation;
        static situation another_situation;

        static void State_Initialize()
        {
            food = 10;
            fuel = 10;
            money = 0;
            morale = 100;
            choice = "0";

            crew = new List<crew_type>();

            crew.Add(crew_type.player);
            crew.Add(crew_type.lava_alien_pet);
            crew.Add(crew_type.lava_alien_pet);
            crew.Add(crew_type.lava_alien_pet);


            inventory = new List<item_type>();
            inventory.Add(item_type.lava_suit);
            inventory.Add(item_type.pickaxe);
            inventory.Add(item_type.shovel);


            game_text = new List<string>();

            //Create all situation
            test_situation = new situation();
            test_situation.intro_text = "You find yourself in a room with three buttons.\n" +
                "The buttons say 1, 2, 3. Very interesting... So many choices... What to choose?";

            test_situation.choices.Add(new choice
            {
                option_text = "1. Press the first button",
                outcome_text = "You chose 1....\nGood for you"
            });


            test_situation.choices.Add(new choice {

                option_text = "2. Press the second button",
                outcome_text = "You chose 2.... \nOkay"

            });

            test_situation.choices.Add(new choice
            {

                option_text = "3. Press the third button",
                outcome_text = "Something"

            });

            another_situation = new situation();
            another_situation.intro_text = "You walk into a house";

            another_situation.choices.Add(new choice
            {

                option_text = "1. Do somehthing",
                outcome_text = "Something"

            });

        }

        //Art
        static void Art_Blank()
        {
            //Blank 50 * 30
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
        }
        static void Art_Planet_Village_Sun()
        {
            //Blank
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                        ▒▒▒▒▒▒▒▒▒▒▒▒▒▒              ");
            Console.WriteLine("                                                                    ▒▒▒▒░░░░░░░░░░░░░░▒▒▒▒          ");
            Console.WriteLine("                                                                ▒▒▒▒░░░░░░░░░░░░░░░░░░░░░░▒▒▒▒      ");
            Console.WriteLine("                                                              ▒▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▒    ");
            Console.WriteLine("                                                            ▒▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▒  ");
            Console.WriteLine("                                                            ▒▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▒  ");
            Console.WriteLine("                                                          ▒▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▒");
            Console.WriteLine("                                                          ▒▒░░░░░░░░░░██░░░░░░░░░░░░░░██░░░░░░░░░░▒▒");
            Console.WriteLine("                                                        ▒▒░░░░░░░░░░██  ██░░░░░░░░░░██  ██░░░░░░░░░░");
            Console.WriteLine("                                                        ▒▒░░░░░░░░██      ██░░░░░░██      ██░░░░░░░░");
            Console.WriteLine("                                                        ▒▒░░░░░░██████████████░░██████████████░░░░░░");
            Console.WriteLine("                                                        ▒▒░░░░░░░░██      ██░░░░░░██      ██░░░░░░░░");
            Console.WriteLine("                                                        ▒▒░░░░░░░░██  ██  ██░░░░░░██  ██  ██░░░░░░░░");
            Console.WriteLine("                                                        ▒▒░░░░░░░░██      ██░░░░░░██      ██░░░░░░░░");
            Console.WriteLine("                                                          ▒▒░░██████████████████████████████████████");
            Console.WriteLine("                                                    ██████████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
            Console.WriteLine("                                            ████████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
            Console.WriteLine("                                      ██████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
            Console.WriteLine("                                  ████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
        }
        static void Art_Planet_Village_Stars()
        {
            //Blank
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("                                                          ██                                        ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("            ██                                                                          ██          ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                    ██                                                              ");
            Console.WriteLine("                                                                      ██                            ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                        ██                                          ");
            Console.WriteLine("                            ██                                                      ██              ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                          ██                                                        ");
            Console.WriteLine("                                                                            ██                      ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                                                                                    ");
            Console.WriteLine("                                  ██                                  ██              ██            ");
            Console.WriteLine("                ██                                                  ██  ██          ██  ██          ");
            Console.WriteLine("                                                                  ██      ██      ██      ██        ");
            Console.WriteLine("                                                                ██████████████  ██████████████      ");
            Console.WriteLine("                                                                  ██      ██      ██      ██        ");
            Console.WriteLine("                                              ██                  ██  ██  ██      ██  ██  ██        ");
            Console.WriteLine("                                                                  ██      ██      ██      ██        ");
            Console.WriteLine("                                                              ██████████████████████████████████████");
            Console.WriteLine("                                                    ██████████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
            Console.WriteLine("      ██                                    ████████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
            Console.WriteLine("                                      ██████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
            Console.WriteLine("                                  ████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
        }
        static void Art_Alien_Friend()
        {
            //Blank
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("                                          ▓▓▓▓▓▓                                                    ");
            Console.WriteLine("                                          ▓▓▓▓▓▓                                                    ");
            Console.WriteLine("                                          ▓▓▓▓▓▓                                                    ");
            Console.WriteLine("                                          ▓▓▓▓▓▓                                                    ");
            Console.WriteLine("                                          ▓▓▓▓▓▓                                                    ");
            Console.WriteLine("                                          ▓▓▓▓▓▓                                                    ");
            Console.WriteLine("                                          ▓▓▓▓▓▓                                                    ");
            Console.WriteLine("                                          ▓▓▓▓▓▓                                                    ");
            Console.WriteLine("                                          ▓▓▓▓▓▓                                                    ");
            Console.WriteLine("                                          ▓▓▓▓▓▓                                                    ");
            Console.WriteLine("                                          ▓▓▓▓▓▓                                                    ");
            Console.WriteLine("                                          ▓▓▓▓▓▓                                                    ");
            Console.WriteLine("                                          ▓▓▓▓▓▓                      ██████                        ");
            Console.WriteLine("                                          ▓▓▓▓▓▓                  ████      ██████                  ");
            Console.WriteLine("                                          ▓▓▓▓▓▓                ██                ██                ");
            Console.WriteLine("                                          ▓▓▓▓▓▓              ██                    ██              ");
            Console.WriteLine("                                          ▓▓▓▓▓▓            ██    ░░████              ██            ");
            Console.WriteLine("                                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██  ░░░░░░████            ██▓▓▓▓▓▓▓▓▓▓▓▓");
            Console.WriteLine("                                      ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██    ██░░██████      ░░██    ██▓▓▓▓▓▓▓▓▓▓");
            Console.WriteLine("                                  ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██    ██████████    ░░░░████  ██▓▓▓▓▓▓▓▓▓▓");
            Console.WriteLine("                              ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓            ██      ██████      ████████  ██          ");
            Console.WriteLine("                          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓              ██                      ████      ██        ");
            Console.WriteLine("                      ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓                ██      ██      ██    ██            ██        ");
            Console.WriteLine("                  ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓                  ██        ██      ██    ██        ██  ██        ");
            Console.WriteLine("              ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓                      ██      ██        ██    ██          ██          ");
            Console.WriteLine("          ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓                            ████████    ████      ██            ██        ");
            Console.WriteLine("      ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓                                        ████    ██  ██  ██          ██        ");
            Console.WriteLine("  ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓                                                      ██      ██████████          ");
            Console.WriteLine("▓▓▓▓▓▓▓▓▓▓▓▓▓▓                                                                                      ");
            Console.WriteLine("▓▓▓▓▓▓▓▓                                                                                            ");
        }
        static void Art_Spaceship_Animation()
        {
            for (int i = 0; i < 2; i++)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("                          ██                                                                        ");
                Console.WriteLine("                        ██  ██                                                                      ");
                Console.WriteLine("                          ██                                                                        ");
                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                                                                                  ██                ");
                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                            ██████                                                                  ");
                Console.WriteLine("                          ██      ██                                                                ");
                Console.WriteLine("                          ██        ██                                                              ");
                Console.WriteLine("                    ▒▒▒▒▒▒▒▒████████████████████████████████                                        ");
                Console.WriteLine("                ▒▒▒▒░░░░░░░░▒▒██                            ██                                      ");
                Console.WriteLine("      ██      ▒▒░░░░░░░░░░▒▒▒▒██                    ████      ██                                    ");
                Console.WriteLine("            ▒▒▒▒░░░░░░░░▒▒████████████            ██    ██      ██                                  ");
                Console.WriteLine("                ▒▒░░░░░░▒▒████████████            ██    ██      ██                                  ");
                Console.WriteLine("                  ▒▒▒▒░░░░░░▒▒██                    ████      ██                                    ");
                Console.WriteLine("                      ▒▒░░░░░░██                            ██                                      ");
                Console.WriteLine("                        ▒▒▒▒████████████████████████████████                                        ");
                Console.WriteLine("                          ██        ██                                                              ");
                Console.WriteLine("                          ██      ██                                                        ██      ");
                Console.WriteLine("                            ██████                                                                  ");
                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                                                      ██                                            ");
                Console.WriteLine("                                                    ██  ██                                          ");
                Console.WriteLine("                                                      ██                                            ");
                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                                                                                                    ");
                Thread.Sleep(400);
                Console.SetCursorPosition(0, 0);

                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                          ██                                                                        ");
                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                                                                                  ██                ");
                Console.WriteLine("                            ██████                                              ██  ██              ");
                Console.WriteLine("                          ██      ██                                              ██                ");
                Console.WriteLine("                          ██        ██                                                              ");
                Console.WriteLine("                            ████████████████████████████████                                        ");
                Console.WriteLine("                          ▒▒░░██                            ██                                      ");
                Console.WriteLine("                        ▒▒░░░░██                    ████      ██                                    ");
                Console.WriteLine("      ██                ▒▒████████████            ██    ██      ██                                  ");
                Console.WriteLine("    ██  ██            ▒▒░░████████████            ██    ██      ██                                  ");
                Console.WriteLine("      ██            ▒▒▒▒░░░░░░██                    ████      ██                                    ");
                Console.WriteLine("                        ▒▒░░░░██                            ██                                      ");
                Console.WriteLine("                          ▒▒████████████████████████████████                                        ");
                Console.WriteLine("                          ██        ██                                                              ");
                Console.WriteLine("                          ██      ██                                                                ");
                Console.WriteLine("                            ██████                                                          ██      ");
                Console.WriteLine("                                                                                          ██  ██    ");
                Console.WriteLine("                                                                                            ██      ");
                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                                                      ██                                            ");
                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                                                                                                    ");
                Console.WriteLine("                                                                                                    ");
                Thread.Sleep(400);
                Console.SetCursorPosition(0, 0);
            }
        }

        //UI
        static void UI_Draw_All()
        {
            Console.Clear();

            UI_Resources();
            UI_Crew();
            Art_Planet_Village_Sun();
            UI_inventory();
            UI_Game_Text();
        }
        static void UI_Resources()
        {
            int pos_x = 104;
            int pos_y = 40;

            Console.SetCursorPosition(pos_x, pos_y);
            Console.WriteLine("█████████████████████");

            for (int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition(pos_x, pos_y + 1 + i * 2);
                Console.WriteLine("██╬═════════╬═════╬██");
            }

            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(pos_x, pos_y + 2 + i * 2);
                Console.WriteLine("██║         ║     ║██");
            }

            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(pos_x + 3, pos_y + 2 + i * 2);
                if (i == 0)
                    Console.Write("Food:");
                if (i == 1)
                    Console.Write("Fuel:");
                if (i == 2)
                    Console.Write("Bucks:");
                if (i == 3)
                    Console.Write("Morale:");
            }
            
            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(pos_x + 13, pos_y + 2 + i * 2);
                if (i == 0)
                    Console.Write(food);
                if (i == 1)
                    Console.Write(fuel);
                if (i == 2)
                    Console.Write($"{money}$");
                if (i == 3)
                    Console.Write($"{morale}%");
            }
            Console.SetCursorPosition(pos_x, pos_y + 10);
            Console.WriteLine("█████████████████████");
        }
        static void UI_Crew()
        {
            int pos_x = 104;
            int pos_y = 32;
            for (int i = 0; i < crew.Count; i++)
            {
                if (crew[i] == crew_type.player)
                {
                    Console.SetCursorPosition(pos_x + i * 7, pos_y);
                    Console.WriteLine("  ██ ");
                    Console.SetCursorPosition(pos_x + i * 7, pos_y + 1);
                    Console.WriteLine("  █  ");
                    Console.SetCursorPosition(pos_x + i * 7, pos_y + 2);
                    Console.WriteLine(" ███ ");
                    Console.SetCursorPosition(pos_x + i * 7, pos_y + 3);
                    Console.WriteLine("█ █ █");
                    Console.SetCursorPosition(pos_x + i * 7, pos_y + 4);
                    Console.WriteLine("  █  ");
                    Console.SetCursorPosition(pos_x + i * 7, pos_y + 5);
                    Console.WriteLine(" █ █ ");
                    Console.SetCursorPosition(pos_x + i * 7, pos_y + 6);
                    Console.WriteLine("█   █");
                }
                else if (crew[i] == crew_type.lava_alien_pet)
                {
                    Console.SetCursorPosition(pos_x + i * 7, pos_y);
                    Console.WriteLine("     ");
                    Console.SetCursorPosition(pos_x + i * 7, pos_y + 1);
                    Console.WriteLine("     ");
                    Console.SetCursorPosition(pos_x + i * 7, pos_y + 2);
                    Console.WriteLine("  █  ");
                    Console.SetCursorPosition(pos_x + i * 7, pos_y + 3);
                    Console.WriteLine(" █▒█ ");
                    Console.SetCursorPosition(pos_x + i * 7, pos_y + 4);
                    Console.WriteLine("█▒▒▒█");
                    Console.SetCursorPosition(pos_x + i * 7, pos_y + 5);
                    Console.WriteLine("█▒▒▒█");
                    Console.SetCursorPosition(pos_x + i * 7, pos_y + 6);
                    Console.WriteLine(" ███ ");
                }
            }
        }
        static void UI_inventory()
        {
            int pos_x = 104;
            int pos_y = 2;

            Console.SetCursorPosition(pos_x, pos_y);
            Console.Write("Inventory:");

            for (int i = 0; i < inventory.Count; i++)
            {
                Console.SetCursorPosition(pos_x, pos_y + 2 + i);
                Console.Write(inventory[i]);
            }
        }
        static void UI_Game_Text()
        {
            int pos_x = 2;
            int pos_y = 50;

            int amount_of_text;
            if (game_text.Count < 10)
                amount_of_text = game_text.Count;
            else
                amount_of_text = 10;

            for (int i = 0; i < amount_of_text; i++)
            {
                Console.SetCursorPosition(pos_x, pos_y - i * 2);
                Console.Write(game_text[game_text.Count - i - 1]);
            }
            Console.SetCursorPosition(2, 51);
            Console.Write("__________________________________________________________________________________________");
        }

        //Choices
        static void Make_A_Choice(situation this_situation)
        {
            string[] intro_text_lines = this_situation.intro_text.Split('\n');

            foreach (string line in intro_text_lines)
            {
                game_text.Add(line);
            }

            game_text.Add("");

            foreach (choice choice in this_situation.choices)
            {
                game_text.Add(choice.option_text);
            }

            game_text.Add("");
            for (; ; )
            {
                UI_Draw_All();
                Console.SetCursorPosition(2, 53);
                choice = Console.ReadLine();

                if (choice == "1")
                {
                    game_text.Add("You chose 1....");
                    game_text.Add("Good for you");
                    break;
                }
                else if (choice == "2")
                {
                    game_text.Add("You chose 2....");
                    game_text.Add("Okay");
                    break;
                }
                else if (choice == "3")
                {
                    game_text.Add("You chose 3....");
                    game_text.Add("Good choice");
                    break;
                }
            }
            UI_Draw_All();
        }

        static void Main(string[] args)
        {
            //Variables and other stuff to set at start
            Console.CursorVisible = false;
            Console.SetWindowSize(135, 55);

            State_Initialize();

            Make_A_Choice(test_situation);
        }
    }
}
