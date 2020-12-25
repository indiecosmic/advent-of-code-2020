using System;
using System.Collections.Generic;
using System.Linq;

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

        public static int Part2(string input = null)
        {
            input ??= Input.ReadAllText(nameof(Day19));
            var inputParts = input.Split("\n\n",
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var ruleStrings = inputParts[0].Split("\n");
            var messages = inputParts[1]
                .Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var rules = CreateRules(ruleStrings);
            rules[11] = Rule.Parse("11: 42 31 | 42 11 31");
            rules[8] = Rule.Parse("8: 42 | 42 8");

            var valid = 0;
            foreach (var message in messages)
            {
                if (GenerateVariants(0, rules, message).Contains(message))
                    valid++;
            }

            return valid;
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

        public static IEnumerable<string> GenerateVariants(int ruleId, Dictionary<int, Rule> rules, string find)
        {
            var rule = rules[ruleId];

            if (rule.IsLetter)
                yield return rule.Value;
            else
            {
                foreach (var subRule in rule.SubRules)
                {
                    switch (subRule.Length)
                    {
                        case 1:
                            var strings = GenerateVariants(subRule[0], rules, find);
                            foreach (var s in strings)
                            {
                                yield return s;
                            }

                            break;
                        case 2:
                            var leftStrings = GenerateVariants(subRule[0], rules, find);

                            foreach (var left in leftStrings)
                            {
                                if (!find.StartsWith(left))
                                    continue;

                                var rightStrings = GenerateVariants(subRule[1], rules, find.Substring(left.Length));
                                foreach (var right in rightStrings)
                                {
                                    yield return left + right;
                                }
                            }

                            break;
                        case 3:
                            var first = GenerateVariants(subRule[0], rules, find);

                            foreach (var f in first)
                            {
                                if (!find.StartsWith(f))
                                    continue;

                                var second = GenerateVariants(subRule[1], rules, find.Substring(f.Length));
                                foreach (var s in second)
                                {
                                    if (!find.StartsWith(f + s))
                                        continue;
                                    var third = GenerateVariants(subRule[2], rules, find.Substring(f.Length + s.Length));
                                    foreach (var t in third)
                                    {
                                        yield return f + s + t;
                                    }
                                }
                            }

                            break;
                    }
                }
            }
        }

        public static (bool valid, int pos) Validate(string message, int pos, Rule rule, Dictionary<int, Rule> rules)
        {
            if (pos >= message.Length)
            {
                return (false, pos);
            }

            if (rule.Letter != 0)
            {
                return (message[pos] == rule.Letter, pos + 1);
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

            return (isValid ?? false, curr);
        }

        public static Dictionary<int, Rule> CreateRules(string[] ruleStrings)
        {
            var rules = new Dictionary<int, Rule>();
            foreach (var ruleString in ruleStrings)
            {
                var rule = Rule.Parse(ruleString);
                rules.Add(rule.Id, rule);
            }

            return rules;
        }

        public class Rule
        {
            public int Id { get; init; }
            public int[] Alt1 { get; init; } = Array.Empty<int>();
            public int[] Alt2 { get; init; } = Array.Empty<int>();
            private List<int[]> _subRules;
            public List<int[]> SubRules
            {
                get
                {
                    if (_subRules != null) return _subRules;
                    _subRules = new List<int[]>();
                    if (Alt1.Length > 0)
                        _subRules.Add(Alt1);
                    if (Alt2.Length > 0)
                        _subRules.Add(Alt2);

                    return _subRules;
                }
            }
            public char Letter { get; init; }
            public bool IsLetter => Letter != 0;
            public string Value => Letter.ToString();

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

            public static Rule Parse(string input)
            {
                input = input.Replace("\"", "");
                var separatorIx = input.IndexOf(":", StringComparison.Ordinal);
                var ruleId = int.Parse(input.Substring(0, separatorIx));
                
                var subRules = input
                    .Substring(separatorIx + 1)
                    .Split("|", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (subRules.Length == 2)
                    return new Rule
                    {
                        Id = ruleId,
                        Alt1 = subRules[0].Split(" ").Select(int.Parse).ToArray(),
                        Alt2 = subRules[1].Split(" ").Select(int.Parse).ToArray()
                    };
                if (subRules[0] == "a" || subRules[0] == "b")
                {
                    return new Rule
                    {
                        Id = ruleId,
                        Letter = subRules[0][0]
                    };
                }
                return new Rule
                {
                    Id = ruleId,
                    Alt1 = subRules[0].Split(" ").Select(int.Parse).ToArray()
                };
            }
        }
    }
}
