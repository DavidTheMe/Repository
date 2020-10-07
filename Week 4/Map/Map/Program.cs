using System;
using System.Collections.Generic;

namespace Map
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variables
            string map_name = "Pretty cool map B)";

            int map_height = 25;
            int map_width = 100;
            var random = new Random();

            int road_straightness = 9;
            int river_straightness = 5;
            int river_width = 5;
            int blocks_before_river = 5;
            int wall_straightness = 7;

            int road_y_start = random.Next(map_height);
            var road_y = new List<int> { road_y_start };

            int river_x_start = random.Next(map_width);
            var river_x = new List<int> { river_x_start };

            var bridge_y_upper_edge = new List<int> { 0, 0 };
            var bridge_y_lower_edge = new List<int> { 0, 0 };

            int wall_block_x = -1;
            int wall_block_y = -1;

            int forest_width = map_width / 4;

            int wall_x_start = random.Next(forest_width + 4, map_width - (map_width / 2));
            var wall_x = new List<int> { wall_x_start };

            int river_x_axis = random.Next(forest_width, map_width);

            int town_x = random.Next(1, map_width);
            int town_y = random.Next(1, map_height);

            //Generate wall
            for (int y = 1; y < map_height; y++)
            {
                if (random.Next(1, wall_straightness) == 1)
                    wall_x.Add(wall_x[y - 1] + random.Next(-1, 2));
                else
                    wall_x.Add(wall_x[y - 1]);
            }

            //Generate river
            for (int y = 1; y < map_height; y++)
            {
                if (random.Next(1, river_straightness) == 1)
                    river_x.Add(river_x[y - 1] + random.Next(-1, 2));
                else
                    river_x.Add(river_x[y - 1]);
            }

            //Generate road
            for (int x = 1; x < map_width; x++)
            {
                int beach_x = river_x[road_y[x - 1]] - river_width - blocks_before_river;

                int before_wall_x = wall_x[road_y[x - 1]] - 2;

                //Bridge
                if (x >= beach_x && x <= beach_x + river_width + blocks_before_river)
                {
                    bridge_y_upper_edge.Add(road_y[x - 1] + 1);
                    bridge_y_lower_edge.Add(road_y[x - 1] - 1);
                    road_y.Add(road_y[x - 1]);
                }
                //Wall block
                else if (x >= before_wall_x && x <= before_wall_x + 2)
                {
                    bridge_y_upper_edge.Add(-1);
                    bridge_y_lower_edge.Add(-1);
                    wall_block_x = x - 1;
                    wall_block_y = road_y[x - 1];
                    road_y.Add(road_y[x - 1]);
                    wall_x[wall_block_y + 1] = wall_block_x + 2;
                    wall_x[wall_block_y - 1] = wall_block_x + 2;
                }
                //Road
                else if (random.Next(1, road_straightness) == 1)
                {
                    bridge_y_upper_edge.Add(-1);
                    bridge_y_lower_edge.Add(-1);
                    if (road_y[x - 1] < 1)
                        road_y.Add(road_y[x - 1] + random.Next(0, 2));
                    else if (road_y[x - 1] > map_height - 2)
                        road_y.Add(road_y[x - 1] + random.Next(-1, 1));
                    else
                        road_y.Add(road_y[x - 1] + random.Next(-1, 2));
                }
                else
                {
                    bridge_y_upper_edge.Add(-1);
                    bridge_y_lower_edge.Add(-1);
                    road_y.Add(road_y[x - 1]);
                }
            }

            //Draw shit
            for (int y = 0; y < map_height + 1; y++)
            {
                //Change river direction
                int i = random.Next(1, 6);

                if (i == 1)
                    river_x_axis -= 1;

                if (i == 2)
                    river_x_axis += 1;

                for (int x = 0; x < map_width + 1; x++)
                {
                    Console.ForegroundColor = ConsoleColor.White;

                    //Corners of borders
                    if (x == 0 && y == 0)
                    {
                        Console.Write("╔");
                    }
                    else if (x == map_width && y == 0)
                    {
                        Console.Write("╗");
                    }
                    else if (x == 0 && y == map_height)
                    {
                        Console.Write("╚");
                    }
                    else if (x == map_width && y == map_height)
                    {
                        Console.Write("╝");
                    }
                    //Borders
                    else if (x == 0 || x == map_width)
                    {
                        Console.Write("║");
                    }
                    else if (y == 0 || y == map_height)
                    {
                        Console.Write("═");
                    }
                    //Title
                    else if (y == 1 && x == (map_width / 2) - (map_name.Length / 2))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(map_name);
                        x += map_name.Length - 1;
                    }
                    //Road
                    else if (road_y[x] == y)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("#");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    //Bridge
                    else if (bridge_y_upper_edge[x] == y || bridge_y_lower_edge[x] == y)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("=");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    //Wall_block_upper
                    else if (x == wall_block_x && (y == wall_block_y + 1))
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("[");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (x == wall_block_x + 1 && (y == wall_block_y + 1))
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("]");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    //Wall_block_lower
                    else if (x == wall_block_x && (y == wall_block_y - 1))
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("[");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (x == wall_block_x + 1 && (y == wall_block_y - 1))
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("]");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    //Wall
                    else if ((wall_x[y] > x) && (wall_x[y] < x + 2 + 1))
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("┼");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    //River
                    else if ((river_x[y] > x) && (river_x[y] < x + river_width + 1))
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("S");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    //Forest
                    else if (x < forest_width)
                    {
                        if (random.Next(1, x) < 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("T");
                        }
                        else if (random.Next(1, x) < 2)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write(".");
                        }
                        else
                        {
                            Console.Write(" ");
                        }

                    }
                    //Empty space
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
