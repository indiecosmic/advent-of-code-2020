using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public static class Day22
    {
        public static long Part1(string[] input = null)
        {
            input ??= Input.ReadAllLines(nameof(Day22));

            //input = new[]
            //{
            //    "Player 1:",
            //    "9",
            //    "2",
            //    "6",
            //    "3",
            //    "1",
            //    "",
            //    "Player 2:",
            //    "5",
            //    "8",
            //    "4",
            //    "7",
            //    "10"
            //};
            
            
            var decks = CreateDecks(input);

            while (decks[0].Count > 0 && decks[1].Count > 0)
            {
                var p1 = decks[0].Dequeue();
                var p2 = decks[1].Dequeue();
                var round = p1 > p2 ? 0 : 1;
                decks[round].Enqueue(Math.Max(p1, p2));
                decks[round].Enqueue(Math.Min(p1, p2));
            }

            var winner = decks[1].Count == 0 ? 0 : 1;
            
            long sum = 0;
            for (var i = decks[winner].Count; i > 0; i--)
            {
                sum += i * decks[winner].Dequeue();
            }

            return sum;
        }

        public static long Part2(string[] input = null)
        {
            input ??= Input.ReadAllLines(nameof(Day22));

            //input = new[]
            //{
            //    "Player 1:",
            //    "9",
            //    "2",
            //    "6",
            //    "3",
            //    "1",
            //    "",
            //    "Player 2:",
            //    "5",
            //    "8",
            //    "4",
            //    "7",
            //    "10"
            //};
            var decks = CreateDecks(input);

            var (_, winningDeck) = Play(decks);
            long sum = 0;
            for (var i = winningDeck.Count; i > 0; i--)
            {
                sum += i * winningDeck.Dequeue();
            }

            return sum;
        }

        private static (int winner, Queue<int> winningDeck) Play(Queue<int>[] decks)
        {
            var previousRounds = new List<string>();
            while (decks[0].Count > 0 && decks[1].Count > 0)
            {
                var state = string.Join(",", decks[0].ToArray()) + ";" + string.Join(",", decks[1].ToArray());
                if (previousRounds.Contains(state)) { 
                    return (0, decks[0]);
                }
                previousRounds.Add(state);
                
                var p1 = decks[0].Dequeue();
                var p2 = decks[1].Dequeue();

                int round;
                if (decks[0].Count >= p1 && decks[1].Count >= p2)
                {
                    var subdeck1 = new int[p1];
                    Array.Copy(decks[0].ToArray(), subdeck1, p1);
                    var subdeck2 = new int[p2];
                    Array.Copy(decks[1].ToArray(), subdeck2, p2);
                    
                    (round, _) = Play(new[]
                    {
                        new Queue<int>(subdeck1),
                        new Queue<int>(subdeck2)
                    });
                    if (round == 0)
                    {
                        decks[round].Enqueue(p1);
                        decks[round].Enqueue(p2);
                    }
                    else
                    {
                        decks[round].Enqueue(p2);
                        decks[round].Enqueue(p1);
                    }
                }
                else
                {
                    round = p1 > p2 ? 0 : 1;
                    decks[round].Enqueue(Math.Max(p1, p2));
                    decks[round].Enqueue(Math.Min(p1, p2));
                }
            }

            var winner = decks[1].Count == 0 ? (0, decks[0]) : (1, decks[1]);
            return winner;
        }

        private static Queue<int>[] CreateDecks(IEnumerable<string> input)
        {
            var decks = new[]
            {
                new Queue<int>(),
                new Queue<int>()
            };
            var p = 0;
            foreach (var line in input)
            {
                if (line.StartsWith("Player"))
                    continue;
                if (string.IsNullOrWhiteSpace(line)) { 
                    p++;
                    continue;
                }
                decks[p].Enqueue(int.Parse(line));
            }

            return decks;
        }
    }
}
