using System;
using System.Linq;

namespace AdventOfCode
{
    public class Day9
    {
        public static void Run()
        {
            var input = Input.ReadAllLines(nameof(Day9));
            var numbers = input.Select(long.Parse).ToArray();
            const int preambleLength = 25;
            //var numbers = new long[]
            //{
            //    35, 20, 15, 25, 47, 40, 62, 55, 65, 95, 102, 117, 150, 182, 127, 219, 299, 277, 309, 576
            //};
            //const int preambleLength = 5;
            var invalidNumber = FindInvalidNumber(numbers, preambleLength);
            Console.WriteLine($"Invalid number: {invalidNumber}");
            var res = Break(numbers, invalidNumber);
            var weakness = res.Min() + res.Max();
            Console.WriteLine($"Weakness: {weakness}");
        }

        public static long FindInvalidNumber(long[] numbers, int preambleLength)
        {
            for (var i = preambleLength; i < numbers.Length; i++)
            {
                Index i1 = i - (preambleLength);
                Index i2 = i;
                if (!IsValid(numbers[i], numbers[i1..i2]))
                {
                    return numbers[i];
                }
            }

            return -1;
        }

        public static long[] Break(long[] numbers, long invalidNumber)
        {
            for (var i = 0; i < numbers.Length; i++)
            {

                for (var j = i + 1; j < numbers.Length; j++)
                {
                    Index i1 = i;
                    Index i2 = j;

                    if (numbers[i1..i2].Sum() == invalidNumber)
                    {
                        return numbers[i1..i2];
                    }
                }
            }

            return null;
        }

        public static bool IsValid(long number, long[] previousNumbers)
        {
            for (var i = 0; i < previousNumbers.Length; i++)
            {
                for (var j = 0; j < previousNumbers.Length; j++)
                {
                    if (i == j) continue;

                    if (previousNumbers[i] + previousNumbers[j] == number)
                        return true;
                }
            }

            return false;
        }
    }
}
