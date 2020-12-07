using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day6
    {
        public static void Run()
        {
            var input = File.ReadAllText("day6_input.txt");
            var groups = input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            var sum = 0;
            foreach (var group in groups)
            {
                var answers = group.Replace("\n", "");
                var unique = answers.Distinct().Count();
                sum += unique;
            }
            Console.WriteLine(sum);

            var total = 0;
            foreach (var group in groups)
            {
                var groupSum = 0;
                var answers = group.Replace("\n", "").Distinct();
                var count = group.Split("\n", StringSplitOptions.RemoveEmptyEntries).Length;
                foreach (var letter in answers)
                {
                    var x = group.Count(l => l == letter);
                    if (x == count)
                        groupSum += 1;
                }

                total += groupSum;
            }
            Console.WriteLine(total);
        }
    }
}
