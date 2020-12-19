using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day17
    {
        public static int Part1(string[] input = null)
        {
            //input = new[]
            //{
            //    ".#.",
            //    "..#",
            //    "###"
            //};
            input ??= Input.ReadAllLines(nameof(Day17));

            var cubes = new Dictionary<(int x, int y, int z), bool>();
            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '#')
                    {
                        cubes.Add((x, y, 0), true);
                    }
                }
            }

            for (var i = 0; i < 6; i++)
            {
                cubes = SimulateCycle(cubes) as Dictionary<(int x, int y, int z), bool>;
            }

            return cubes?.Keys.Count ?? 0;
        }

        public static int Part2(string[] input = null)
        {
            //input = new[]
            //{
            //    ".#.",
            //    "..#",
            //    "###"
            //};
            input ??= Input.ReadAllLines(nameof(Day17));

            var cubes = new Dictionary<(int x, int y, int z, int w), bool>();
            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '#')
                    {
                        cubes.Add((x, y, 0, 0), true);
                    }
                }
            }

            for (var i = 0; i < 6; i++)
            {
                cubes = SimulateCycle4D(cubes) as Dictionary<(int x, int y, int z, int w), bool>;
            }

            return cubes?.Keys.Count ?? 0;
        }

        private static void Print(IDictionary<(int x, int y, int z), bool> cubes)
        {
            var xMin = cubes.Keys.Min(k => k.x);
            var xMax = cubes.Keys.Max(k => k.x);
            var yMin = cubes.Keys.Min(k => k.y);
            var yMax = cubes.Keys.Max(k => k.y);
            var zMin = cubes.Keys.Min(k => k.z);
            var zMax = cubes.Keys.Max(k => k.z);

            for (var z = zMin; z <= zMax; z++)
            {
                Console.WriteLine($"z={z}");
                for (var y = yMin; y <= yMax; y++)
                {
                    for (var x = xMin; x <= xMax; x++)
                    {
                        var curr = (x, y, z);
                        Console.Write(cubes.ContainsKey(curr) ? '#' : '.');
                    }
                    Console.WriteLine();
                }
            }
        }

        private static void Print4D(IDictionary<(int x, int y, int z, int w), bool> cubes)
        {
            var xMin = cubes.Keys.Min(k => k.x);
            var xMax = cubes.Keys.Max(k => k.x);
            var yMin = cubes.Keys.Min(k => k.y);
            var yMax = cubes.Keys.Max(k => k.y);
            var zMin = cubes.Keys.Min(k => k.z);
            var zMax = cubes.Keys.Max(k => k.z);
            var wMin = cubes.Keys.Min(k => k.w);
            var wMax = cubes.Keys.Max(k => k.w);
            for (var w = wMin; w <= wMax; w++)
            {
                for (var z = zMin; z <= zMax; z++)
                {
                    Console.WriteLine($"z={z}, w={w}");
                    for (var y = yMin; y <= yMax; y++)
                    {
                        for (var x = xMin; x <= xMax; x++)
                        {
                            var curr = (x, y, z, w);
                            Console.Write(cubes.ContainsKey(curr) ? '#' : '.');
                        }
                        Console.WriteLine();
                    }
                }
            }
        }

        private static IDictionary<(int x, int y, int z), bool> SimulateCycle(
            IDictionary<(int x, int y, int z), bool> cubes)
        {
            var xMin = cubes.Keys.Min(k => k.x) - 1;
            var xMax = cubes.Keys.Max(k => k.x) + 1;
            var yMin = cubes.Keys.Min(k => k.y) - 1;
            var yMax = cubes.Keys.Max(k => k.y) + 1;
            var zMin = cubes.Keys.Min(k => k.z) - 1;
            var zMax = cubes.Keys.Max(k => k.z) + 1;

            var activeCubes = new Dictionary<(int x, int y, int z), bool>();
            for (var z = zMin; z <= zMax; z++)
            {
                for (var y = yMin; y <= yMax; y++)
                {
                    for (var x = xMin; x <= xMax; x++)
                    {
                        var curr = (x, y, z);
                        var activeNeighbors = CountActiveNeighbors(curr, cubes);
                        if (cubes.ContainsKey(curr) && activeNeighbors >= 2 && activeNeighbors <= 3)
                        {
                            activeCubes.Add(curr, true);
                        }
                        else if (!cubes.ContainsKey(curr) && activeNeighbors == 3)
                        {
                            activeCubes.Add(curr, true);
                        }
                    }
                }
            }

            return activeCubes;
        }

        private static IDictionary<(int x, int y, int z, int w), bool> SimulateCycle4D(
            IDictionary<(int x, int y, int z, int w), bool> cubes)
        {
            var xMin = cubes.Keys.Min(k => k.x) - 1;
            var xMax = cubes.Keys.Max(k => k.x) + 1;
            var yMin = cubes.Keys.Min(k => k.y) - 1;
            var yMax = cubes.Keys.Max(k => k.y) + 1;
            var zMin = cubes.Keys.Min(k => k.z) - 1;
            var zMax = cubes.Keys.Max(k => k.z) + 1;
            var wMin = cubes.Keys.Min(k => k.w) - 1;
            var wMax = cubes.Keys.Max(k => k.w) + 1;

            var activeCubes = new Dictionary<(int x, int y, int z, int w), bool>();
            for (var w = wMin; w <= wMax; w++)
            {
                for (var z = zMin; z <= zMax; z++)
                {
                    for (var y = yMin; y <= yMax; y++)
                    {
                        for (var x = xMin; x <= xMax; x++)
                        {
                            var curr = (x, y, z, w);
                            var activeNeighbors = CountActiveNeighbors4D(curr, cubes);
                            if (cubes.ContainsKey(curr) && activeNeighbors >= 2 && activeNeighbors <= 3)
                            {
                                activeCubes.Add(curr, true);
                            }
                            else if (!cubes.ContainsKey(curr) && activeNeighbors == 3)
                            {
                                activeCubes.Add(curr, true);
                            }
                        }
                    }
                }
            }

            return activeCubes;
        }

        private static int CountActiveNeighbors((int x, int y, int z) pos, IDictionary<(int x, int y, int z), bool> cubes)
        {
            var numActive = 0;
            for (var z = pos.z - 1; z <= pos.z + 1; z++)
            {
                for (var y = pos.y - 1; y <= pos.y + 1; y++)
                {
                    for (var x = pos.x - 1; x <= pos.x + 1; x++)
                    {
                        var curr = (x, y, z);
                        if (curr == pos)
                        {
                            continue;
                        }

                        if (cubes.ContainsKey(curr) && cubes[curr])
                        {
                            numActive++;
                        }
                    }
                }
            }
            return numActive;
        }

        private static int CountActiveNeighbors4D((int x, int y, int z, int w) pos, IDictionary<(int x, int y, int z, int w), bool> cubes)
        {
            var numActive = 0;
            for (var w = pos.w - 1; w <= pos.w + 1; w++)
            {
                for (var z = pos.z - 1; z <= pos.z + 1; z++)
                {
                    for (var y = pos.y - 1; y <= pos.y + 1; y++)
                    {
                        for (var x = pos.x - 1; x <= pos.x + 1; x++)
                        {
                            var curr = (x, y, z, w);
                            if (curr == pos)
                            {
                                continue;
                            }

                            if (cubes.ContainsKey(curr) && cubes[curr])
                            {
                                numActive++;
                            }
                        }
                    }
                }
            }
            return numActive;
        }
    }
}
