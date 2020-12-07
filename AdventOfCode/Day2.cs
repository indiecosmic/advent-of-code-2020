using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day2
    {
        public static async Task RunAsync()
        {
            var input = await File.ReadAllLinesAsync("day2_input.txt");
            var count = 0;
            foreach (var text in input)
            {
                if (Test(text))
                {
                    count++;
                }
            }

            Console.WriteLine(count);
        }

        protected static bool Test(string input)
        {
            var parts = input.Split(":", StringSplitOptions.RemoveEmptyEntries);

            var policyPart = parts.First();
            var range = policyPart
                .Split(":", StringSplitOptions.RemoveEmptyEntries).First()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries).First()
                .Split("-", StringSplitOptions.RemoveEmptyEntries);
            var min = int.Parse(range.First());
            var max = int.Parse(range.Last());
            var character = policyPart.Split(" ", StringSplitOptions.RemoveEmptyEntries).Last();

            var reg = $"[{character}]";

            var passwordPart = parts.Last().Trim();
            var matches = Regex.Matches(passwordPart, reg).Count;

            return /*(matches >= min && matches <= max) &&*/ IsValid(passwordPart, character, min, max);
        }

        public static bool IsValid(string password, string pattern, int pos1, int pos2)
        {
            var firstChar = password.Substring(pos1 - 1, 1);
            var secondChar = password.Substring(pos2 - 1, 1);
            if (firstChar == pattern && secondChar != pattern)
                return true;
            if (secondChar == pattern && firstChar != pattern)
                return true;
            return false;
        }

    }
}
