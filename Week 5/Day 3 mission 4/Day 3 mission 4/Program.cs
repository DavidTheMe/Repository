using System;
using System.Collections.Generic;
using System.Threading;

namespace Day_3_mission_4
{
    class Program
    {
        static void Main(string[] args)
        {
                string diceNotation = Console.ReadLine();
            for (int i = 0; i < 10; i++)
            {
                NewDiceRoll(diceNotation);
            }
        }

        #region Old_DiceRoll
        static void DiceRoll(int dices_amount, int dices_sides, int fixed_number)
        {
            var dice_rolls = new List<int>();
            var rand = new Random();
            int final_number = 0;

            for (int i = 0; i < dices_amount; i++)
            {
                int temp_dice_roll = rand.Next(1, dices_sides + 1);

                dice_rolls.Add(temp_dice_roll);

                Console.WriteLine($"You rolled a {temp_dice_roll}");
                Console.WriteLine();
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

            Console.WriteLine($"The fixed number is {fixed_number}");
            Console.WriteLine();

            final_number += fixed_number;

            Console.WriteLine($"The final amount is {final_number}");
            Console.WriteLine();

            Console.ReadLine();
        }
        #endregion

        #region New_DiceRoll
        static void NewDiceRoll(string diceNotation)
        {
            int dices_amount = (int)Char.GetNumericValue(diceNotation[0]);
            int dices_sides = (int)Char.GetNumericValue(diceNotation[2]);
            int fixed_number = (int)Char.GetNumericValue(diceNotation[4]);

            var dice_rolls = new List<int>();
            var rand = new Random();
            int final_number = 0;

            for (int i = 0; i < dices_amount; i++)
            {
                int temp_dice_roll = rand.Next(1, dices_sides + 1);

                dice_rolls.Add(temp_dice_roll);

                Console.WriteLine($"You rolled a {temp_dice_roll}");
                Console.WriteLine();
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

            Console.WriteLine($"The fixed number is {fixed_number}");
            Console.WriteLine();

            final_number += fixed_number;

            Console.WriteLine($"The final amount is {final_number}");
            Console.WriteLine();

            Thread.Sleep(1000);
        }
        #endregion
    }
}
