using System;
using System.Collections.Generic;

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

        static void DiceRoll(int dices_amount, int dices_sides, int fixed_number)
        {
            var dice_rolls = new List<int>();
            var rand = new Random();
            int final_number = 0;

            for (int i = 0; i < dices_amount; i++)
            {
                int temp_dice_roll = rand.Next(1, dices_sides + 1);

                dice_rolls.Add(temp_dice_roll);
            }

            for (int i = 0; i < dice_rolls.Count; i++)
            {
                if (i == 0)
                {
                    final_number = dice_rolls[0];
                }
                else 
                {
                    final_number += dice_rolls[i];
                }
            }
            final_number += fixed_number;
        }
    }
}