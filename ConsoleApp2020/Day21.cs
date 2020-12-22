using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ConsoleApp2020
{
    class Day21 : IDay
    {
        public long Part1()
        {
            var input = ParseInput();
          
            var foundIngredients = GetAllergens(input);

            var otheringredients = input.SelectMany(f => f.Ingredients).Where(i => !foundIngredients.ContainsKey(i)).GroupBy(i => i).Select(group => new{Ingredient = group.Key, Count = group.Count()});

            return otheringredients.Sum(o => o.Count);
        }

        private static Dictionary<string, string> GetAllergens(List<Food> input)
        {
            var foundIngredients = new Dictionary<string, string>();

            var allAllergens = input.SelectMany(f => f.Allergens).Distinct();
            var foodsByAllergen = allAllergens.Select(a => (a, input.Where(i => i.Allergens.Contains(a))));

            var work = new Queue<string>(allAllergens);

            while (work.Count > 0)
            {

                var allergen = work.Dequeue();
                var foods = foodsByAllergen.Where(a => a.a == allergen).SelectMany(a => a.Item2);

                var commoningredients = foods.Select(f => f.Ingredients).IntersectMany(i => i)
                    .Where(i => !foundIngredients.ContainsKey(i)).ToList();

                if (commoningredients.Count() == 1)
                {
                    foundIngredients.Add(commoningredients.First(), allergen);
                }
                else
                {
                    work.Enqueue(allergen);
                }
            }

            return foundIngredients;
        }

        public long Part2()
        {
            var input = ParseInput();
            var foundIngredients = GetAllergens(input);

            var result1 = string.Join(',',foundIngredients.OrderBy(kv => kv.Value).Select(kv => kv.Key));
            Console.WriteLine(result1);
            var result2 = string.Join(',', foundIngredients.OrderBy(kv => kv.Value).Select(kv => kv.Value));
            Console.WriteLine(result2);

            return 0;
        }

        public List<Food> ParseInput()
        {
            return File.ReadAllLines($"Day21.txt").Select(l => new Food(l)).ToList();
        }
    }

    internal class Food
    {
        public List<string> Ingredients = new List<string>();
        public List<string> Allergens = new List<string>();


        public Food(string input)
        {
            var inputs = input.Split(" (contains ");
            var ingredients = inputs[0];
            Ingredients = ingredients.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
            if (inputs.Length > 1)
            {
                var allergens = inputs[1].TrimEnd(')');
                Allergens = allergens.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList();
            }
           
        }
    }
}