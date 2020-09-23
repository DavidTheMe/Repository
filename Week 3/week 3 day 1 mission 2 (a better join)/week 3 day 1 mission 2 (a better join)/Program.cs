using System;
using System.Collections.Generic;
using System.Threading;

namespace week_3_day_1_mission_2__a_better_join_
{
    class Program
    {
        static void Main(string[] args)
        {
            var members = new List<string> { "Leo", "Pedro", "Andrea", "Andrea"};
            Console.WriteLine($"{JoinWithAnd(members)} are currently in the party.");
        }
        static string JoinWithAnd(List<string> items, bool useSerialComma = true)
        {
            int count = items.Count;

            if (count == 0)
                return "No people";
            else if (count == 1)
                return items[0];
            else if (count == 2)
                return $"{items[0]} and {items[1]}";
            else if ((count == 3) && (useSerialComma == false))
                return $"{items[0]}, {items[1]} and {items[2]}";
            else if ((count == 3) && (useSerialComma == true))
                return $"{items[0]}, {items[1]}, and {items[2]}";
            else if ((count == 4) && (useSerialComma == false))
                return $"{items[0]}, {items[1]}, {items[2]} and {items[3]}";
            else if ((count == 4) && (useSerialComma == true))
                return $"{items[0]}, {items[1]}, {items[2]}, and {items[3]}";
            else
                return "Too many people";
        }
    }
}
