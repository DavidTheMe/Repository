using System;
using System.Reflection.Metadata.Ecma335;

namespace Ordinal_numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int number = 1; number < 11; number++)
            {
                Console.WriteLine($"{number}{Function(number)}");
            }
            
            static string Function(int funcNumber)
            {
                if (funcNumber == 1)
                    return "st";
                else if (funcNumber == 2)
                    return "nd";
                else if (funcNumber == 3)
                    return "rd";
                else 
                    return "th";
            }

        }
    }
}
