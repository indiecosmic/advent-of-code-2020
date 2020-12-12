using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Day4
    {
        public static void Run()
        {
            var input = File.ReadAllLines("day4_input.txt");
            var passports = new List<Passport>();
            var passport = new Passport();
            foreach (var line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    passports.Add(passport);
                    passport = new Passport();
                    continue;
                }

                var fields = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                foreach (var field in fields)
                {
                    var parts = field.Split(":", StringSplitOptions.RemoveEmptyEntries);
                    passport.Fields.Add(parts[0], parts[1]);
                }
            }
            passports.Add(passport);

            var validCount = passports.Count(p => p.IsValid());
            Console.WriteLine(validCount);
        }

        private class Passport
        {
            public static readonly string[] RequiredFields = new[]
            {
                "byr",
                "iyr",
                "eyr",
                "hgt",
                "hcl",
                "ecl",
                "pid"
                // "cid" (Country ID)"
            };

            public Dictionary<string, string> Fields = new Dictionary<string, string>();

            public bool IsValid()
            {
                if (!RequiredFields.All(field => Fields.ContainsKey(field)))
                    return false;

                if (!ValidateRange(Fields["byr"], 1920, 2002))
                    return false;
                if (!ValidateRange(Fields["iyr"], 2010, 2020))
                    return false;
                if (!ValidateRange(Fields["eyr"], 2020, 2030))
                    return false;
                var hgt = Regex.Match(Fields["hgt"], @"^(\d{2,3})(in|cm)$");
                if (!hgt.Success)
                    return false;
                if (hgt.Groups[2].Value == "in" && !ValidateRange(hgt.Groups[1].Value, 59, 76))
                    return false;
                if (hgt.Groups[2].Value == "cm" && !ValidateRange(hgt.Groups[1].Value, 150, 193))
                    return false;
                if (!Regex.IsMatch(Fields["hcl"], @"^#[\da-f]{6}$"))
                    return false;
                if (!Regex.IsMatch(Fields["ecl"], "^(amb|blu|brn|gry|grn|hzl|oth)$"))
                    return false;
                if (!Regex.IsMatch(Fields["pid"], @"^\d{9}$"))
                    return false;
                return true;
            }

            private static bool ValidateRange(string input, int min, int max)
            {
                if (!int.TryParse(input, out var value))
                    return false;
                if (value < min || value > max)
                    return false;
                return true;
            }
        }
    }
}
