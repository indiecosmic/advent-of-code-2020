using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day15
    {
        public static void Run()
        {
            var startingNumbers = new[] { 1, 20, 11, 6, 12, 0 };
            var result = GetNthNumberSpoken(startingNumbers, 2020);
            Console.WriteLine(result);

            var time = System.Diagnostics.Stopwatch.StartNew();
            result = GetNthNumberSpoken(startingNumbers, 30000000);
            time.Stop();
            Console.WriteLine(result.ToString() +" "+ time.Elapsed);
        }

        public static int GetNthNumberSpoken(int[] startingNumbers, int n)
        {
            var spokenNumbers = new Dictionary<int, int[]>();
            for (var i = 0; i < startingNumbers.Length; i++)
            {
                spokenNumbers.Add(startingNumbers[i], new []{-1, i});
            }
            
            var lastNumber = startingNumbers[^1];
            for (var i = startingNumbers.Length; i < n; i++)
            {
                var numberToSpeak = spokenNumbers[lastNumber][0] == -1
                    ? 0
                    : spokenNumbers[lastNumber][^1] - spokenNumbers[lastNumber][^2];

                if (!spokenNumbers.ContainsKey(numberToSpeak))
                {
                    spokenNumbers.Add(numberToSpeak, new[] {-1, i });
                }
                else
                {
                    spokenNumbers[numberToSpeak][0] = spokenNumbers[numberToSpeak][1];
                    spokenNumbers[numberToSpeak][1] = i;
                }

                lastNumber = numberToSpeak;
            }

            return lastNumber;
        }
    }
}
