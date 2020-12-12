using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Day12
    {
        public static void Run()
        {
            var input = Input.ReadAllLines(nameof(Day12));

            //var input = new string[] {
            //    "F10",
            //    "N3",
            //    "F7",
            //    "R90",
            //    "F11"
            //};

            var instructions = input.Select(s => (action: s.Substring(0, 1), value: int.Parse(s[1..]))).ToArray();
            Console.WriteLine(Part1(instructions));
            Console.WriteLine(Part2(instructions));
        }

        private static int Part2((string action, int value)[] instructions)
        {
            var pos = (x: 0, y: 0);
            var dir = (dx: 10, dy: 1);

            foreach (var (action, value) in instructions)
            {
                switch (action)
                {
                    case "F":
                        pos = (pos.x + (dir.dx * value), pos.y + (dir.dy * value));
                        break;
                    case "N":
                        dir.dy += value;
                        break;
                    case "S":
                        dir.dy -= value;
                        break;
                    case "W":
                        dir.dx -= value;
                        break;
                    case "E":
                        dir.dx += value;
                        break;
                    case "R":
                        dir = TurnRight(dir, value / 90);
                        break;
                    case "L":
                        dir = TurnLeft(dir, value / 90);
                        break;
                }
            }

            return Math.Abs(pos.x) + Math.Abs(pos.y);
        }

        private static int Part1((string action, int value)[] instructions)
        {
            var pos = (x: 0, y: 0);
            var dir = (dx: 1, dy: 0);

            foreach (var (action, value) in instructions)
            {
                switch (action)
                {
                    case "F":
                        pos = (pos.x + (dir.dx * value), pos.y + (dir.dy * value));
                        break;
                    case "N":
                        pos.y += value;
                        break;
                    case "S":
                        pos.y -= value;
                        break;
                    case "W":
                        pos.x -= value;
                        break;
                    case "E":
                        pos.x += value;
                        break;
                    case "R":
                        dir = TurnRight(dir, value / 90);
                        break;
                    case "L":
                        dir = TurnLeft(dir, value / 90);
                        break;
                }
            }

            return Math.Abs(pos.x) + Math.Abs(pos.y);
        }

        public static (int,int) TurnLeft((int dx, int dy) dir, int times)
        {
            for (var i = 0; i < times; i++)
            {
                dir = (-dir.dy, dir.dx);
            }

            return dir;
        }
        public static (int,int) TurnRight((int dx, int dy) dir, int times)
        {
            for (var i = 0; i < times; i++)
            {
                dir = (dir.dy, -dir.dx);
            }

            return dir;
        }
    }
}
