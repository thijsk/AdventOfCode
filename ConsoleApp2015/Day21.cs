using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2015
{
    class Day21 : IDay
    {
        private const string input = @"Hit Points: 104
Damage: 8
Armor: 1";


        private class Player
        {
            public int Hitpoints;
            public int Damage;
            public int Armor;
        }


        private class Item
        {
            public Item(string name, int cost, int damage, int armor)
            {
                Name = name;
                Cost = cost;
                Damage = damage;
                Armor = armor;
            }

            public string Name;
            public int Cost;
            public int Damage;
            public int Armor;
        }

        public long Part1()
        {
            List<Item> weapons, armors, rings;
            Shop(out weapons, out armors, out rings);

            var minCost = int.MaxValue;

            foreach (var weapon in weapons)
                foreach (var armor in armors)
                    foreach (var ring1 in rings)
                        foreach (var ring2 in rings.Where(r => r != ring1))
                        {
                            var hero = new Player();
                            hero.Hitpoints = 100;
                            hero.Armor = weapon.Armor + armor.Armor + ring1.Armor + ring2.Armor;
                            hero.Damage = weapon.Damage + armor.Damage + ring1.Damage + ring2.Damage;
                            var cost = weapon.Cost + armor.Cost + ring1.Cost + ring2.Cost;

                            var boss = new Player();
                            var bossstats = input.Split(Environment.NewLine);
                            boss.Hitpoints = int.Parse(bossstats[0].Split(':')[1].Trim());
                            boss.Damage = int.Parse(bossstats[1].Split(':')[1].Trim());
                            boss.Armor = int.Parse(bossstats[2].Split(':')[1].Trim());

                            while (hero.Hitpoints > 0 && boss.Hitpoints > 0)
                            {
                                boss.Hitpoints -= Math.Max(hero.Damage - boss.Armor, 1);
                                hero.Hitpoints -= Math.Max(boss.Damage - hero.Armor, 1);
                            }

                            if (boss.Hitpoints <= 0)
                            {
                                minCost = Math.Min(minCost, cost);
                            }

                        }

            return minCost;
        }

        private static void Shop(out List<Item> weapons, out List<Item> armors, out List<Item> rings)
        {
            weapons = new List<Item>();
            weapons.Add(new Item("Dagger", 8, 4, 0));
            weapons.Add(new Item("Shortsword", 10, 5, 0));
            weapons.Add(new Item("Warhammer", 25, 6, 0));
            weapons.Add(new Item("Longsword", 40, 7, 0));
            weapons.Add(new Item("Greataxe", 74, 8, 0));

            armors = new List<Item>();
            armors.Add(new Item("Null", 0, 0, 0));
            armors.Add(new Item("Leather", 13, 0, 1));
            armors.Add(new Item("Chainmail", 31, 0, 2));
            armors.Add(new Item("Splintmail", 53, 0, 3));
            armors.Add(new Item("Bandedmail", 75, 0, 4));
            armors.Add(new Item("Platemail", 102, 0, 5));

            rings = new List<Item>();
            rings.Add(new Item("Null1", 0, 0, 0));
            rings.Add(new Item("Null2", 0, 0, 0));
            rings.Add(new Item("Damage + 1", 25, 1, 0));
            rings.Add(new Item("Damage + 2", 50, 2, 0));
            rings.Add(new Item("Damage + 3", 100, 3, 0));
            rings.Add(new Item("Defense + 1", 20, 0, 1));
            rings.Add(new Item("Defense + 2", 40, 0, 2));
            rings.Add(new Item("Defense + 3", 80, 0, 3));
        }

        public long Part2()
        {
            List<Item> weapons, armors, rings;
            Shop(out weapons, out armors, out rings);

            var maxCost = int.MinValue;

            foreach (var weapon in weapons)
                foreach (var armor in armors)
                    foreach (var ring1 in rings)
                        foreach (var ring2 in rings.Where(r => r != ring1))
                        {
                            var hero = new Player();
                            hero.Hitpoints = 100;
                            hero.Armor = weapon.Armor + armor.Armor + ring1.Armor + ring2.Armor;
                            hero.Damage = weapon.Damage + armor.Damage + ring1.Damage + ring2.Damage;
                            var cost = weapon.Cost + armor.Cost + ring1.Cost + ring2.Cost;

                            var boss = new Player();
                            var bossstats = input.Split(Environment.NewLine);
                            boss.Hitpoints = int.Parse(bossstats[0].Split(':')[1].Trim());
                            boss.Damage = int.Parse(bossstats[1].Split(':')[1].Trim());
                            boss.Armor = int.Parse(bossstats[2].Split(':')[1].Trim());

                            while (hero.Hitpoints > 0 && boss.Hitpoints > 0)
                            {
                                boss.Hitpoints -= Math.Max(hero.Damage - boss.Armor, 1);
                                hero.Hitpoints -= Math.Max(boss.Damage - hero.Armor, 1);
                            }

                            if (hero.Hitpoints <= 0 && boss.Hitpoints > 0 && cost > maxCost)
                            {
                                maxCost = cost;
                            }

                        }

            return maxCost;
        }
    }
}
