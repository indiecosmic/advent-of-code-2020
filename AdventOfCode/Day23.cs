using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day23
    {
        public static string Part1(string input = null)
        {
            input ??= "653427918";

            var cups = new LinkedList<int>(input.Select(c => (int)char.GetNumericValue(c)));
            var current = cups.First;
            for (var i = 0; i < 100; i++)
            {
                var destinationValue = current.Value - 1;
                var next = current == cups.Last ? cups.First : current?.Next;
                var pickup = new LinkedListNode<int>[3];
                for (var p = 0; p < 3; p++)
                {
                    pickup[p] = next;
                    var prev = next;
                    next = (next == cups.Last) ? cups.First : next?.Next;
                    cups.Remove(prev);
                }

                var destination = FindDestination(cups, destinationValue);
                for (var p = 2; p >= 0; p--)
                {
                    cups.AddAfter(destination, pickup[p]);
                }

                current = current == cups.Last ? cups.First : current?.Next;
            }

            var result = "";
            var start = FindDestination(cups, 1);
            start = start == cups.Last ? cups.First : start.Next;
            for (var i = 0; i < cups.Count - 1; i++)
            {
                result += start.Value;
                start = start == cups.Last ? cups.First : start.Next;
            }
            
            return result;
        }

        public static string Part2(string input = null)
        {
            input ??= "653427918";

            var cupDict = new Dictionary<int, LinkedListNode<int>>();
            var cups = new LinkedList<int>();
            foreach (var c in input)
            {
                var number = (int) char.GetNumericValue(c);
                var node = cups.AddLast(number);
                cupDict.Add(number, node);
            }
            
            foreach (var number in Enumerable.Range(10, 999991))
            {
                var node = cups.AddLast(number);
                cupDict.Add(number, node);
            }
            
            var current = cups.First;
            var pickup = new LinkedListNode<int>[3];
            for (var i = 0; i < 10000000; i++)
            {
                var destinationValue = current.Value - 1;
                var next = current == cups.Last ? cups.First : current?.Next;
                for (var p = 0; p < 3; p++)
                {
                    pickup[p] = next;
                    var prev = next;
                    next = (next == cups.Last) ? cups.First : next?.Next;
                    cups.Remove(prev);
                    cupDict.Remove(prev.Value);
                }

                var destination = FindDestination(cupDict, destinationValue);
                for (var p = 2; p >= 0; p--)
                {
                    cups.AddAfter(destination, pickup[p]);
                    cupDict.Add(pickup[p].Value, pickup[p]);
                }

                current = current == cups.Last ? cups.First : current?.Next;
            }

            var start = FindDestination(cupDict, 1);
            var res = (long)start.Next.Value * (long)start.Next.Next.Value;

            return res.ToString();
        }

        public static LinkedListNode<int> FindDestination(LinkedList<int> cups, int value)
        {
            LinkedListNode<int> destination = null;
            while (destination == null)
            {
                destination = cups.Find(value);
                value--;
                if (value <= 0)
                    value = 9;
            }

            return destination;
        }

        public static LinkedListNode<int> FindDestination(Dictionary<int, LinkedListNode<int>> cupDict, int value)
        {
            while (true)
            {
                if (cupDict.ContainsKey(value))
                {
                    return cupDict[value];
                }
                value--;
                if (value <= 0)
                    value = 1000000;
            }
        }
    }
}
