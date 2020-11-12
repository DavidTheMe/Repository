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

        static situation back_to_ship = new situation();

        #region Initialize_All_Planets
        static situation desert_planet_situation = new situation();
        static situation desert_planet_village_situation = new situation();
        static situation desert_planet_village_converse_with_alien = new situation();
        static situation desert_planet_escape_from_aliens = new situation();
        static situation desert_planet_cave_situation = new situation();

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

            #region Back_to_ship
            back_to_ship.initialize = delegate ()
            {
                Back_To_Ship();
            };
            #endregion

            #region Desert_Planet_Situation
            desert_planet_situation.initialize = delegate ()
            {
                var things_you_see = new List<string>();

                bool high_sand_dunes = false;

                var possible_choices = new List<choice>
                {
                    new choice
                    {
                        option_text = $"Go to village",
                        next_situation = desert_planet_village_situation,
                        initialize = delegate
                        {
                            things_you_see.Add("a small village");
                        }
                    },
                    new choice
                    {
                        option_text = $"Go to cave",
                        next_situation = desert_planet_cave_situation,
                        initialize = delegate
                        {
                            things_you_see.Add("cave enterance");
                        }
                    },
                    new choice
                    {
                        option_text = $"Land somewhere else on this planet",
                        next_situation = desert_planet_situation
                    },
                    new choice
                    {
                        option_text = $"Climb on top of the sand dune you're on",
                        initialize = delegate
                        {
                            high_sand_dunes = true;
                        }
                    },
                    new choice
                    {
                        option_text = $"Go to pond",
                        initialize = delegate
                        {
                            things_you_see.Add("pond");
                        }
                    },
                    new choice
                    {
                        option_text = $"Say hello to the caravan",
                        initialize = delegate
                        {
                            things_you_see.Add("wandering caravan");
                        }
                    },
                    new choice
                    {
                        option_text = $"Go to city",
                        initialize = delegate
                        {
                            things_you_see.Add("relatively big city");
                        }
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
                        next_situation = back_to_ship,
                    },
                    new choice
                    {
                        option_text = $"Give the {alien_creature} a gift",
                        condition = delegate ()
                        {
                            return inventory.Contains(item_type.mysterious_object);
                        },
                        fail_text = "You start digging in your pockets but you realize that you don't have anything that it would need \n" +
                        "It's such a simple happy creature that probably wouldn't care for materialistic things.",
                        next_situation = desert_planet_escape_from_aliens
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

            #region Desert_Planet_Escape_From_Aliens
            desert_planet_escape_from_aliens.initialize = delegate ()
            {
                var rand = new Random();

                var possible_choices = new List<choice>
                {
                    new choice
                    {
                        option_text = $"Run back to the ship",
                        next_situation = back_to_ship,
                    },
                };

                Pick_Choices(desert_planet_escape_from_aliens, possible_choices, 1);

                desert_planet_escape_from_aliens.intro_text =
                "You start digging in your pockets...\n" +
                $"The {alien_creature} sees the mysterious object in your pockets and starts pointing at it while screaming \n" +
                "Every one sitting at the table starts freaking out. You stand up in shock \n" +
                $"The {alien_creature} starts slowly walking towards you and you get the feeling that he doesn't have good intentions \n" +
                "You start running away from it and it chases you. The only logical thing to do here would be to escape... \n" +
                "Even though is ended badly, it was nice to befriend some aliens for a while atleast \n" +
                "(+20 Morale)";

                int amount_of_morale = 20;
                if (morale + amount_of_morale > 100)
                    morale = 100;
                else
                    morale += 20;
            };
            #endregion

            #region Desert_Planet_Cave_Situation
            desert_planet_cave_situation.initialize = delegate ()
            {
                bool there_is_a_tunnel = false;
                var possible_choices = new List<choice>
                {
                    new choice
                    {
                        option_text = $"Try to keep digging the tunnels",
                        next_situation = desert_planet_village_situation,

                        initialize = delegate
                        {
                            bool there_is_a_tunnel = true;
                        },

                        condition = delegate ()
                        {
                            return inventory.Contains(item_type.shovel);
                        },

                        fail_text = "You try to dig into the sand with your hands but it's pretty hard \n" +
                        "You realize that it was actually rather silly to try digging in a cave with your hands \n" +
                        "(Didn't you read the hint?)"
                    },
                };

                // Pick three choices you can make
                Pick_Choices(desert_planet_cave_situation, possible_choices, 2);

                desert_planet_cave_situation.intro_text = "You walk down the stairs of the cave and you see a sign on the wall \n" +
                $"{(there_is_a_tunnel == true ? "It says 'GOLD MINE' with big bold letters. Despite apparently being a gold mine it's not too deep" : "It says 'Tread lightly'. 'Okay I will' you think to yourself")}\n" +
                "The walls seem to be made out of not too compact sandstone that could probably \n" +
                "be dug into with a decent digging tool *hint hint*";
            };
            #endregion
        }
        #endregion

        #region State_Initialize
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
            inventory.Add(item_type.shovel);
            inventory.Add(item_type.mysterious_object);

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
        #endregion

        #region Art
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
            for (int i = 0; i < 4; i++)
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
        #endregion

        //UI
        #region UI_Draw_All
        static void UI_Draw_All()
        {
            Console.Clear();

            UI_Resources();
            UI_Crew();
            Art_Planet_Village_Sun();
            UI_Inventory();
            UI_Game_Text();
        }
        #endregion

        #region UI_Resources
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
        #endregion

        #region UI_Crew
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
        #endregion

        #region UI_Inventory
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
        #endregion

        #region UI_Game_Text
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
        #endregion

        //Choices
        #region Pick_Choices
        private static void Pick_Choices(situation current_situation, List<choice> possible_choices, int number_of_choices)
        {
            var rand = new Random();
            bool doable_choice_selected = false;

            current_situation.choices.Clear();
            for (int current_option = 0; current_option < number_of_choices; current_option++)
            {
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
        #endregion

        #region Make_A_Choice
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
        #endregion

        #region Back_To_Ship
        static void Back_To_Ship()
        {
            game_text.Add("You get on your ship and set course to your next planet");
            game_text.Add("While you're waiting to get there you eat some food");
            UI_Draw_All();
            Art_Spaceship_Animation();
            game_text.Add("");
            game_text.Add("After a smooth ride you land on your next planet...");
            Initialize_Desert_Planet();
            Make_A_Choice(desert_planet_situation);
        }
        #endregion

        static void Main(string[] args)
        {
            //Variables and other stuff to set at start
            Console.CursorVisible = false;
            Console.SetWindowSize(135, 55);

            State_Initialize();
            Back_To_Ship();
        }
    }
}
