using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
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

            var random = new Random();

            int damage;
            int baseliskHp = random.Next(24, 81);

            //Text
            Console.WriteLine($"A party of warriors, {members[0]}, {members[1]}, {members[2]} and {members[3]} arrives!");
            Console.WriteLine("");
            Console.WriteLine($"There is a baselisk with {baseliskHp} HP");
            Console.WriteLine("");

            //Warriors different attacks

            void SwordHit(string member)
            {
                damage = random.Next(2, 13);
                baseliskHp = baseliskHp - damage;
                Console.WriteLine($"{member} attacked the baselisk with a sword for {damage} damage");
            }

            void DaggerHit(string member)
            {
                damage = random.Next(1, 4);
                baseliskHp = baseliskHp - damage;
                Console.WriteLine($"{member} attacked the baselisk with a dagger for {damage} damage");
            }

            //Attack
            for ( ; ; )
            {
                if (baseliskHp > 0)
                {
                    if (membersHP[0] + membersHP[1] + membersHP[2] + membersHP[3] > 0)
                    {

                        if (membersHP[0] > 0)
                            SwordHit(members[0]);
                        if (membersHP[1] > 0)
                            DaggerHit(members[1]);
                        if (membersHP[2] > 0)
                            DaggerHit(members[2]);
                        if (membersHP[3] > 0)
                            DaggerHit(members[3]);

                        Console.WriteLine("");
                        if (baseliskHp > 0)
                            Console.WriteLine($"Baselisk has {baseliskHp} HP");
                        else
                            Console.WriteLine($"It's dead now tbh :D");
                        Console.WriteLine("");
                        if (baseliskHp > 0)
                            baseliskAttack();
                    }
                }
            }
            
            void baseliskAttack()
            {
                int memberAtacked = random.Next(membersHP.Count);
                membersHP[memberAtacked] = 0;

                Console.WriteLine($"The basilisk turns {members[memberAtacked]} to stone");
                Console.WriteLine("");
                if (membersHP[0] + membersHP[1] + membersHP[2] + membersHP[3] == 0)
                    Console.WriteLine($"Everyone turned to stone and the basilisk survived with {baseliskHp} HP");
            }
        
        }
    }
}