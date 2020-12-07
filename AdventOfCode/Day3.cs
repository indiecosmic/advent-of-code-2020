using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day3
    {
        public static void RunAsync()
        {
            var input = File.ReadAllLines("day3_input.txt");
            var grid = new char[input[0].Length, input.Length];
            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    grid[x, y] = input[y][x];
                }
            }

            var strats = new List<(int, int)>
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2)
            };
            var counts = new List<int>();
            foreach (var strat in strats) {
                var pos = (x:0, y:0);
                var count = 0;
                while (pos.y < input.Length)
                {
                    if (grid[pos.x, pos.y] == '#') count++;
                    pos = (x: pos.x + strat.Item1, y: pos.y + strat.Item2);
                    if (pos.x >= input[0].Length)
                        pos.x = pos.x - input[0].Length;
                }
                counts.Add(count);
                Console.WriteLine(count);
            }
            
        }
    }
}
