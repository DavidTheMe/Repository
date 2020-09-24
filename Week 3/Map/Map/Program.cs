using System;

namespace Map
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variables
            string map_name = "Holy cr*p this map is fl*ppin' epic yo!!!!!!";
            
            int map_height = 25;
            int map_width = 100;

            var random = new Random();

            int forest_width = map_width / 4;

            int river_x_axis = random.Next(forest_width, map_width);

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
                    else if(y == 1 && x == (map_width / 2) - (map_name.Length / 2))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(map_name);
                        x += map_name.Length - 1;
                    }
                    //River
                    else if (x == river_x_axis || x == river_x_axis + 1 || x == river_x_axis + 2 || x == river_x_axis + 3 || x == river_x_axis + 4)
                    {
                        int j = random.Next(1, 5);

                        if (j == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("o");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("S");
                        }
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
