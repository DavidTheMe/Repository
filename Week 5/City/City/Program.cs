using System;
using System.Collections.Generic;
using System.Threading;

namespace City
{
    class Program
    {
        static void DrawStuff(string[,] cordinates, int map_width, int map_height)
        {
            for (int x = 0; x < map_width; x++)
            {
                for (int y = 0; y < map_height; y++)
                {
                    if (cordinates[x, y] == "road")
                        Console.Write("#");
                    else if (cordinates[x, y] == "empty")
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        static void GenerateRoad(string[,] cordinates, int startX, int startY, int direction, int map_width, int map_height)
        {
            for (int i = 0; i < 15; i++)
            {
                if (startX + i < 1 || startX + i > map_width || startY + i < 1 || startY + i > map_height)
                    break;
                else if (direction == 0)
                    cordinates[startX + i, startY] = "road";
                else if (direction == 1)
                    cordinates[startX - i, startY] = "road";
                else if (direction == 2)
                    cordinates[startX, startY + i] = "road";
                else if (direction == 3)
                    cordinates[startX, startY - i] = "road";
            }

        }
        static void Main(string[] args)
        {
            //variables
            int map_height = 20;
            int map_width = 40;

            var rand = new Random();

            //Create map
            var cordinates = new string[map_width, map_height];
            for (int y = 0; y < map_height; y++)
            {
                for (int x = 0; x < map_height; x++)
                {
                    cordinates[x, y] = "empty";
                }
            }

            //Generate roads
            for (int i = 0; i < 10; i++)
            {
                GenerateRoad(cordinates, rand.Next(1, map_width), rand.Next(1, map_height), rand.Next(0, 4), map_width, map_height);
                DrawStuff(cordinates, map_width, map_height);
                Thread.Sleep(10);
            }
        }
    }
}