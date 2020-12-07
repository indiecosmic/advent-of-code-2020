using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day5
    {
        public static void Run()
        {
            var input = File.ReadAllLines("day5_input.txt");
            var max = 0;
            var seats = new List<(int,int,int)>();
            foreach (var line in input)
            {
                var seat = FindRow(line, 0, 0, 127, (0, 0));
                var seatId = seat.Item1 * 8 + seat.Item2;
                if (seatId > max)
                    max = seatId;
                seats.Add((seat.Item1, seat.Item2, seatId));
            }

            seats = seats.OrderBy(s => s.Item1).ThenBy(s => s.Item2).ToList();
            var minSeatId = seats.Min(s => s.Item3);
            var maxSeatId = seats.Max(s => s.Item3);
            var mySeat = -1;
            for (var i = minSeatId; i < maxSeatId; i++)
            {
                if (seats.All(s => s.Item3 != i))
                    mySeat = i;
            }
        }

        static (int,int) FindRow(string input, int pos, int min, int max, (int,int) seat)
        {
            if (pos >= input.Length)
                return seat;
            if (min == max)
                seat.Item1 = min;
            var c = input[pos];
            return c switch
            {
                'F' => FindRow(input, pos+1, min, (int)Math.Floor(max - (max - min) / 2f), seat),
                'B' => FindRow(input, pos+1, (int)Math.Ceiling(min + (max - min) / 2f), max, seat),
                _ => FindCol(input, pos, 0, 7, seat)
            };
        }

        static (int, int) FindCol(string input, int pos, int min, int max, (int, int) seat)
        {
            if (min == max)
                seat.Item2 = min;
            if (pos >= input.Length)
                return seat;
            var c = input[pos];
            return c switch
            {
                'L' => FindCol(input, pos + 1, min, (int)Math.Floor(max - (max - min) / 2f), seat),
                'R' => FindCol(input, pos + 1, (int)Math.Ceiling(min + (max - min) / 2f), max, seat),
                _ => seat
            };
        }
    }
}
