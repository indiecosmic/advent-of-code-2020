using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Day16
    {
        public static (List<ValidationRule> validationRules, int[] yourTicket, List<int[]> nearbyTickets) ParseInput(string[] input)
        {
            var validationRules = new List<ValidationRule>();
            var nearbyTickets = new List<int[]>();
            int[] yourTicket = null;
            var part = 0;
            foreach (var line in input)
            {
                if (line == "nearby tickets:" || line == "your ticket:")
                    continue;
                if (string.IsNullOrWhiteSpace(line))
                {
                    part++;
                    continue;
                }
                if (part == 0)
                {
                    var rule = ValidationRule.Create(line);
                    validationRules.Add(rule);
                }
                if (part == 1)
                {
                    yourTicket = line.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                }
                if (part == 2)
                {
                    var ticket = line.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                    nearbyTickets.Add(ticket);
                }
            }

            return (validationRules, yourTicket, nearbyTickets);
        }

        public static int Part1(string[] input = null)
        {
            input ??= Input.ReadAllLines(nameof(Day16));
            var (validationRules,_,nearbyTickets) = ParseInput(input);

            var invalidFields = new List<int>();
            foreach (var ticket in nearbyTickets)
            {
                foreach (var field in ticket)
                {
                    if (!validationRules.Any(r => r.IsValid(field)))
                        invalidFields.Add(field);
                }
            }
            return invalidFields.Sum();
        }

        public static long Part2(string[] input = null)
        {
            input ??= Input.ReadAllLines(nameof(Day16));
            var fields = CalculateFields(input);
            var result = fields
                .Where(f => f.name.StartsWith("departure"))
                .Select(f => f.value)
                .Aggregate(1L, (acc, f) => acc * f);
            return result;
        }

        public static (string name, int value)[] CalculateFields(string[] input)
        {
            var (validationRules, yourTicket, nearbyTickets) = ParseInput(input);
            var validTickets = new List<int[]>();
            foreach (var ticket in nearbyTickets)
            {
                var isValid = true;
                foreach (var field in ticket)
                {
                    if (!validationRules.Any(r => r.IsValid(field)))
                        isValid = false;
                }
                if (isValid)
                    validTickets.Add(ticket);
            }
            validTickets.Add(yourTicket);

            var fieldNames = new string[validTickets[0].Length];
            while (validationRules.Count > 0) {
                for (var i = 0; i < fieldNames.Length; i++)
                {
                    if (!string.IsNullOrEmpty(fieldNames[i]))
                        continue;

                    var possibleRules = validationRules.ToList();
                    foreach (var ticket in validTickets)
                    {
                        possibleRules = possibleRules.Where(r => r.IsValid(ticket[i])).ToList();
                    }

                    if (possibleRules.Count == 1)
                    {
                        var rule = possibleRules.First();
                        fieldNames[i] = rule.Name;
                        validationRules.Remove(rule);
                    }
                }
            }

            return fieldNames.Select((name, i) => (name, yourTicket[i])).ToArray();
        }

        public class ValidationRule
        {
            public string Name { get; }
            private readonly int _min1;
            private readonly int _max1;
            private readonly int _min2;
            private readonly int _max2;

            private ValidationRule(string name, int min1, int max1, int min2, int max2)
            {
                Name = name;
                _min1 = min1;
                _max1 = max1;
                _min2 = min2;
                _max2 = max2;
            }

            public bool IsValid(int value)
            {
                return (value >= _min1 && value <= _max1) ||
                       (value >= _min2 && value <= _max2);
            }

            public static ValidationRule Create(string s)
            {
                var match = Regex.Match(s,
                    @"(?<name>[^:]+): (?<min1>\d+)-(?<max1>\d+) or (?<min2>\d+)-(?<max2>\d+)");
                return new ValidationRule(
                    match.Groups["name"].Value,
                    int.Parse(match.Groups["min1"].Value),
                    int.Parse(match.Groups["max1"].Value),
                    int.Parse(match.Groups["min2"].Value),
                    int.Parse(match.Groups["max2"].Value)
                    );
            }
        }
    }
}
