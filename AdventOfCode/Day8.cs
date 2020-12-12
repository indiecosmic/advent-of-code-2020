using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public static class Day8
    {
        public static void Run()
        {
            var input = Input.ReadAllLines(nameof(Day8));
            var instructions = new List<(string, int)>();
            foreach (var row in input)
            {
                var parts = row.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var instruction = parts[0].Trim();
                var value = parts[1].Replace("+", "").Trim();
                instructions.Add((instruction, int.Parse(value)));
            }

            var day1 = RunProgram(instructions);

            for (var i = 0; i < instructions.Count; i++)
            {
                if (instructions[i].Item1 == "nop" || instructions[i].Item1 == "jmp")
                {
                    var orig = instructions[i];
                    instructions[i] = (orig.Item1 == "nop"? "jmp":"nop", orig.Item2);
                    var result = RunProgram(instructions);
                    instructions[i] = orig;
                    if (result.Item1)
                    {
                        Console.WriteLine($"Result: {result.Item2}");
                    }
                }
            }
        }

        public static (bool, int) RunProgram(List<(string, int)> instructions)
        {
            var visitedInstructions = new List<int>();
            var acc = 0;
            var curr = 0;
            var returncode = false;
            while (!visitedInstructions.Contains(curr))
            {
                if (curr == instructions.Count)
                {
                    returncode = true;
                    break;
                }

                if (curr < 0 || curr > instructions.Count)
                {
                    break;
                }
                visitedInstructions.Add(curr);
                var instruction = instructions[curr];
                switch (instruction.Item1)
                {
                    case "acc":
                        acc += instruction.Item2;
                        curr++;
                        break;
                    case "nop":
                        curr++;
                        break;
                    case "jmp":
                        curr += instruction.Item2;
                        break;
                }
            }

            return (returncode, acc);
        }
    }
}
