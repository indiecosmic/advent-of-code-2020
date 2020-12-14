using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day10
    {
        public static void Run()
        {
            var input = Input.ReadAllLines(nameof(Day10));
            var adapters = input.Select(int.Parse).ToList();

            //            var adapters = new List<int>(new int[]
            //            {
            //28
            //,33
            //,18
            //,42
            //,31
            //,14
            //,46
            //,20
            //,48
            //,47
            //,24
            //,23
            //,49
            //,45
            //,19
            //,38
            //,39
            //,11
            //,1
            //,32
            //,25
            //,35
            //,8
            //,17
            //,7
            //,9
            //,4
            //,2
            //,34
            //,10
            //,3
            //            });
            const int start = 0;
            var goal = adapters.Max() + 3;
            adapters.Add(goal);
            List<int> used = new List<int>();
            var diffs = (0, 0);
            var current = start;
            while (used.Count < adapters.Count) {
                if (current == goal)
                    break;
                var compatible = FindCompatible(adapters, current).Min();
                used.Add(compatible);
                if (compatible - current == 1)
                    diffs.Item1++;
                if (compatible - current == 3)
                    diffs.Item2++;
                current = compatible;
            }
            var result = diffs.Item1 * diffs.Item2;

            adapters.Add(start);

            adapters.Sort();
            var connections = new long[adapters.Count];
            connections[0] = 1;
            for (var i = 1; i < connections.Length; i++)
            {
                connections[i] = 0;
                for (var j = i - 1; j >= 0; j--)
                {
                    if (adapters[i] - adapters[j] <= 3)
                        connections[i] += connections[j];
                    else break;
                }
            }
        }

        public static int[] FindCompatible(IEnumerable<int> adapters, int target) => adapters.Where(a => CanConnect(a, target)).ToArray();

        public static bool CanConnect(int source, int dest) => dest < source && source - dest <= 3;
    }
}
