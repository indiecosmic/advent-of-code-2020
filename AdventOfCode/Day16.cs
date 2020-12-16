using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day16
    {
        public static void Part1()
        {
            var input = Input.ReadAllLines(nameof(Day16));

            //var input = new[]
            //{
            //    "class: 1-3 or 5-7",
            //    "row: 6-11 or 33-44",
            //    "seat: 13-40 or 45-50",
            //    "",
            //    "your ticket:",
            //    "7,1,14",
            //    "",
            //    "nearby tickets:",
            //    "7,3,47",
            //    "40,4,50",
            //    "55,2,20",
            //    "38,6,12"
            //};

            var validationRules = new List<ValidationRule>();
            var invalidFields = new List<int>();
            var part = 0;
            foreach (var line in input)
            {
                if (line == "nearby tickets:")
                    continue;
                if (string.IsNullOrWhiteSpace(line)) {
                    part++;
                    continue;
                }
                if (part == 0) {
                    var rule = ValidationRule.Create(line);
                    validationRules.Add(rule);
                }
                if (part == 2)
                {
                    var ticket = line.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
                    foreach (var field in ticket)
                    {
                        if (!validationRules.Any(r => r.IsValid(field)))
                            invalidFields.Add(field);
                    }
                }
            } 

            Console.WriteLine(invalidFields.Sum());
        }

        public class ValidationRule
        {
            private readonly int _min1;
            private readonly int _max1;
            private readonly int _min2;
            private readonly int _max2;

            private ValidationRule(int min1, int max1, int min2, int max2)
            {
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
                    int.Parse(match.Groups["min1"].Value),
                    int.Parse(match.Groups["max1"].Value),
                    int.Parse(match.Groups["min2"].Value),
                    int.Parse(match.Groups["max2"].Value)
                    );
            }
        }
    }
}
