using System.Collections.Generic;

namespace AdventOfCode.Extensions
{
    public static class StringExtensions
    {
        public static int[] AllIndexesOf(this string str, char value)
        {
            var indexes = new List<int>();
            for (var i = 0; ; i++)
            {
                i = str.IndexOf(value, i);
                if (i == -1)
                    return indexes.ToArray();
                indexes.Add(i);
            }
        }
    }
}
