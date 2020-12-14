using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Extensions;

namespace AdventOfCode
{
    public static class Day14
    {
        internal static void Run()
        {
            var instructions = Input.ReadAllLines(nameof(Day14));

            Console.WriteLine(Part1(instructions));
            Console.WriteLine(Part2(instructions));
        }

        private static long Part1(string[] instructions)
        {
            var currentMask = "";
            var mem = new Dictionary<int, long>();
            foreach (var instruction in instructions)
            {
                var instructionParts = instruction.Split(" = ", StringSplitOptions.RemoveEmptyEntries);
                if (instructionParts[0] == "mask")
                {
                    currentMask = instructionParts[1];
                }
                else
                {
                    var value = ApplyMask(instructionParts[1], currentMask);
                    var pos = int.Parse(instructionParts[0].Replace("mem[", "").Replace("]", ""));
                    mem[pos] = value;
                }
            }

            return mem.Values.Sum();
        }

        public static long Part2(string[] instructions)
        {
            var currentMask = "";
            var mem = new Dictionary<long, long>();
            foreach (var instruction in instructions)
            {
                var instructionParts = instruction.Split(" = ", StringSplitOptions.RemoveEmptyEntries);
                if (instructionParts[0] == "mask")
                {
                    currentMask = instructionParts[1];
                }
                else
                {
                    var pos = instructionParts[0].Replace("mem[", "").Replace("]", "");
                    var maskedAddress = ApplyAddressMask(pos, currentMask);
                    var addresses = GetAddressVariants(maskedAddress);
                    var value = long.Parse(instructionParts[1]);
                    foreach (var address in addresses)
                    {
                        var decimalAddress = Convert.ToInt64(new string(address), 2);
                        mem[decimalAddress] = value;
                    }
                }
            }

            return mem.Values.Sum();
        }

        private static long ApplyMask(string v, string currentMask)
        {
            var dec = long.Parse(v);
            var binary = Convert.ToString(dec, 2).PadLeft(36, '0').ToArray();
            var mask = currentMask.ToArray();
            for (var i = 0; i < 36; i++)
            {
                binary[i] = mask[i] == 'X' ? binary[i] : mask[i];
            }

            var result = Convert.ToInt64(new string(binary), 2);
            return result;
        }

        public static string ApplyAddressMask(string value, string bitmask)
        {
            var dec = long.Parse(value);
            var binary = Convert.ToString(dec, 2).PadLeft(36, '0').ToArray();
            var mask = bitmask.ToArray();
            for (var i = 0; i < 36; i++)
            {
                binary[i] = mask[i] == '0' ? binary[i] : mask[i];
            }

            return new string(binary);
        }

        public static string[] GetAddressVariants(string maskedValue)
        {
            var indexes = maskedValue.AllIndexesOf('X');
            if (indexes.Length == 0)
                return new[] { maskedValue };

            var variants = new List<string>();
            var binary = maskedValue.ToArray();
            CreateBinaryStrings(binary, 0, indexes, variants);
            return variants.ToArray();
        }

        private static void CreateBinaryStrings(char[] arr, int pos, int[] indexes, List<string> variants)
        {
            if (pos == indexes.Length)
            {
                variants.Add(new string(arr));
                return;
            }

            arr[indexes[pos]] = '0';
            CreateBinaryStrings(arr, pos + 1, indexes, variants);
            arr[indexes[pos]] = '1';
            CreateBinaryStrings(arr, pos + 1, indexes, variants);
        }
    }
}
