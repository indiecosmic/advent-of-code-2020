using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode
{
    public static class Input
    {
        public static string[] ReadAllLines(string day)
        {
            var filename = $"{day.ToLower()}_input.txt";
            return File.ReadAllLines(filename);
        }
       
    }
}
