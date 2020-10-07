using System;
using System.Threading;

namespace Seasons
{
    class Program
    {
        
        static string CreateOrdinalNr(int number)
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
        static string CreateDayDescription(int day, int season, int year)
        {
            return $"{CreateOrdinalNr(day)} day of {DetermineSeason(season)}, year {year}";
        }
        static string DetermineSeason (int season_nr)
        {
            string[] seasons = { "spring", "summer", "fall", "winter" };
            return seasons[season_nr];
        }
        static void Main(string[] args)
        {
            int dates_amount = 10;

            var rand = new Random();
            //day, season, year
            int[,] date = new int[dates_amount, 3];

            //rand.Next(1, 41), rand.Next(1, 5), rand.Next(1, 3000)
            for (int i = 0; i < dates_amount; i++)
            {
                date[i, 0] = rand.Next(1, 41);
                date[i, 1] = rand.Next(0, 4);
                date[i, 2] = rand.Next(1, 3000);
            }
            for (int i = 0; i < dates_amount; i++)
            {
                Console.WriteLine(CreateDayDescription(date[i, 0], date[i, 1], date[i, 2]));
            }
        }
    }
}
