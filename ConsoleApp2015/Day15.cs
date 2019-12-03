using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace ConsoleApp2015
{
    class Day15 : IDay
    {
        private string input = @"Frosting: capacity 4, durability -2, flavor 0, texture 0, calories 5
Candy: capacity 0, durability 5, flavor -1, texture 0, calories 8
Butterscotch: capacity -1, durability 0, flavor 5, texture 0, calories 6
Sugar: capacity 0, durability 0, flavor -2, texture 2, calories 1";

        class Ingredient
        {
            public string Name;
            public int Capacity;
            public int Durability;
            public int Flavor;
            public int Texture;
            public int Calories;
        }


        public int Part1()
        {
//            input = @"Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
//Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3";

            var ingredients = ParseInput().ToArray();
            var recipes = DivideInParts(100, ingredients.Count());


            var bestresult = 0;
            int[] bestrecipe = new[] {0};

            foreach (var recipe in recipes)
            {
                var capacity = 0;
                var durability = 0;
                var flavor = 0;
                var texture = 0;
                for (int ing = 0; ing < recipe.Length; ing++)
                {
                    var ingredient = ingredients[ing];
                    var multiplier = recipe[ing];
                    capacity += ingredient.Capacity * multiplier;
                    durability += ingredient.Durability * multiplier;
                    flavor += ingredient.Flavor * multiplier;
                    texture += ingredient.Texture * multiplier;
                }

                var recipiresult = Math.Max(0, capacity) * Math.Max(0, durability) * Math.Max(0, flavor) *
                                   Math.Max(0, texture);

                if (recipiresult > bestresult)
                {
                    bestresult = recipiresult;
                    bestrecipe = recipe;
                }

            }

            Console.WriteLine(String.Join(',', bestrecipe));
            return bestresult;
        }

        public int Part2()
        {
            //            input = @"Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
            //Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3";

            var ingredients = ParseInput().ToArray();
            var recipes = DivideInParts(100, ingredients.Count());


            var bestresult = 0;
            int[] bestrecipe = new[] {0};

            foreach (var recipe in recipes)
            {
                var capacity = 0;
                var durability = 0;
                var flavor = 0;
                var texture = 0;
                var calories = 0;
                for (int ing = 0; ing < recipe.Length; ing++)
                {
                    var ingredient = ingredients[ing];
                    var multiplier = recipe[ing];
                    capacity += ingredient.Capacity * multiplier;
                    durability += ingredient.Durability * multiplier;
                    flavor += ingredient.Flavor * multiplier;
                    texture += ingredient.Texture * multiplier;
                    calories += ingredient.Calories * multiplier;
                }

                var recipiresult = Math.Max(0, capacity) * Math.Max(0, durability) * Math.Max(0, flavor) *
                                   Math.Max(0, texture);

                if (recipiresult > bestresult && calories == 500)
                {
                    bestresult = recipiresult;
                    bestrecipe = recipe;
                }

            }

            Console.WriteLine(String.Join(',', bestrecipe));
            return bestresult;
        }


        private IEnumerable<Ingredient> ParseInput()
        {
            var result = new List<Ingredient>();
            foreach (var ingtxt in input.Split('\n'))
            {
                var sub = ingtxt.Split(':')[1].Split(',').Select(s => int.Parse(s.Split(' ')[2].Trim())).ToArray();
                var ingredient = new Ingredient()
                {
                    Name = ingtxt.Split(':')[0],
                    Capacity = sub[0],
                    Durability = sub[1],
                    Flavor = sub[2],
                    Texture = sub[3],
                    Calories = sub[4]
                };
                result.Add(ingredient);
            }
            return result;
        }

        private IEnumerable<int[]> DivideInParts(int number, int parts)
        {
            if (parts == 1)
                return new List<int[]>() {new[] {number}};

            var result = new List<int[]>();

            int max = number - parts - 1;
            for (int i = 1; i <= max; i++)
            {
                var rest = DivideInParts(number - i, parts - 1);
                foreach (var tail in rest)
                {
                    var part = new int[parts];
                    part[0] = i;
                    tail.CopyTo(part, 1);
                    result.Add(part);
                }
            }
            return result;
        }
    }


}
