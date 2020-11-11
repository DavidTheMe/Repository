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
        compass,
        mysterious_object,

        //Suits
        lava_suit,
        toxic_slime_suit,
        swim_suit
        
    }

    // Classes
    class choice
    {
        public string option_text;
        public situation next_situation;
        public Func<bool> condition;
        public string fail_text;
        public Action initialize;
    }
    class situation
    {
        public string intro_text;
        public List<choice> choices = new List<choice>();
        public Action initialize;
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
        static List<string> game_text;

        static situation test_situation = new situation();
        static situation another_situation = new situation();
        
        static situation desert_planet_situation = new situation();
        static situation desert_planet_village_situation = new situation();
        static situation desert_planet_village_converse_with_alien = new situation();
        static void Initialize_Desert_Planet()
        {
            #region Choose_Inhabitance
            string alien_creature;
            string alien_creature_plural;

            var rand = new Random();
            int choose_alien = rand.Next(3);

            if (choose_alien == 0)
            {
                alien_creature = "fly alien";
                alien_creature_plural = "alien flies";
            }
            else if (choose_alien == 1)
            {
                alien_creature = "lizard person";
                alien_creature_plural = "lizard people";
            }
            else
            {
                alien_creature = "fuzzy creature";
                alien_creature_plural = "fuzzy creatures";
            }
            #endregion

            #region Desert_Planet_Situation
            desert_planet_situation.initialize = delegate ()
            {
                var things_you_see = new List<string>();

                bool high_sand_dunes = true;

                var possible_choices = new List<choice>
                {
                    new choice
                    {
                        option_text = $"Go to village",
                        next_situation = desert_planet_village_situation,
                        initialize = delegate ()
                        {
                        },
                        condition = delegate()
                        {
                            return inventory.Contains(item_type.lava_suit);
                        }
                    },
                    new choice
                    {
                        option_text = $"Go to cave",
                    },
                    new choice
                    {
                        option_text = $"Land somewhere else on this planet",
                        next_situation = desert_planet_situation
                    },
                    new choice
                    {
                        option_text = $"Climb on top of the sand dune you're on",
                    },
                    new choice
                    {
                        option_text = $"Go to pond",
                    },
                    new choice
                    {
                        option_text = $"Say hello to the caravan",
                    },
                    new choice
                    {
                        option_text = $"Go to city",
                    },
                };

                // Pick three choices you can make
                Pick_Choices(desert_planet_situation, possible_choices, 3);

                desert_planet_situation.intro_text = "You walk out of your ship and you're greeted with a bright sun and a very hot climate \n" +
                    "After your eyes have adjusted you can tell that your in the middle of a huge desert \n" +
                    $"{(high_sand_dunes ? "The sand dunes here seem really high and you could probably climb them" : "It looks like this planet is pretty much just a flat plain of sand")} \n" +
                    $"{(things_you_see.Count == 1 ? $"You look at the horizon while squinting your eyes... a {things_you_see[0]}!!" : "")}" +
                    $"{(things_you_see.Count == 2 ? $"You look into the distance and you see two things: a {things_you_see[0]} and a {things_you_see[1]}" : "")}" +
                    $"{(things_you_see.Count == 3 ? $"You look around a bit and there seems to be a {things_you_see[0]}, a {things_you_see[1]} and a {things_you_see[2]} nearby" : "")}";
            };
            #endregion

            #region Desert_Planet_Village_Situation
            desert_planet_village_situation.initialize = delegate ()
            {

                var possible_choices = new List<choice>
                {
                    new choice
                    {
                        option_text = $"Go to shop",
                    },
                    new choice
                    {
                        option_text = $"Converse with a {alien_creature}",
                        next_situation = desert_planet_village_converse_with_alien,
                    },
                    new choice
                    {
                        option_text = $"Pickpocket the {alien_creature_plural}",
                    },
                    new choice
                    {
                        option_text = $"Dig into the dump",
                        condition = delegate ()
                        {
                            return inventory.Contains(item_type.shovel);
                        },
                        fail_text = "You start digging into the garbage with your hands. It seems like a such a great idea \n" +
                        "But then suddenly you cut yourself on a sharp piece of metal. You feel very silly, you silly person"
                    },
                };

                Pick_Choices(desert_planet_village_situation, possible_choices, 2);

                desert_planet_village_situation.intro_text = $"You enter the village. It seems to be inhabited by {alien_creature_plural}";
            };
            #endregion

            #region Desert_Planet_Village_Converse_With_Alien
            desert_planet_village_converse_with_alien.initialize = delegate ()
            {
                var rand = new Random();

                var possible_choices = new List<choice>
                {
                    new choice
                    {
                        option_text = $"Go back to ship",
                    },
                    new choice
                    {
                        option_text = $"Give the {alien_creature} a gift",
                        condition = delegate ()
                        {
                            return inventory.Contains(item_type.mysterious_object);
                        },
                        fail_text = "You start digging in your pockets but you realize that you don't have anything that it would need \n" +
                        "It's such a simple happy creature that probably wouldn't care for materialistic things."
                    },
                };

                Pick_Choices(desert_planet_village_converse_with_alien, possible_choices, 2);

                desert_planet_village_converse_with_alien.intro_text =
                $"You walk up to one of the {alien_creature_plural} and say hi. It looks at you for a while \n" +
                "Suddenly it starts making all sorts of weird squeking sounds. You stand there horrified \n" +
                "The weird creature then starts walking and waves for you to follow it, and you do \n" +
                $"It leads you to a dinner table where a family of {alien_creature_plural} have gathered \n" +
                "Of course! It's exited to have a visitor and it is inviting you for dinner \n" +
                $"You sit down and have a nice dinner with the {alien_creature}s family \n" +
                "Even though you don't understand eatchother you feel like you're forming somewhat of a bond \n" +
                "by screaming random words and flailing your arms at eatchother \n" +
                "It's getting late and you decide its time to leave, so you tell the alien that led you here: \n" +
                $"'Hey, you're not too bad for being a {alien_creature}' without really thinking about the implications";
            };
            #endregion
        }
        static void State_Initialize()
        {
            food = 10;
            fuel = 10;
            money = 0;
            morale = 100;

            crew = new List<crew_type>();

            crew.Add(crew_type.player);
            crew.Add(crew_type.lava_alien_pet);
            crew.Add(crew_type.lava_alien_pet);
            crew.Add(crew_type.lava_alien_pet);


            inventory = new List<item_type>();
            inventory.Add(item_type.lava_suit);
            inventory.Add(item_type.pickaxe);

            game_text = new List<string>();

            //Create all situation

            //test situation
            test_situation.intro_text = "You find yourself in a room with three buttons.\n" +
                "The buttons say 1, 2, 3. Very interesting... So many choices... What to choose?";
            test_situation.initialize = delegate () {
                inventory.Add(item_type.lava_suit);
                food += 10;
            };

            test_situation.choices.Add(new choice
            {
                option_text = "1. Press the first button",
                next_situation = another_situation
            });


            test_situation.choices.Add(new choice {

                option_text = "2. Press the second button",
                next_situation = another_situation,
                condition = null
            });

            test_situation.choices.Add(new choice
            {

                option_text = "3. Press the third button",
                next_situation = another_situation,
                condition = delegate()
                {
                    return inventory.Contains(item_type.shovel);
                },
                fail_text = "You try to dig into the dirt with your hands but the dirt is too strong."
            });

            //another situation 
            another_situation.intro_text = "You walk into a house";
            another_situation.initialize = delegate() {
                morale -= 10;
                inventory.Add(item_type.shovel);
            };

            another_situation.choices.Add(new choice
            {
                option_text = "1. Do somehthing",
                next_situation = test_situation
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
            UI_Inventory();
            UI_Game_Text();
        }
        static void UI_Resources()
        {
            int pos_x = 108;
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
        static void UI_Inventory()
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
            if (game_text.Count < 20)
                amount_of_text = game_text.Count;
            else
                amount_of_text = 20;

            for (int i = 0; i < amount_of_text; i++)
            {
                Console.SetCursorPosition(pos_x, pos_y - i * 1);
                Console.Write(game_text[game_text.Count - i - 1]);
            }
            Console.SetCursorPosition(2, 51);
            Console.Write("__________________________________________________________________________________________");
        }

        //Choices
        private static void Pick_Choices(situation current_situation, List<choice> possible_choices, int number_of_choices)
        {
            var rand = new Random();

            current_situation.choices.Clear();
            for (int current_option = 0; current_option < number_of_choices; current_option++)
            {
                bool doable_choice_selected = false;
                bool condition_passed = false;
                choice selected_choice;

                do
                {
                    int random_index = rand.Next(0, possible_choices.Count);
                    selected_choice = possible_choices[random_index];

                    if (selected_choice.condition == null)
                    {
                        condition_passed = true;
                    }
                    else
                    {
                        condition_passed = selected_choice.condition();
                    }
                } while (!condition_passed && !doable_choice_selected);

                selected_choice.option_text = $"{current_option + 1}. {selected_choice.option_text}";

                current_situation.choices.Add(selected_choice);
                possible_choices.Remove(selected_choice);
                doable_choice_selected = true;

                if (selected_choice.initialize != null)
                {
                    selected_choice.initialize();
                }
            }
        }
        static void Make_A_Choice(situation this_situation)
        {
            this_situation.initialize();
            do
            {
                game_text.Add("_________________________________________________________________________________________");

                //Output intro text
                string[] intro_text_lines = this_situation.intro_text.Split('\n');

                foreach (string line in intro_text_lines)
                {
                    game_text.Add(line);
                }

                game_text.Add("");

                //Output choices
                foreach (choice situation_choice in this_situation.choices)
                {
                    game_text.Add(situation_choice.option_text);
                }

                game_text.Add("");

                //Ask user for choice
                int choice;

                for (; ; )
                {
                    UI_Draw_All();
                    Console.SetCursorPosition(2, 53);
                    string choice_text = Console.ReadLine();
                    choice = Int32.Parse(choice_text);

                    if (choice <= this_situation.choices.Count && choice > 0)
                    {
                        choice selectedChoice = this_situation.choices[choice - 1];
                        bool condition_passed = true;

                        if (selectedChoice.condition != null)
                        {
                            condition_passed = selectedChoice.condition();
                        }

                        if (condition_passed)
                        {
                            Make_A_Choice(selectedChoice.next_situation);
                            break;
                        }
                        else
                        {
                            game_text.Add(selectedChoice.fail_text);
                            game_text.Add("");
                            UI_Draw_All();
                            Console.ReadKey();
                            break;
                        }
                    }
                }
            } while (true);
        }

        static void Main(string[] args)
        {
            //Variables and other stuff to set at start
            Console.CursorVisible = false;
            Console.SetWindowSize(135, 55);

            State_Initialize();
            Initialize_Desert_Planet();
            Make_A_Choice(desert_planet_situation);
        }
    }
}
