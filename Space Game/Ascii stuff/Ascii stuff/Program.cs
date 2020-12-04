using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Ascii_stuff
{
    // Enumerations

    enum crew_type
    {
        player,
        lava_alien_pet,
        water_alien_pet
    }
    enum item_type
    {
        //Tools
        shovel,
        pickaxe,
        fishing_rod,
        mysterious_object,
        laser_rifle,

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
        public Func<string> condition;
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
        static List<string> random_adjective;

        static int food;
        static int fuel;
        static int money;
        static int morale;
        static List<string> game_text;
        static int planets_explored;

        static Dictionary<item_type, int> prices;

        #region Situation_Helpers

        #region Assign_Shop_Items
        static void Assign_Shop_Items(situation buy_items_situation)
        {
            var buy_items_again = new situation()
            {
                intro_text = "'Watcha buyin?'"
            };

            buy_items_again.initialize = delegate ()
            {
                buy_items_again.choices.Clear();
                buy_items_again.choices.AddRange(buy_items_situation.choices);
            };

            var sell_items = new situation
            {
                intro_text = "'Watcha sellin?'",
            };

            sell_items.initialize = delegate ()
            {
                Assign_Items_To_Sell(sell_items, buy_items_again);
            };

            buy_items_situation.choices.Clear();

            List<item_type> all_items = Enum.GetValues(typeof(item_type)).Cast<item_type>().ToList();
            var rand = new Random();

            int shop_items_count = rand.Next(2, 5);

            for (int i = 0; i < shop_items_count; i++)
            {
                int item_to_add = rand.Next(all_items.Count);
                item_type item_available = all_items[item_to_add];
                all_items.RemoveAt(item_to_add);

                int cost = prices[item_available];

                var item_sold_situation = new situation
                {
                    intro_text = $"You bought a {item_available}",
                    initialize = delegate
                    {
                        inventory.Add(item_available);
                        Add_Money(-cost);
                    },

                    choices = new List<choice>
                    {
                        new choice
                        {
                            option_text = $"1. Keep shopping",
                            next_situation = buy_items_again,
                        }
                    }
                };

                var item_choice = new choice
                {
                    option_text = $"{i + 1}. Buy {item_available} ({cost}$)",
                    next_situation = item_sold_situation,

                    condition = delegate ()
                    {
                        if (inventory.Contains(item_available))
                            return "You already have one of those";

                        if (money < cost)
                            return "You're too broke :(";

                        return null;
                    },
                };

                buy_items_situation.choices.Add(item_choice);
            }

            var sell_choice = new choice
            {
                option_text = $"{shop_items_count + 1}. Sell something",
                next_situation = sell_items
            };
            buy_items_situation.choices.Add(sell_choice);

            var return_to_ship = new choice
            {
                option_text = $"{shop_items_count + 2}. Return to ship",
                next_situation = back_to_ship
            };
            buy_items_situation.choices.Add(return_to_ship);
        }
        #endregion

        #region Assign_Items_To_Sell
        static void Assign_Items_To_Sell(situation sell_items_situation, situation buy_items_situation)
        {
            sell_items_situation.choices.Clear();

            var rand = new Random();

            for (int i = 0; i < inventory.Count; i++)
            {

                item_type item_to_sell = inventory[i];

                int cost = (int)(prices[item_to_sell] * 0.8);

                int item_to_sell_index = i;

                var item_bought_situation = new situation
                {
                    intro_text = $"You sold a {item_to_sell}",
                    initialize = delegate
                    {
                        inventory.RemoveAt(item_to_sell_index);
                        Add_Money(cost);
                    },

                    choices = new List<choice>
                    {
                        new choice
                        {
                            option_text = $"1. Keep selling",
                            next_situation = sell_items_situation,
                        }
                    }
                };

                var item_choice = new choice
                {
                    option_text = $"{i + 1}. Sell {inventory[i]} ({cost}$)",
                    next_situation = item_bought_situation,
                };

                sell_items_situation.choices.Add(item_choice);
            }
            var buy_choice = new choice
            {
                option_text = $"{inventory.Count + 1}. Buy something",
                next_situation = buy_items_situation
            };
            sell_items_situation.choices.Add(buy_choice);

            var return_to_ship = new choice
            {
                option_text = $"{inventory.Count + 2}. Return to ship",
                next_situation = back_to_ship
            };
            sell_items_situation.choices.Add(return_to_ship);
        }
        #endregion

        #region Assign_Food_Market
        static void Assign_Food_Market(situation buy_items_situation)
        {
            buy_items_situation.choices.Clear();

            var rand = new Random();

            int shop_items_count = rand.Next(2, 5);

            for (int i = 1; i <= shop_items_count; i++)
            {
                int food_to_add = i * 5;

                int cost = i * 15;

                var item_sold_situation = new situation
                {
                    intro_text = $"You bought {food_to_add} food",
                    initialize = delegate
                    {
                        food += food_to_add;
                        Add_Money(-cost);
                    },

                    choices = new List<choice>
                    {
                        new choice
                        {
                            option_text = $"1. Go back to ship",
                            next_situation = back_to_ship,
                        }
                    }
                };

                var item_choice = new choice
                {
                    option_text = $"{i}. Buy {food_to_add} food ({cost}$)",
                    next_situation = item_sold_situation,

                    condition = delegate ()
                    {
                        if (money < cost)
                            return "You're too broke :(";

                        return null;
                    },
                };

                buy_items_situation.choices.Add(item_choice);
            }

            var return_to_ship = new choice
            {
                option_text = $"{shop_items_count + 1}. Return to ship",
                next_situation = back_to_ship
            };
            buy_items_situation.choices.Add(return_to_ship);
        }
        #endregion

        #region Assign_Food_Market
        static void Assign_Fuel_Prices(situation buy_items_situation)
        {
            buy_items_situation.choices.Clear();

            var rand = new Random();

            int shop_items_count = rand.Next(2, 5);

            for (int i = 1; i <= shop_items_count; i++)
            {
                int fuel_to_add = i * 5;

                int cost = i * 15;

                var item_sold_situation = new situation
                {
                    intro_text = $"You bought {fuel_to_add} fuel",
                    initialize = delegate
                    {
                        fuel += fuel_to_add;
                        Add_Money(-cost);
                    },

                    choices = new List<choice>
                    {
                        new choice
                        {
                            option_text = $"1. Go back to ship",
                            next_situation = back_to_ship,
                        }
                    }
                };

                var item_choice = new choice
                {
                    option_text = $"{i}. Buy {fuel_to_add} fuel ({cost}$)",
                    next_situation = item_sold_situation,

                    condition = delegate ()
                    {
                        if (money < cost)
                            return "You're too broke :(";

                        return null;
                    },
                };

                buy_items_situation.choices.Add(item_choice);
            }

            var return_to_ship = new choice
            {
                option_text = $"{shop_items_count + 1}. Return to ship",
                next_situation = back_to_ship
            };
            buy_items_situation.choices.Add(return_to_ship);
        }
        #endregion

        #region Add_Back_To_Ship
        static void Add_Back_To_Ship(situation current_situation, bool running = false)
        {
            string option_text = "Go back to the ship";

            if (running)
                option_text = "Run back to the ship";

            current_situation.choices.Add
            (
                new choice
                {
                    option_text = $"{current_situation.choices.Count + 1}. {option_text}",
                    next_situation = back_to_ship,
                }
            );
        }

        static void Add_Back_To_Ship_With_Friend(situation current_situation, string alien_type)
        {
            current_situation.choices.Add
            (
                new choice
                {
                    option_text = $"{current_situation.choices.Count + 1}. Go back to the ship with your new {alien_type} friend",
                    next_situation = back_to_ship,
                }
            );
        }

        #endregion

        #endregion

        #region Random_Ship_Event_On_Ship

        static void Random_Ship_Event()
        {
            var rand = new Random();

            if (rand.Next(5) == 0)
            {
                #region Lava_Alien_Pet
                if (crew.Contains(crew_type.lava_alien_pet))
                {
                    int random_number = rand.Next(1);
                    string event_text;

                    if (random_number == 0)
                    {
                        event_text = "The lava alien burned a hole in the floor of the ship and fell out \n" +
                            "That's a shame... Luckily lava aliens are good at surviving in space so you don't feel too bad \n" +
                            "You have to spend a couple of days repairing the ship though \n" +
                            "(-3 food) (-1 Lava Alien Pet)";

                        food -= 3;
                        crew.Remove(crew_type.lava_alien_pet);
                    }
                    else if (random_number == 1)
                    {
                        event_text = "The lava alien makes a little squeek and it's sooo adorable \n" +
                            "You are very happy you own this odd little pet\n" +
                            "(+5 Morale)";

                        Add_Morale(5);
                    }
                    else
                    {
                        event_text = "This event doesn't exist";
                    }

                    UI_Draw_Big_Line();
                    UI_Add_Game_Text(event_text);
                    UI_Draw_All();
                    Console.ReadKey();
                    Console.WriteLine("");
                }
                #endregion

                #region Water_Alien_Pet
                if (crew.Contains(crew_type.water_alien_pet))
                {
                    int random_number = rand.Next(2);
                    string event_text;

                    if (random_number == 0)
                    {
                        event_text = "The water alien blows a little bubble and it's really funny! Good job alien pet!! \n" +
                            "(+10 Morale)";
                        Add_Morale(10);
                    }
                    else if (random_number == 1)
                    {
                        event_text = "The water alien makes a little squeek and it's sooo adorable \n" +
                            "You are very happy you own this odd little pet\n" +
                            "(+5 Morale)";

                        Add_Morale(5);
                    }
                    else if (random_number == 2)
                    {
                        event_text = "You are a bit thirsty so you stick a straw into the water alien and drink a bit of it \n" +
                            "Your thirst is clenched but you feel a bit bad for your pet\n" +
                            "(-5 Morale) (+1 Food)";

                        food += 1;
                        Add_Morale(-5);
                    }
                    else
                    {
                        event_text = "This event doesn't exist";
                    }

                    UI_Draw_Big_Line();
                    UI_Add_Game_Text(event_text);
                    UI_Draw_All();
                    Console.ReadKey();
                    Console.WriteLine("");
                }
                #endregion
            }
        }

        #endregion

        static situation back_to_ship = new situation();

        #region Initialize_All_Planets

        #region Desert_Planet
        static situation desert_planet_situation = new situation();
        static situation desert_planet_village_situation = new situation();
        static situation desert_planet_village_converse_with_alien = new situation();
        static situation desert_planet_escape_from_aliens = new situation();
        static situation desert_planet_cave_situation = new situation();
        static situation desert_planet_tunnels_dug = new situation();
        static situation desert_planet_pillage_the_village = new situation();
        static situation desert_planet_minecart_rolercoaster = new situation();
        static situation desert_planet_minecart_long_walk = new situation();
        static situation desert_planet_city = new situation();
        static situation desert_planet_escape_from_aliens_city = new situation();
        static situation desert_planet_food_market = new situation();
        static situation desert_planet_rob_bank = new situation();
        static situation desert_planet_museum = new situation();
        static situation desert_planet_pond = new situation();
        static situation desert_planet_water_alien_friend = new situation();
        static situation desert_planet_drink_water = new situation();
        static situation desert_planet_pond_treasure = new situation();
        static situation desert_planet_caravan_buy = new situation();
        static situation desert_planet_lava_alien = new situation();
        static situation desert_planet_dig_dump = new situation();
        static situation desert_planet_pond_fish = new situation();

        static void Initialize_Desert_Planet()
        {
            var rand = new Random();

            #region Choose_Inhabitance

            string adjective = random_adjective[rand.Next(random_adjective.Count)];
            string alien_creature;
            string alien_creature_plural;
            int choose_alien = rand.Next(4);

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
            else if (choose_alien == 2)
            {
                alien_creature = "fuzzy creature";
                alien_creature_plural = "fuzzy creatures";
            }
            else
            {
                alien_creature = $"{adjective} creature";
                alien_creature_plural = $"{adjective} creatures";
            }
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
                            things_you_see.Add("small village");
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
                    //new choice
                    //{
                        //option_text = $"Climb on top of the sand dune you're on",
                        //initialize = delegate
                        //{
                            //high_sand_dunes = true;
                        //}
                    //},
                    new choice
                    {
                        option_text = $"Go to pond",
                        next_situation = desert_planet_pond,
                        initialize = delegate
                        {
                            things_you_see.Add("pond");
                        }
                    },
                    new choice
                    {
                        option_text = $"Say hello to the caravan",
                        next_situation = desert_planet_caravan_buy,
                        initialize = delegate
                        {
                            things_you_see.Add("wandering caravan");
                        }
                    },
                    new choice
                    {
                        option_text = $"Go to city",
                        next_situation = desert_planet_city,
                        initialize = delegate
                        {
                            things_you_see.Add("relatively big city");
                        }
                    },
                };

                // Pick three choices you can make
                Pick_Choices(desert_planet_situation, possible_choices, 3);

                desert_planet_situation.intro_text = "You walk out of your ship and you're greeted with a bright sun and a very hot climate \n" +
                    "After your eyes have adjusted you can tell that you're in the middle of a huge desert \n" +
                    $"{(high_sand_dunes ? "The sand dunes here seem really high and you could probably climb them" : "It looks like this planet is pretty much just a flat plain of sand")} \n" +
                    $"{(things_you_see.Count == 1 ? $"You look at the horizon while squinting your eyes... a {things_you_see[0]}!!" : "")}" +
                    $"{(things_you_see.Count == 2 ? $"You look into the distance and you see two things: a {things_you_see[0]} and a {things_you_see[1]}" : "")}" +
                    $"{(things_you_see.Count == 3 ? $"You look around a bit and there seems to be a {things_you_see[0]}, a {things_you_see[1]} and a {things_you_see[2]} nearby" : "")}";
            };
            #endregion

            #region Desert_Planet_Pillage_The_Village
            desert_planet_pillage_the_village.initialize = delegate ()
            {
                var rand = new Random();
                int how_much_food = rand.Next(1, 7);

                desert_planet_pillage_the_village.intro_text =
                "You begin to pillage the village... wait, that rhymes! \n" +
                $"Pillage the village! Pillage the village! You start singing as you're casually killing {alien_creature_plural}s \n" +
                "Now when everyones dead it's time to start looting! You start picking up a bit of everything \n" +
                "As you're walking around their houses looting things it becomes more and more apparent to you\n" +
                "These were innocent people with families... And you killed them... You killed them all! \n" +
                "They're dead! Every single one of them! Not just the men, but the women and the children too!\n" +
                "(-50 Morale) \n" +
                "On the other hand, they did have some suplies so atleast there's that \n" +
                $"(+{how_much_food} Food)(+{10 - how_much_food} Fuel)(+1 shovel)";
                food += how_much_food;
                fuel += 10 - how_much_food;
                inventory.Add(item_type.shovel);
                Add_Morale(-50);
            };
            Add_Back_To_Ship(desert_planet_pillage_the_village);

            #endregion

            #region Desert_Planet_Village_Situation
            desert_planet_village_situation.initialize = delegate ()
            {
                var possible_choices = new List<choice>
                {
                    //new choice
                    //{
                        //option_text = $"Go to shop",
                    //},
                    new choice
                    {
                        option_text = $"Converse with one of the {alien_creature}",
                        next_situation = desert_planet_village_converse_with_alien,
                    },
                    new choice
                    {
                        option_text = $"Pillage and loot the village",
                        next_situation = desert_planet_pillage_the_village,
                    },
                    new choice
                    {
                        option_text = $"Dig into the dump",
                        next_situation = desert_planet_dig_dump,
                        condition = delegate ()
                        {
                            if (!inventory.Contains(item_type.shovel))
                                return "You start digging into the garbage with your hands\n" +
                                "It seemed like a good idea at first but after a couple of hours you change your mind";
                            return null;
                        },
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
                        option_text = $"Give the {alien_creature} a gift",
                        condition = delegate ()
                        {
                            if (!inventory.Contains(item_type.mysterious_object))
                                return "You start digging in your pockets but you realize that you don't have anything that it would need \n" +
                        "It's such a simple happy creature that probably wouldn't care for materialistic things.";
                            return null;
                        },
                        next_situation = desert_planet_escape_from_aliens
                    },
                };

                Pick_Choices(desert_planet_village_converse_with_alien, possible_choices, 1);

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
                $"'Hey, you're not too bad for being a {alien_creature}' without really thinking about the implications \n" +
                "Good thing it can't hear you \n" +
                "(+20 morale)";

                Add_Morale(20);
            };
            #endregion

            #region Desert_Planet_Escape_From_Aliens
            desert_planet_escape_from_aliens.initialize = delegate ()
            {
                var rand = new Random();

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
            Add_Back_To_Ship(desert_planet_escape_from_aliens, true);
            #endregion

            #region Desert_Planet_Cave_Situation
            desert_planet_cave_situation.initialize = delegate ()
            {
                bool there_is_a_tunnel = false;

                bool lava_alien_is_here = false;

                int amount_of_minecarts = 0;

                string cart_adjective1 = random_adjective[rand.Next(random_adjective.Count)];
                string cart_adjective2 = random_adjective[rand.Next(random_adjective.Count)];

                var possible_choices = new List<choice>
                {
                    new choice
                    {
                        option_text = $"Try to keep digging the tunnels",
                        next_situation = desert_planet_tunnels_dug,

                        initialize = delegate
                        {
                            there_is_a_tunnel = true;
                        },

                        condition = delegate ()
                        {
                            if (!inventory.Contains(item_type.pickaxe))
                                return "You try to dig into the sand stone with your hands but it's pretty hard (Did you not read the hint?)";
                            return null;
                        },
                    },
                    new choice
                    {
                        option_text = $"Ride the {cart_adjective1} minecart",
                        next_situation = desert_planet_minecart_rolercoaster,

                        initialize = delegate
                        {
                            amount_of_minecarts += 1;
                        },
                    },
                    new choice
                    {
                        option_text = $"Ride the {cart_adjective2} minecart",
                        next_situation = desert_planet_minecart_long_walk,

                        initialize = delegate
                        {
                            amount_of_minecarts += 1;
                        },
                    },
                    new choice
                    {
                        option_text = $"Try to feed the lava alien",
                        next_situation = desert_planet_lava_alien,

                        initialize = delegate
                        {
                            lava_alien_is_here = true;
                        },
                    },
                };

                // Pick the choices you can make
                Pick_Choices(desert_planet_cave_situation, possible_choices, 2);

                desert_planet_cave_situation.intro_text = "You walk down the stairs of the cave and you see a sign on the wall \n" +
                $"{(there_is_a_tunnel == true ? "It says 'GOLD MINE' with big bold letters. Despite apparently being a gold mine it's not too deep" : "It says 'Tread lightly'. 'Okay I will' you think to yourself")}\n" +
                "The walls seem to be made out of not too compact sandstone that could probably \n" +
                "be dug into with a decent digging tool *hint hint* \n" +
                $"{(amount_of_minecarts == 1 ? "You see a minecart that is on a rail that seems to be leading into a tunnel" : "")}" +
                $"{(amount_of_minecarts == 2 ? "You see two minecarts that lead into eatch tunnel. This is outragous! Which one to go with??" : "")}" +
                $"{(lava_alien_is_here ? "There's a little lava slime alien in the corner" : "")}";
            };
            #endregion

            #region Desert_Planet_Minecart_Rolercoaster
            desert_planet_minecart_rolercoaster.initialize = delegate ()
            {
                var rand = new Random();

                desert_planet_minecart_rolercoaster.intro_text =
                "You get in the minecart and put it into motion\n" +
                "The minecart starts moving very slowly and you notice how shaky it is\n" +
                "A couple of meters away there's a slope coming up and you start to wonder if this was such a great idea \n" +
                "You go down the slope and it speeds away and you fear for your life, but after a while you kind of start to get used to it \n" +
                "'Actually, this is kind of like a rolercoaster' you think to yourself, 'and a pretty fun one at that' \n" +
                "(+10 Morale)";

                Add_Morale(10);
            };
            Add_Back_To_Ship(desert_planet_minecart_rolercoaster);
            #endregion

            #region Desert_Planet_Minecart_long_walk
            desert_planet_minecart_long_walk.initialize = delegate ()
            {
                var rand = new Random();

                desert_planet_minecart_long_walk.intro_text =
                "You get in the minecart and put it into motion\n" +
                "The minecart starts moving very slowly and you notice how shaky it is\n" +
                "A couple of meters away there's a slope coming up and you start to wonder if this was such a great idea \n" +
                "You go down the slope and you fall off the minecart. Shoot! Now you have to walk back \n" +
                "You try to push the cart back on the track out of desparation but it's way too heavy \n" +
                "Atleast you bought granola bar that you can eat on your journey back\n" +
                "(-15 Morale) (-1 Food)";

                Add_Morale(-15);
                food -= 1;
            };
            Add_Back_To_Ship(desert_planet_minecart_long_walk);
            #endregion

            #region Desert_Planet_Tunnels_Dug
            desert_planet_tunnels_dug.initialize = delegate ()
            {
                var rand = new Random();

                desert_planet_tunnels_dug.intro_text =
                "You start digging with your pickaxe and it turns out that the sign was accurate\n" +
                "It seems like most of the gold vein has already been dug out but you manage to find a bit of gold here and there\n" +
                "You dig out about 40 space bucks worth of gold. How could you tell how much the gold is worth?\n" +
                "You used your portable space scale ofcourse. DUHHH!\n" +
                "(+40 Money)";

                money += 40;
            };
            Add_Back_To_Ship(desert_planet_tunnels_dug);
            #endregion

            #region Desert_Planet_City
            desert_planet_city.initialize = delegate ()
            {
                var things_you_see = new List<string>();

                var possible_choices = new List<choice>
                {
                    new choice
                    {
                        option_text = $"Go to farmers market",
                        next_situation = desert_planet_food_market,
                        initialize = delegate
                        {
                            things_you_see.Add("what seems to be a farmers market");
                        }
                    },
                    new choice
                    {
                        option_text = $"Go to museum",
                        next_situation = desert_planet_museum,
                        initialize = delegate
                        {
                            things_you_see.Add("a museum");
                        }
                    },
                    new choice
                    {
                        option_text = $"Converse with one of the {alien_creature_plural}",
                        next_situation = desert_planet_escape_from_aliens_city,
                        condition = delegate ()
                        {
                            if (!inventory.Contains(item_type.mysterious_object))
                                return $"You try to converse with some of the {alien_creature}s but they all seem too busy to talk";
                            return null;
                        },
                    },
                    new choice
                    {
                        option_text = $"Rob the bank",
                        next_situation = desert_planet_rob_bank,
                        initialize = delegate
                        {
                            things_you_see.Add("large bank");
                        },
                        condition = delegate ()
                        {
                            if (!inventory.Contains(item_type.laser_rifle))
                                return $"You walk into the bank but you realize that you don't have anything to rob the bank with... Oops. You walk out again";
                            return null;
                        },
                    },
                };

                Pick_Choices(desert_planet_city, possible_choices, 2);

                desert_planet_city.intro_text = "You walk into the city and it seems relatively big for being in the middle of a desert \n" +
                    $"There are {alien_creature_plural} everywhere you look and they all look like they've got somewhere to be \n" +
                    $"The houses are somewhat like skyscrapers, but built out of stone and they're also smaller, so without the skyscraping \n" +
                    $"{(things_you_see.Count == 1 ? $"You notice that you're right next to a {things_you_see[0]}!!" : "")}" +
                    $"{(things_you_see.Count == 2 ? $"You notice that you're right next to a {things_you_see[0]} and not to far away there's a {things_you_see[1]}" : "")}" +
                    $"{(things_you_see.Count == 3 ? $"You look around a bit and there seems to be a {things_you_see[0]}, a {things_you_see[1]} and a {things_you_see[2]} nearby" : "")}";
            };
            #endregion

            #region Desert_Planet_Escape_From_Aliens_City
            desert_planet_escape_from_aliens_city.initialize = delegate ()
            {
                var rand = new Random();

                desert_planet_escape_from_aliens_city.intro_text =
                $"The {alien_creature} sees the mysterious object you got with you and starts pointing at it while screaming \n" +
                $"Every {alien_creature} in the vicinity drops what they're doing and stares in shock\n" +
                $"The {alien_creature} starts slowly walking towards you and you get the feeling that he doesn't have good intentions \n" +
                "You start running away from it and they all start chasing you. The only logical thing to do here would be to escape...";
            };
            Add_Back_To_Ship(desert_planet_escape_from_aliens_city, true);
            #endregion

            #region Desert_Planet_Food_Market
            desert_planet_food_market.initialize = delegate ()
            {
                var rand = new Random();

                Assign_Food_Market(desert_planet_food_market);
                desert_planet_food_market.intro_text =
                "You walk up to the farmers market and they seem to have a bunch of food";
            };
            #endregion

            #region Desert_Planet_Rob_Bank
            desert_planet_rob_bank.initialize = delegate ()
            {
                var rand = new Random();

                desert_planet_rob_bank.intro_text =
                "You pull out your laser rifle and walk into the bank\n" +
                "'GET DOWN' you shout and everyone seems to comply. The teller start putting money in a bag\n" +
                $"You start hearing sirens from {alien_creature} cop cars. That means it's time to get out of there\n" +
                "Luckely there is a back door so you don't have to go through the main entrence where all the cop cars are\n" +
                "'Huh, bad bank desing' you say to yourself as you leave \n" +
                "(+100 Money) \n" +
                "You feel a bit sorry for scaring everyone in the bank though. You're not completely heartless after all \n" +
                $"On the other hand some rich {alien_creature} capitalist so that kind of softens the blow... But what would your mother think...? \n" +
                $"There's a lot of factors that come into play (not just game balancing) but in the end you feel like your mood decreased about 25% \n" +
                "(-25 Morale)";

                Add_Money(100);
                Add_Morale(-25);
            };
            Add_Back_To_Ship(desert_planet_rob_bank, true);
            #endregion

            #region Desert_Planet_Museum
            desert_planet_museum.initialize = delegate ()
            {
                var rand = new Random();

                desert_planet_museum.intro_text =
                $"You go to the museum and learn about the great desert war between {rand.Next(2050, 2500)} and {rand.Next(2500, 3100)}\n" +
                $"where the {alien_creature_plural} fought the {random_adjective[rand.Next(random_adjective.Count)]} creatures about this very city\n" +
                "(+10 Morale)";

                Add_Money(-5);
                Add_Morale(+10);
            };
            Add_Back_To_Ship(desert_planet_museum);
            #endregion

            #region Desert_Planet_Pond
            desert_planet_pond.initialize = delegate ()
            {
                bool there_is_alien_friend = false;

                bool there_is_treasure = false;

                bool there_is_fish = false;

                var possible_choices = new List<choice>
                {
                    new choice
                    {
                        option_text = $"Befriend the water alien",
                        next_situation = desert_planet_water_alien_friend,

                        initialize = delegate
                        {
                            there_is_alien_friend = true;
                        },
                    },
                    new choice
                    {
                        option_text = $"Drink water",
                        next_situation = desert_planet_drink_water,
                    },
                    new choice
                    {
                        option_text = $"Dive into the pond and get the treasure",
                        next_situation = desert_planet_pond_treasure,

                        condition = delegate ()
                        {
                            if (!inventory.Contains(item_type.swim_suit))
                                return $"You jump into the pond but you remember that you can't swim so you quickly get out of the water";
                            return null;
                        },

                        initialize = delegate
                        {
                            there_is_treasure = true;
                        },
                    },
                    new choice
                    {
                        option_text = $"Try to fish",
                        next_situation = desert_planet_pond_fish,

                        condition = delegate ()
                        {
                            if (!inventory.Contains(item_type.fishing_rod))
                                return $"You try to fish but it feels like something is missing... like a fishing rod or something...\n" +
                                "After about an hour of blankly staring at the fish waiting for it to catch itself,\n" +
                                "you decide that this isn't the most time efficent way of doing things";
                            return null;
                        },

                        initialize = delegate
                        {
                            there_is_fish = true;
                        },
                    }
                };

                Pick_Choices(desert_planet_pond, possible_choices, 2);

                desert_planet_pond.intro_text = $"{(there_is_treasure == true ? "You walk up to the pond and you notice that it's much bigger and deeper than you initially thought\nYou look down into the water and you see a treasure\n" : "You walk up to the pond, It's a relatively small pond but not too small\n")}" +
                $"{(there_is_alien_friend == true ? "There seems to be an alien sitting next to the water\nIt's like a little sentient blob of water. It's probably not too dangerous by the looks of it\n" : "")}" +
                $"{(there_is_fish == true ? "There are some fishes swiming around in the pond" : "")}";

            };
            #endregion

            #region Desert_Planet_Water_Alien_Friend
            desert_planet_water_alien_friend.initialize = delegate ()
            {
                var rand = new Random();

                desert_planet_water_alien_friend.intro_text =
                "You get down on one knee and reach out your hands, kind of like how you would greet a dog\n" +
                "The alien friend jumps on your shoulder to your suprise. It's your friend now it seems\n" +
                "(+1 Water Alien Pet)";

                crew.Add(crew_type.water_alien_pet);
            };
            Add_Back_To_Ship_With_Friend(desert_planet_water_alien_friend, "water alien");
            #endregion

            #region Desert_Planet_Drink_Water
            desert_planet_drink_water.initialize = delegate ()
            {
                var rand = new Random();

                desert_planet_drink_water.intro_text =
                "For some reason it just makes sense to you to drink some water in the middle of the desert\n" +
                "even though you have all the water you need in your ship\n" +
                "You don't even consider if it's clean or not\n" +
                "After drinking it you notice your stomach starts aching. You've got food poisoning, or water poisoning I guess\n" +
                "Luckily though it goes away relatively quick\n" +
                "(-10 Morale)";

                Add_Morale(-10);
            };
            Add_Back_To_Ship(desert_planet_drink_water);
            #endregion

            #region Desert_Planet_Pond_Treasure
            desert_planet_pond_treasure.initialize = delegate ()
            {
                var rand = new Random();

                desert_planet_pond_treasure.intro_text =
                "You put on your water suit and dive into the pond and get the treasure\n" +
                "After you've got out of the water you open it up. There's a bit of gold inside\n" +
                "(+25 money)";

                Add_Money(25);
            };
            Add_Back_To_Ship(desert_planet_pond_treasure);
            #endregion

            #region Desert_Planet_Caravan_Buy
            desert_planet_caravan_buy.initialize = delegate ()
            {
                var rand = new Random();

                Assign_Shop_Items(desert_planet_caravan_buy);
                desert_planet_caravan_buy.intro_text =
                "You walk up to the caravan and it seems that they're willing to trade";
            };
            #endregion

            #region Desert_Planet_Water_Alien_Friend
            desert_planet_lava_alien.initialize = delegate ()
            {
                var rand = new Random();

                desert_planet_lava_alien.intro_text =
                "You give the lava alien some food and it gobbles it up quickly\n" +
                "It seems to want to follow you everywhere you walk. I guess it's your friend now\n" +
                "(+1 Lava Alien Pet) (-1 Food)";

                food -= 1;
                crew.Add(crew_type.lava_alien_pet);
            };
            Add_Back_To_Ship_With_Friend(desert_planet_lava_alien, "lava alien");
            #endregion

            #region Desert_Planet_Dig_Dump
            desert_planet_dig_dump.initialize = delegate ()
            {
                desert_planet_dig_dump.intro_text =
                "You use your shovel to dig into the dump and you dig up an old lava suit. Neat";

                inventory.Add(item_type.lava_suit);
            };
            Add_Back_To_Ship(desert_planet_dig_dump);
            #endregion

            #region Desert_Planet_Pond_Fish
            desert_planet_pond_fish.initialize = delegate ()
            {
                desert_planet_pond_fish.intro_text =
                "You manage to catch some fish with your fishing rod\n" +
                "(+3 Food)";

                food += 3;
            };
            Add_Back_To_Ship(desert_planet_pond_fish);
            #endregion
        }
        #endregion

        #region Water_Planet

        static situation water_planet_situation = new situation();
        static situation water_planet_fish = new situation();
        static situation water_planet_dive = new situation();
        static situation water_planet_city = new situation();
        static situation water_planet_city_shop = new situation();
        static situation water_planet_mermaid = new situation();
        static situation water_planet_squid = new situation();
        static situation water_planet_kill_squid = new situation();
        static situation water_planet_squid_ask_to_move = new situation();
        static situation water_planet_squid_talk = new situation();

        static void Initialize_Water_Planet()
        {
            #region Water_Planet_Situation
            water_planet_situation.initialize = delegate ()
            {
                var possible_choices = new List<choice>
                {
                    new choice
                    {
                        option_text = $"Dive into the water",
                        next_situation = water_planet_dive,
                        condition = delegate ()
                        {
                            if (!inventory.Contains(item_type.swim_suit))
                                return "You jump into the water but you remember that you can't swim";
                            return null;
                        },
                    },
                    new choice
                    {
                        option_text = $"Try to fish",
                        next_situation = water_planet_fish,
                        condition = delegate ()
                        {
                            if (!inventory.Contains(item_type.fishing_rod))
                                return "You don't have anything to fish with, you silly goose";
                            return null;
                        },
                    },
                };

                // Pick three choices you can make
                Pick_Choices(water_planet_situation, possible_choices, 2);

                water_planet_situation.intro_text = "Looks like the next planet is a water planet \n" +
                    "You deploy your blow up raft on your ship right before landing on the surface \n" +
                    "There seems to be a fish shoal not too far off";
            };
            #endregion

            #region Water_Planet_Fish
            water_planet_fish.initialize = delegate ()
            {
                var rand = new Random();

                water_planet_fish.intro_text =
                "You pull out your fishing rod and start fishing\n" +
                "A couple of hours go by and you're a couple of fishes richer\n" +
                "(+6 Food)";

                food += 6;
            };
            Add_Back_To_Ship(water_planet_fish);
            #endregion

            #region Water_Planet_Dive
            water_planet_dive.initialize = delegate ()
            {
                var things_you_see = new List<string>();
                var possible_choices = new List<choice>
                {
                    new choice
                    {
                        option_text = $"Go to city",
                        next_situation = water_planet_city,
                        initialize = delegate
                        {
                            things_you_see.Add("a large city");
                        }
                    },
                    new choice
                    {
                        option_text = $"Talk to mermaid",
                        next_situation = water_planet_mermaid,
                        initialize = delegate
                        {
                            things_you_see.Add("a mermaid");
                        }
                    },
                    new choice
                    {
                        option_text = $"Walk up to squid",
                        next_situation = water_planet_squid,
                        initialize = delegate
                        {
                            things_you_see.Add("a big squid");
                        }
                    },
                };

                Pick_Choices(water_planet_dive, possible_choices, 2);

                water_planet_dive.intro_text = "You dive into the water with your swim suit on and look around you\n" +
                $"You see {things_you_see[0]} and {things_you_see[1]}";
            };
            #endregion

            #region Water_Planet_City
            water_planet_city.initialize = delegate ()
            {
                var possible_choices = new List<choice>
                {
                    new choice
                    {
                        option_text = $"Go to shop",
                        next_situation = water_planet_city_shop
                    },
                };

                Pick_Choices(water_planet_city, possible_choices, 1);

                water_planet_city.intro_text = "You enter the underwater city. It seems to be inhabited by humanoid underwater people";
            };
            #endregion

            #region Water_Planet_City_Shop
            water_planet_city_shop.initialize = delegate ()
            {
                var rand = new Random();

                Assign_Shop_Items(water_planet_city_shop);
                water_planet_city_shop.intro_text =
                "You walk into the store and start looking at the items";
            };
            #endregion

            #region Water_Planet_Mermaid
            water_planet_mermaid.initialize = delegate ()
            {
                var rand = new Random();

                water_planet_mermaid.intro_text =
                "You walk up to the mermaid. The closer you get to it the more you start to realize that this is not a normal mermaid\n" +
                "After you've walked up to it you notice that it's almost twice your size and really muscular and monstorous\n" +
                "Despite it's threatening aura you manage to squeek out a little 'hi'\n" +
                "The mermaid punches you in the stomach and steals your wallet. You've just been mugged by a mermaid. Shoot\n" +
                "Luckily you didn't bring all your life savings in it so it's not too much of a blow\n" +
                "(-15 money) (-10 morale)";

                Add_Money(-15); Add_Morale(-10);
            };
            Add_Back_To_Ship(water_planet_mermaid);
            #endregion

            #region Water_Planet_Squid
            water_planet_squid.initialize = delegate ()
            {
                var possible_choices = new List<choice>
                {
                    new choice
                    {
                        option_text = "Kill the squid",
                        next_situation = water_planet_kill_squid,
                        condition = delegate ()
                        {
                            if (!inventory.Contains(item_type.laser_rifle))
                                return "You don't really have anything to kill the squid with\n" +
                                "plus it's a pretty big scary creature so you decide not to attack it";
                            return null;
                        },
                    },
                    new choice
                    {
                        option_text = "Tell the squid to move away from the gold",
                        next_situation = water_planet_squid_ask_to_move,
                        condition = delegate ()
                        {
                            if (!inventory.Contains(item_type.laser_rifle))
                                return "The squid doesn't relly want to and who are you to tell it what to do?";
                            return null;
                        },
                    },
                    new choice
                    {
                        option_text = "Talk to squid",
                        next_situation = water_planet_squid_talk
                    },
                };

                Pick_Choices(water_planet_squid, possible_choices, 3);

                water_planet_squid.intro_text = "You walk up to the squid and you notice that it's sitting on a pile of gold\n";
            };
            #endregion

            #region Water_Planet_Kill_Squid
            water_planet_kill_squid.initialize = delegate ()
            {
                var rand = new Random();

                water_planet_kill_squid.intro_text =
                "You shoot the squid right between its eyes with your laser rifle and it dies\n" +
                "The gold is yours, plus you can eat the squid meat, although you feel like you could've been more peaceful here\n" +
                "(+50 Money) (+6 Food) (-15 Morale)";

                food += 6;
                Add_Money(50);
                Add_Morale(-15);
            };
            Add_Back_To_Ship(water_planet_kill_squid);
            #endregion

            #region Water_Planet_Squid_Ask_To_Move
            water_planet_squid_ask_to_move.initialize = delegate ()
            {
                var rand = new Random();

                water_planet_squid_ask_to_move.intro_text =
                "You tell the squid to move and it seems unfazed. It inspects you from head to toe with and evil look in its eyes\n" +
                "Then suddenly it sees that you're carrying a laser rifle and swims away terrified\n" +
                "You pick up the gold\n" +
                "(+50 Money)";

                Add_Money(50);
            };
            Add_Back_To_Ship(water_planet_squid_ask_to_move);
            #endregion

            #region Water_Planet_Squid_Talk
            water_planet_squid_talk.initialize = delegate ()
            {
                var rand = new Random();

                water_planet_squid_talk.intro_text =
                "You say hi to the squid and it says hi back\n" +
                "The squid seems pretty happy to have a visitor\n" +
                "You sit down with the squid and have a leangthy conversation over a cup of tea\n" +
                "How does squids drink tea under water you may ask, well instead of using water in their tea they use air" +
                "(+15 morale)";

                Add_Morale(15);
            };
            Add_Back_To_Ship(water_planet_squid_talk);
            #endregion
        }
        #endregion

        #region Gas_Station
        static situation gas_station_situation = new situation();
        static situation gas_station_store = new situation();
        static situation gas_station_refill_gas = new situation();

        static void Initialize_Gas_Station()
        {
            #region Gas_Station_Situation
            gas_station_situation.initialize = delegate ()
            {
                var possible_choices = new List<choice>
                {
                    new choice
                    {
                        option_text = $"Go into the store and look at wares",
                        next_situation = gas_station_store,
                    },
                    new choice
                    {
                        option_text = $"Buy fuel",
                        next_situation = gas_station_refill_gas,
                    },
                };

                Pick_Choices(gas_station_situation, possible_choices, 2);

                gas_station_situation.intro_text = "You arive at a gas station for travelers\n" +
                "It seems like it's just about to close so you won't have time to both refill your fuel and look around in the store\n" +
                "Choose wisely...";
            };
            #endregion

            #region Gas_Station_Store
            gas_station_store.initialize = delegate ()
            {
                var rand = new Random();

                Assign_Shop_Items(gas_station_store);
                gas_station_store.intro_text =
                "You walk into the gas station and look at the items...";
            };
            #endregion

            #region Gas_Station_Refill_Gas
            gas_station_refill_gas.initialize = delegate ()
            {
                var rand = new Random();

                Assign_Fuel_Prices(gas_station_refill_gas);
                gas_station_refill_gas.intro_text =
                "You take a look at the gas prices...";
            };
            #endregion
        }
        #endregion

        #endregion

        #region State_Initialize
        static void State_Initialize()
        {
            food = 7;
            fuel = 7;
            money = 40;
            morale = 100;
            planets_explored = 0;

            crew = new List<crew_type>();

            crew.Add(crew_type.player);

            inventory = new List<item_type>();
            //inventory.Add(item_type.swim_suit);

            prices = new Dictionary<item_type, int>
            {
                { item_type.shovel, 25},
                { item_type.pickaxe, 35},
                { item_type.fishing_rod, 20},
                { item_type.mysterious_object, 70},
                { item_type.laser_rifle, 40},

                { item_type.lava_suit, 40},
                { item_type.toxic_slime_suit, 30},
                { item_type.swim_suit, 25}
            };

            random_adjective = new List<string>();
            random_adjective.Add("shiny");
            random_adjective.Add("fancy");
            random_adjective.Add("old");
            random_adjective.Add("metal");
            random_adjective.Add("green");
            random_adjective.Add("blue");
            random_adjective.Add("red");
            random_adjective.Add("yellow");
            random_adjective.Add("damaged");
            random_adjective.Add("monsterous");

            game_text = new List<string>();

            #region Back_To_Ship_Situation
            back_to_ship.initialize = delegate ()
            {
                Back_To_Ship();
            };
            #endregion
        }
        static void Add_Morale(int morale_to_add)
        {
            if (morale + morale_to_add > 99)
                morale = 100;
            else
                morale += morale_to_add;
        }

        static void Add_Money(int money_to_add)
        {
            money += money_to_add;

            if (money < 0)
                money = 0;
        }

        static void Lose_If_You_Are_Supposed_To()
        {
            if (food < 0 || morale < 0 || fuel < 0)
            {
                Console.Clear();
                Console.SetCursorPosition(1, 1);
                Console.Write("GAME OVER!!");
                Console.SetCursorPosition(1, 3);
                if (food < 0)
                    Console.Write($"You ran out of food so you ate yourself to death");
                else if (morale < 0)
                    Console.Write($"You ran out of morale so you decided to go to some random planet and become a farmer or something");
                else if (fuel < 0)
                    Console.Write($"You ran out of fuel so you went to the closest resort planets to live a peaceful life there for the rest of your life");
                Console.SetCursorPosition(1, 5);
                Console.Write($"You managed to explore {planets_explored} planets");

                if (food < 0 || morale < 0 || fuel < 0)
                {
                    for (; ; )
                        Console.ReadKey();
                }
            }
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
            int pos_x = 128;
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
            int pos_x = 124;
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
                else if (crew[i] == crew_type.water_alien_pet)
                {
                    Console.SetCursorPosition(pos_x + i * 7, pos_y);
                    Console.WriteLine("     ");
                    Console.SetCursorPosition(pos_x + i * 7, pos_y + 1);
                    Console.WriteLine("     ");
                    Console.SetCursorPosition(pos_x + i * 7, pos_y + 2);
                    Console.WriteLine("  ▒  ");
                    Console.SetCursorPosition(pos_x + i * 7, pos_y + 3);
                    Console.WriteLine(" ▒░▒ ");
                    Console.SetCursorPosition(pos_x + i * 7, pos_y + 4);
                    Console.WriteLine("▒░░░▒");
                    Console.SetCursorPosition(pos_x + i * 7, pos_y + 5);
                    Console.WriteLine("▒░░░▒");
                    Console.SetCursorPosition(pos_x + i * 7, pos_y + 6);
                    Console.WriteLine(" ▒▒▒ ");
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
            UI_Draw_Big_Line(false);
        }
        #endregion

        #region UI_Add_Game_Text
        static void UI_Add_Game_Text(string text)
        {
            string[] text_lines = text.Split('\n');

            foreach (string line in text_lines)
            {
                game_text.Add(line);
            }
        }
        #endregion

        #region UI_Draw_All
        static void UI_Draw_Big_Line(bool add_to_game_text = true)
        {
            string line = "_____________________________________________________________________________________________________________";

            if (add_to_game_text == true)
                game_text.Add(line);
            else
                Console.WriteLine(line);
        }
        #endregion

        //Choices
        #region Pick_Choices
        private static void Pick_Choices(situation current_situation, List<choice> possible_choices, int number_of_choices)
        {
            var rand = new Random();

            current_situation.choices.Clear();
            for (int current_option = 0; current_option < number_of_choices; current_option++)
            {
                int random_index = rand.Next(0, possible_choices.Count);
                choice selected_choice = possible_choices[random_index];


                selected_choice.option_text = $"{current_option + 1}. {selected_choice.option_text}";

                current_situation.choices.Add(selected_choice);
                possible_choices.Remove(selected_choice);

                if (selected_choice.initialize != null)
                {
                    selected_choice.initialize();
                }
            }

            Add_Back_To_Ship(current_situation);

        }
        #endregion

        #region Make_A_Choice
        static void Make_A_Choice(situation this_situation)
        {
            this_situation.initialize();
            do
            {
                UI_Draw_Big_Line();

                //Output intro text
                UI_Add_Game_Text(this_situation.intro_text);

                game_text.Add("");

                //Output choices
                foreach (choice situation_choice in this_situation.choices)
                {
                    game_text.Add(situation_choice.option_text);
                }

                game_text.Add("");

                //Ask user for choice
                int choice;
                int choices_count = this_situation.choices.Count;

                for (; ; )
                {
                    UI_Draw_All();
                    Console.SetCursorPosition(2, 53);

                    for (; ; )
                    {
                        ConsoleKeyInfo pressed_key = Console.ReadKey(true);
                        bool succeded = Int32.TryParse(pressed_key.KeyChar.ToString(), out choice);
                        if (succeded && choice >= 1 && choice <= choices_count)
                            break;
                    }

                    choice selectedChoice = this_situation.choices[choice - 1];
                    string fail_text = null;

                    if (selectedChoice.condition != null)
                    {
                        fail_text = selectedChoice.condition();
                    }

                    if (fail_text == null)
                    {
                        Make_A_Choice(selectedChoice.next_situation);
                        break;
                    }
                    else
                    {
                        UI_Add_Game_Text(fail_text);
                        game_text.Add("");
                        UI_Draw_All();
                        Console.ReadKey();
                        break;
                    }
                }
            } while (true);
        }
        #endregion

        #region Back_To_Ship
        static void Back_To_Ship()
        {
            planets_explored += 1;

            UI_Draw_Big_Line();
            int morale_to_add = -10 + crew.Count * 5;
            int food_to_lose = -1 * crew.Count;
            game_text.Add("You get on your ship and set course to your next planet");
            game_text.Add("While you're waiting to get there you eat some food");

            if (morale_to_add < 0 && morale > 50)
                game_text.Add("You feel a bit lonely unfortunately");
            else if (morale_to_add < 0 && morale < 25)
                game_text.Add("You feel as if you want to murder orphans. Sorry to bring you the news but you need help");
            else if (morale_to_add < 0 && morale < 50)
                game_text.Add("The loneliness is starting to get to you");
            else if (morale_to_add > 0 && morale < 35)
                game_text.Add("You're a bit down, but luckily your friends are there with you");
            else
                game_text.Add("Ain't every thing swell?");

            game_text.Add($"({food_to_lose} Food)");
            game_text.Add($"(-1 Fuel)");
            if(morale_to_add > 0)
                game_text.Add($"(+{morale_to_add} Morale)");
            else
                game_text.Add($"({morale_to_add} Morale)");
            Add_Morale(morale_to_add);
            food += food_to_lose;
            fuel -= 1;

            UI_Draw_All();
            Art_Spaceship_Animation();
            Lose_If_You_Are_Supposed_To();

            game_text.Add("");
            game_text.Add("After a smooth ride you arrive at your next destination...");

            UI_Draw_All();

            Console.ReadKey();

            var rand = new Random();

            int planet_to_choose = rand.Next(0, 3);

            if (planet_to_choose == 0)
            {
                Initialize_Desert_Planet();
                Make_A_Choice(desert_planet_situation);
            }
            else if (planet_to_choose == 1)
            {
                Initialize_Water_Planet();
                Make_A_Choice(water_planet_situation);
            }
            else if (planet_to_choose == 2)
            {
                Initialize_Gas_Station();
                Make_A_Choice(gas_station_situation);
            }
        }
        #endregion

        static void Main(string[] args)
        {
            //Variables and other stuff to set at start
            Console.CursorVisible = false;
            Console.SetWindowSize(155, 55);

            State_Initialize();
            Back_To_Ship();
        }
    }
}