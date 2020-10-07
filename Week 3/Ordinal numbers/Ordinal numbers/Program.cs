using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading;

namespace Ordinal_numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int number = 1; number < 100; number++)
            {
                Console.WriteLine($"{OrdinalNumber(number)}");
                Thread.Sleep(200);
            }

            static string OrdinalNumber(int number)
            {
                int last_digit = number % 10;
                int second_last_digit = 0;
                if (number > 10)
                {
                    second_last_digit = number / 10;
                    second_last_digit = second_last_digit % 10;
                }
                if (second_last_digit == 1)
                    return number + "th";
                else if (last_digit == 1)
                    return number + "st";
                else if (last_digit == 2)
                    return number + "nd";
                else if (last_digit == 3)
                    return number + "rd";
                else
                    return number + "th";
            }
        }
    }
}
