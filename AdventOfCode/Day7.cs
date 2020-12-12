using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Day7
    {
        public static void Run()
        {
            var bagdefs = Input.ReadAllLines(nameof(Day7));
            //var bagdefs = new[]
            //{
            //    "light red bags contain 1 bright white bag, 2 muted yellow bags.",
            //    "dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
            //    "bright white bags contain 1 shiny gold bag.",
            //    "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
            //    "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
            //    "dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
            //    "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
            //    "faded blue bags contain no other bags.", "dotted black bags contain no other bags."
            //};

            //var bagdefs = new[]
            //{
            //    "shiny gold bags contain 2 dark red bags.", "dark red bags contain 2 dark orange bags.",
            //    "dark orange bags contain 2 dark yellow bags.", "dark yellow bags contain 2 dark green bags.",
            //    "dark green bags contain 2 dark blue bags.", "dark blue bags contain 2 dark violet bags.",
            //    "dark violet bags contain no other bags."
            //};

            var bags = bagdefs.Select(Bag.Parse).ToList();

            var bagDict = bags.ToDictionary(bag => bag.Name);
            foreach (var name in bagDict.Keys)
            {
                var bag = bagDict[name];
                var children = new List<Bag>();
                foreach (var child in bag.Contains.Keys)
                {
                    children.Add(bagDict[child]);
                    bagDict[child].Parents.Add(bag);
                }

                bag.Children = children.ToArray();
            }

            var roots = new List<string>();
            var start = bagDict["shiny gold"];
            Traverse(start, roots);

            var count = start.GetNumberOfBags();
        }

        public static void Traverse(Bag bag, List<string> roots)
        {
            if (!roots.Contains(bag.Name))
                roots.Add(bag.Name);

            foreach (var parent in bag.Parents)
            {
                Traverse(parent, roots);
            }
        }

        public class Bag
        {
            public string Name { get; set; }
            public IDictionary<string, int> Contains { get; set; }
            public IList<Bag> Parents { get; } = new List<Bag>();
            public Bag[] Children { get; set; }

            public int GetNumberOfBags()
            {
                if (Children.Length == 0)
                    return 1;

                var childCount = 0;
                foreach (var child in Children)
                    childCount += (Contains[child.Name] * child.GetNumberOfBags());
                return 1 + childCount;
            }

            public static Bag Parse(string def)
            {
                var parts = def
                    .Replace("bags","")
                    .Replace("bag","")
                    .Replace(".", "")
                    .Split("contain", StringSplitOptions.RemoveEmptyEntries);
                var bagName = parts[0].Trim();

                var contents = parts[1].Split(",", StringSplitOptions.RemoveEmptyEntries);
                var contains = new Dictionary<string, int>();
                foreach (var child in contents)
                {
                    var bag = child.Trim();
                    if (bag == "no other")
                        continue;
                    var match = Regex.Match(child, @"(?<count>\d+)(?<name>[\w ]+)");
                    var count = int.Parse(match.Groups["count"].Value.Trim());
                    var name = match.Groups["name"].Value.Trim();
                    contains.Add(name, count);
                }

                return new Bag
                {
                    Name = bagName,
                    Contains = contains
                };
            }
        }
    }
}
