using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day19_Tests
    {
        [Fact]
        public void ShouldValidateMessage()
        {
            var ruleStrings = new[]
            {
                "0: 4 1 5",
                "1: 2 3 | 3 2",
                "2: 4 4 | 5 5",
                "3: 4 5 | 5 4",
                "4: \"a\"",
                "5: \"b\"",
            };
            var messages = new[]
            {
                "ababbb"
            };
            var actual = Day19.ValidateMessages(ruleStrings, messages);
            Assert.Equal(1, actual);
        }
        
        [Fact]
        public void ShouldValidateMessages()
        {
            var ruleStrings = new[]
            {
                "0: 4 1 5",
                "1: 2 3 | 3 2",
                "2: 4 4 | 5 5",
                "3: 4 5 | 5 4",
                "4: \"a\"",
                "5: \"b\"",
            };
            var messages = new[]
            {
                "ababbb",
                "bababa",
                "abbbab",
                "aaabbb",
                "aaaabbb"
            };
            var actual = Day19.ValidateMessages(ruleStrings, messages);
            Assert.Equal(2, actual);
        }

        [Fact]
        public void ShouldValidateMessagesPart2()
        {
            var rules = new[]
            {
                "42: 9 14 | 10 1",
                "9: 14 27 | 1 26",
                "10: 23 14 | 28 1",
                "1: \"a\"",
                "11: 42 31",
                "5: 1 14 | 15 1",
                "19: 14 1 | 14 14",
                "12: 24 14 | 19 1",
                "16: 15 1 | 14 14",
                "31: 14 17 | 1 13",
                "6: 14 14 | 1 14",
                "2: 1 24 | 14 4",
                "0: 8 11",
                "13: 14 3 | 1 12",
                "15: 1 | 14",
                "17: 14 2 | 1 7",
                "23: 25 1 | 22 14",
                "28: 16 1",
                "4: 1 1",
                "20: 14 14 | 1 15",
                "3: 5 14 | 16 1",
                "27: 1 6 | 14 18",
                "14: \"b\"",
                "21: 14 1 | 1 14",
                "25: 1 1 | 1 14",
                "22: 14 14",
                "8: 42",
                "26: 14 22 | 1 20",
                "18: 15 15",
                "7: 14 5 | 1 21",
                "24: 14 1"
            };
            var messages = new[]
            {
                "abbbbbabbbaaaababbaabbbbabababbbabbbbbbabaaaa",
                "bbabbbbaabaabba",
                "babbbbaabbbbbabbbbbbaabaaabaaa",
                "aaabbbbbbaaaabaababaabababbabaaabbababababaaa",
                "bbbbbbbaaaabbbbaaabbabaaa",
                "bbbababbbbaaaaaaaabbababaaababaabab",
                "ababaaaaaabaaab",
                "ababaaaaabbbaba",
                "baabbaaaabbaaaababbaababb",
                "abbbbabbbbaaaababbbbbbaaaababb",
                "aaaaabbaabaaaaababaa",
                "aaaabbaaaabbaaa",
                "aaaabbaabbaaaaaaabbbabbbaaabbaabaaa",
                "babaaabbbaaabaababbaabababaaab",
                "aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba"
            };
            var actual = Day19.ValidateMessages(rules, messages);
            Assert.Equal(3, actual);

            rules[4] = "11: 42 31 | 42 11 31";
            rules[26] = "8: 42 | 42 8";

            var r = Day19.CreateRules(rules);

            var x = Day19.Rule.Parse("11: 42 31 | 42 11 31");
            
            
            var count = 0;
            foreach (var message in messages)
            {
                var variants = Day19.GenerateVariants(0, r, message);
                if (variants.Contains(message))
                    count++;
            }
            Assert.Equal(12, count);
        }
    }
}
