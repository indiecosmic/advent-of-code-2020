using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day18
    {
        public static long Part1(string[] input = null)
        {
            //input = new[]
            //{
            //    "2 * 3 + (4 * 5)",
            //    "5 + (8 * 3 + 9 + 3 * 4 * 3)",
            //    "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))",
            //    "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2"
            //};

            input ??= Input.ReadAllLines(nameof(Day18));

            var sum = 0L;
            foreach (var line in input)
            {
                var expression = new Queue<char>(line.Replace(" ", ""));
                var result = Eval(expression);
                sum += result;
            }

            return sum;
        }
        
        public static long Part2(string[] input = null)
        {
            //input = new[]
            //{
            //    "2 * 3 + (4 * 5)",
            //    "5 + (8 * 3 + 9 + 3 * 4 * 3)",
            //    "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))",
            //    "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2"
            //};

            input ??= Input.ReadAllLines(nameof(Day18));

            var sum = 0L;
            foreach (var line in input)
            {
                var expression = new Queue<char>(line.Replace(" ", ""));
                var result = Eval2(expression);
                sum += result;
            }

            return sum;
        }

        public static long Eval(Queue<char> input)
        {
            var op = '*';
            long? res = null;
            long prev = 1;
            while (input.Count > 0)
            {
                var token = input.Dequeue();
                if (token == '(')
                {
                    res = Eval(input);
                }
                else if (char.IsNumber(token))
                {
                    res = (int)char.GetNumericValue(token);
                }
                else if (token == '*' || token == '+')
                {
                    op = token;
                }
                else if (token == ')')
                {
                    return prev;
                }

                if (res != null)
                {
                    if (op == '+')
                        prev += res.Value;
                    else if (op == '*')
                        prev *= res.Value;
                    res = null;
                }
            }

            return prev;
        }
        
        public static long Eval2(Queue<char> input)
        {
            var op = '*';
            long? res = null;
            long prev = 1;
            while (input.Count > 0)
            {
                var token = input.Dequeue();
                if (token == '(')
                {
                    res = Eval2(input);
                }
                else if (char.IsNumber(token))
                {
                    res = (int)char.GetNumericValue(token);
                }
                else if (token == '*' || token == '+')
                {
                    op = token;
                }
                else if (token == ')')
                {
                    return prev;
                }

                if (res != null)
                {
                    if (op == '+')
                        prev += res.Value;
                    else if (op == '*')
                        while (input.Count > 0 && input.Peek() == '+')
                        {
                            input.Dequeue();
                            var token2 = input.Dequeue();
                            if (char.IsNumber(token2))
                            {
                                res += (int)char.GetNumericValue(token2);
                            } else if (token2 == '(')
                            {
                                res += Eval2(input);
                            }
                        }
                    prev *= res.Value;
                    res = null;
                }
            }

            return prev;
        }

        
    }
}
