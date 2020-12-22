using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day19
    {
        public static int Part1(string input = null)
        {
            input ??= Input.ReadAllText(nameof(Day19));
            var inputParts = input.Split("\n\n",
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var ruleStrings = inputParts[0].Split("\n");
            var messages = inputParts[1]
                .Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            return ValidateMessages(ruleStrings, messages);
        }

        public static int ValidateMessages(string[] ruleStrings, string[] messages)
        {
            var rules = CreateRules(ruleStrings);
            var validCount = 0;
            foreach (var message in messages)
            {
                var (valid, pos) = Validate(message, 0, rules[0], rules);
                if (valid && pos == message.Length)
                    validCount++;
            }
            
            return validCount;
        }
        
        public static (bool valid, int pos) Validate(string message, int pos, Rule rule, Dictionary<int, Rule> rules)
        {
            if (pos >= message.Length)
            {
                return (false, pos);
            }
            
            if (rule.Letter != 0)
            {
                return (message[pos] == rule.Letter, pos+1);
            }

            bool? isValid = null;
            var curr = pos;
            for (var i = 0; i < rule.Alt1.Length; i++)
            {
                isValid = true;
                var (valid, newPos) = Validate(message, curr, rules[rule.Alt1[i]], rules);
                if (!valid)
                {
                    isValid = false;
                    break;
                }

                curr = newPos;
            }

            if (isValid.HasValue && isValid.Value)
                return (true, curr);

            curr = pos;
            for (var i = 0; i < rule.Alt2.Length; i++)
            {
                isValid = true;
                var (valid, newPos) = Validate(message, curr, rules[rule.Alt2[i]], rules);
                if (!valid)
                {
                    isValid = false;
                    break;
                }

                curr = newPos;
            }

            return (isValid??false, curr);
        }

        private static Dictionary<int, Rule> CreateRules(string[] ruleStrings)
        {
            var rules = new Dictionary<int, Rule>();
            foreach (var ruleString in ruleStrings)
            {
                var ruleId = int.Parse(ruleString.Substring(0, ruleString.IndexOf(":", StringComparison.Ordinal)));
                var branches = ruleString
                    .Substring(ruleString.IndexOf(':') + 1)
                    .Split("|", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                if (branches.Length == 2)
                {
                    rules.Add(ruleId, new Rule()
                    {
                        Alt1 = branches[0].Split(" ").Select(int.Parse).ToArray(),
                        Alt2 = branches[1].Split(" ").Select(int.Parse).ToArray()
                    });
                }
                else
                {
                    var branch = branches[0].Replace("\"", "");
                    if (branch == "a" || branch == "b")
                    {
                        rules.Add(ruleId, new Rule()
                        {
                            Letter = branch[0]
                        });
                    }
                    else
                    {
                        rules.Add(ruleId, new Rule()
                        {
                            Alt1 = branch.Split(" ").Select(int.Parse).ToArray()
                        });
                    }
                }
            }

            return rules;
        }

        public class Rule
        {
            public int[] Alt1 { get; init; } = Array.Empty<int>();
            public int[] Alt2 { get; init; } = Array.Empty<int>();
            public char Letter { get; init; }

            public override string ToString()
            {
                if (Letter != 0)
                    return Letter.ToString();
                if (Alt1.Length > 0 && Alt2.Length > 0)
                    return string.Join(' ', Alt1) + " | " + string.Join(' ', Alt2);
                if (Alt1.Length > 0)
                    return string.Join(' ', Alt1);
                return string.Join(' ', Alt2);
            }
        }
    }
}
