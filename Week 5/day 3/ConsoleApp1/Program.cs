using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Want to meet for lunch? I'll leave the restaurant address in the south maintenance closet. Bring an ASCII chart, the message will be coed.

            bool usernameIsOkay = true;

            for (; ; )
            {
                string username = Console.ReadLine();

                for (int i = 0; i < username.Length; i++)
                {
                    char current_char = username[i];
                    int character = (int)Convert.ToChar(current_char);

                    if ((character > 96 && character < 123) || (character > 47 && character < 58))
                    {
                        usernameIsOkay = true;
                    }
                    else
                    {
                        usernameIsOkay = false;
                        break;
                    }
                }

                if (usernameIsOkay == true)
                {
                    Console.WriteLine($"{username} = good");
                }
                else
                {
                    Console.WriteLine($"{username} = bad");
                }
                Console.WriteLine();
            }
        }
    }
}