using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day13
    {
        public static void Run()
        {
            var input = Input.ReadAllLines(nameof(Day13));
            var arrival = int.Parse(input[0]);
            var busnames = input[1].Split(',', StringSplitOptions.RemoveEmptyEntries);
            var buses = busnames.Where(n => n != "x").Select(n => int.Parse(n)).ToArray();
            Part1(arrival, buses);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine(Part2(busnames));
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            //Console.WriteLine(Part2(new string[] { "17", "x", "13", "19" }));
            //Console.WriteLine(Part2(new string[] { "67", "7", "x", "59", "61" }));
            //Console.WriteLine(Part2(new string[] { "67", "x", "7", "59", "61" }));
        }

        private static long Part2(string[] schedule)
        {
            var buses = schedule.Where(n => n != "x").Select(long.Parse).ToArray();
            var offsets = new Dictionary<long, long>();
            for (var i = 0; i < schedule.Length; i++)
            {
                if (schedule[i] != "x")
                {
                    offsets[int.Parse(schedule[i])] = i;
                }
            }

            var t = 0L;
            var step = 1L;
            for (var i = 0; i < buses.Length; i++)
            {
                while (!buses.Take(i + 1).All(b => (t + offsets[b]) % b == 0))
                {
                    t += step;
                }
                step = Lcm(buses.Take(i+1).ToArray());
            }
            return t;
        }

        private static void Part1(int arrival, int[] buses)
        {
            var nearest = int.MaxValue;
            var nearestId = 0;
            foreach (var bus in buses)
            {
                var timestamp = bus;
                while (timestamp <= arrival)
                {
                    timestamp += bus;
                }
                if (timestamp - arrival < nearest)
                {
                    nearestId = bus;
                    nearest = timestamp - arrival;
                }
            }
            Console.WriteLine($"{nearestId}: {nearest} {nearestId * nearest}");
        }

        private static long Lcm(params long[] numbers)
        {
            return numbers.Aggregate(Lcm);
        }
        private static long Lcm(long a, long b)
        {
            return Math.Abs(a * b) / Gcd(a, b);
        }
        private static long Gcd(long a, long b)
        {
            return b == 0 ? a : Gcd(b, a % b);
        }
    }
}
