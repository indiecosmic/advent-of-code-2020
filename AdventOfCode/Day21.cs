using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day21
    {
        public static int Part1(string[] input = null)
        {
            input ??= Input.ReadAllLines(nameof(Day21));
            var foodList = CreateFoodList(input);

            var occurrences = new Dictionary<string, int>();
            var allergens = new Dictionary<string, HashSet<string>>();
            foreach (var food in foodList)
            {
                foreach (var allergen in food.allergens)
                {
                    if (!allergens.ContainsKey(allergen))
                        allergens.Add(allergen, new HashSet<string>(food.ingredients));
                    else
                        allergens[allergen] = new HashSet<string>(allergens[allergen].Intersect(new HashSet<string>(food.ingredients)));
                }

                foreach (var ingredient in food.ingredients)
                {
                    if (!occurrences.ContainsKey(ingredient))
                        occurrences.Add(ingredient, 1);
                    else
                        occurrences[ingredient] += 1;
                }
            }

            var identifiedAllergens = new HashSet<string>();
            var ingredientsWithAllergens = new HashSet<string>();
            var ingredientAllergenPair = new HashSet<(string, string)>();
            while (true)
            {
                var ambiguous = false;
                foreach (var allergen in allergens.Keys.Where(k => !identifiedAllergens.Contains(k)))
                {
                    if (allergens[allergen].Count == 1)
                    {
                        var ingredient = allergens[allergen].First();
                        ingredientsWithAllergens.Add(ingredient);
                        EliminateIngedient(ingredient, allergens);
                        identifiedAllergens.Add(allergen);
                        ingredientAllergenPair.Add((ingredient, allergen));
                    }
                    else
                        ambiguous = true;
                }

                if (!ambiguous)
                    break;
            }

            return occurrences.Keys
                .Where(k => !ingredientsWithAllergens.Contains(k))
                .Select(k => occurrences[k]).Sum();
        }

        public static string Part2(string[] input = null)
        {
            input ??= Input.ReadAllLines(nameof(Day21));
            var foodList = CreateFoodList(input);

            var occurrences = new Dictionary<string, int>();
            var allergens = new Dictionary<string, HashSet<string>>();
            foreach (var food in foodList)
            {
                foreach (var allergen in food.allergens)
                {
                    if (!allergens.ContainsKey(allergen))
                        allergens.Add(allergen, new HashSet<string>(food.ingredients));
                    else
                        allergens[allergen] = new HashSet<string>(allergens[allergen].Intersect(new HashSet<string>(food.ingredients)));
                }

                foreach (var ingredient in food.ingredients)
                {
                    if (!occurrences.ContainsKey(ingredient))
                        occurrences.Add(ingredient, 1);
                    else
                        occurrences[ingredient] += 1;
                }
            }

            var identifiedAllergens = new HashSet<string>();
            var ingredientsWithAllergens = new HashSet<string>();
            var ingredientAllergenPair = new HashSet<(string, string)>();
            while (true)
            {
                var ambiguous = false;
                foreach (var allergen in allergens.Keys.Where(k => !identifiedAllergens.Contains(k)))
                {
                    if (allergens[allergen].Count == 1)
                    {
                        var ingredient = allergens[allergen].First();
                        ingredientsWithAllergens.Add(ingredient);
                        EliminateIngedient(ingredient, allergens);
                        identifiedAllergens.Add(allergen);
                        ingredientAllergenPair.Add((ingredient, allergen));
                    }
                    else
                        ambiguous = true;
                }

                if (!ambiguous)
                    break;
            }

            return string.Join(',', ingredientAllergenPair.OrderBy(i => i.Item2).Select(i => i.Item1));
        }

        private static void EliminateIngedient(string ingredient, Dictionary<string, HashSet<string>> allergens)
        {
            foreach (string allergen in allergens.Keys)
                allergens[allergen].Remove(ingredient);
        }

        public static (string[] ingredients, string[] allergens)[] CreateFoodList(string[] input)
        {
            var foodList = new List<(string[], string[])>();
            foreach (var line in input)
            {
                var parts = line.Split("(", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var ingredients = parts[0].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var allergens = parts[1]
                    .Replace("contains", "")
                    .Replace(")", "")
                    .Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foodList.Add((ingredients, allergens));
            }

            return foodList.ToArray();
        }
    }
}
