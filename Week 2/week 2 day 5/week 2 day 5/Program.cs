using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Baselisk_Battle
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variables
            List<string> members = new List<string>();

            members.Add("Leo");
            members.Add("Pedro");
            members.Add("Andrea");
            members.Add("Steve");

            List<int> membersHP = new List<int>();

            membersHP.Add(20);
            membersHP.Add(20);
            membersHP.Add(20);
            membersHP.Add(20);

            List<string> enemies = new List<string>();

            enemies.Add("enemy orc");
            enemies.Add("enemy mage");
            enemies.Add("enemy troll");

            List<int> enemiesHP = new List<int>();

            enemiesHP.Add(12);
            enemiesHP.Add(20);
            enemiesHP.Add(84);

            var random = new Random();

            int damage;

            //Functions
            void SwordHit(int enemyNr, string attacker)
            {
                damage = random.Next(2, 13);

                if (attacker.Contains("enemy"))
                {
                    membersHP[enemyNr] = membersHP[enemyNr] - damage;
                    Console.WriteLine($"The {attacker} attacked {members[enemyNr]} for {damage} damage");
                    Console.WriteLine($"The {members[enemyNr]} now has {membersHP[enemyNr]}");
                    Console.WriteLine("");
                }
                else
                {
                    enemiesHP[enemyNr] = enemiesHP[enemyNr] - damage;
                    Console.WriteLine($"{attacker} attacked {enemies[enemyNr]} with their sword for {damage} damage");
                    Console.WriteLine($"The {enemies[enemyNr]} now has {enemiesHP[enemyNr]}");
                    Console.WriteLine("");
                }
            }

            void simulateBattle()
            {
                if (membersHP[0] + membersHP[1] + membersHP[2] + membersHP[3] < 1)
                    Console.WriteLine("All party members died! GAME OVER!");
                else if (enemiesHP[0] + enemiesHP[1] + enemiesHP[2] < 1)
                    Console.WriteLine("The enemies have been defeated");
                else
                {
                    if (membersHP[0] > 0)
                        SwordHit(random.Next(1, enemies.Count), members[0]);

                    if (membersHP[1] > 0)
                        SwordHit(random.Next(1, enemies.Count), members[1]);

                    if (membersHP[2] > 0)
                        SwordHit(random.Next(1, enemies.Count), members[2]);

                    if (membersHP[3] > 0)
                        SwordHit(random.Next(1, enemies.Count), members[3]);

                    if (enemiesHP[0] > 0)
                        SwordHit(random.Next(1, members.Count), enemies[0]);

                    if (enemiesHP[1] > 0)
                        SwordHit(random.Next(1, members.Count), enemies[1]);

                    if (enemiesHP[2] > 0)
                        SwordHit(random.Next(1, members.Count), enemies[2]);
                }
            }

            //Begining text
            Console.WriteLine($"A party of warriors, {members[0]}, {members[1]}, {members[2]} and {members[3]} arrives!");
            Console.WriteLine("");
            Console.WriteLine($"There are some enemies ahead!");
            Console.WriteLine("");

            //Simulate battle
            for (; ; )
            {
                if (membersHP[0] + membersHP[1] + membersHP[2] + membersHP[3] > 1)
                {
                    if (enemiesHP[0] + enemiesHP[1] + enemiesHP[2] > 1)
                        simulateBattle();
                }
            }
        }
    }
}