using System;
using System.Collections.Generic;
using System.Threading;

namespace City
{
    class Program
    {
        static void DrawStuff(string[,] cordinates, int map_width, int map_height)
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            for (int y = 0; y < map_height; y++)
            {
                for (int x = 0; x < map_width; x++)
                {
                    if (cordinates[x, y] == "road")
                        Console.Write("#");
                    else if (cordinates[x, y] == "empty")
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        static void GenerateRoad(string[,] cordinates, int startX, int startY, int direction)
        {
            //Variables

            
            int map_width = cordinates.GetLength(0);
            int map_height = cordinates.GetLength(1);

            for (int i = 0; i < 30; i++)
            {
                if (direction == 0)
                {
                    if (startX + i < map_width)
                    {
                        cordinates[startX + i, startY] = "road";
                    }
                }
                else if (direction == 1)
                {
                    if (startX - i >= 0)
                    {
                        cordinates[startX - i, startY] = "road";
                    }
                }
                else if (direction == 2)
                {
                    if (startY + i < map_height)
                    {
                        cordinates[startX, startY + i] = "road";
                    }
                }
                else if (direction == 3)
                {
                    if (startY - i >= 0)
                    {
                        cordinates[startX, startY - i] = "road";
                    }
                }
            }

        }
        static void Main(string[] args)
        {
            //variables
            Console.CursorVisible = false;

            int map_height = 20;
            int map_width = 80;

            var rand = new Random();

            //Create map
            var cordinates = new string[map_width, map_height];
            for (int y = 0; y < map_height; y++)
            {
                for (int x = 0; x < map_width; x++)
                {
                    cordinates[x, y] = "empty";
                }
            }

            //Generate roads
            for (int i = 0; i < 1000; i++)
            {
                GenerateRoad(cordinates, rand.Next(1, map_width), rand.Next(1, map_height), rand.Next(0, 4));
                DrawStuff(cordinates, map_width, map_height);
                Thread.Sleep(100);
            }
        }
    }
}