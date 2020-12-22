using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day20
    {



        private static char[,] Rotate(char[,] matrix)
        {
            var ret = new char[10, 10];

            for (var i = 0; i < 10; ++i)
            {
                for (var j = 0; j < 10; ++j)
                {
                    ret[i, j] = matrix[10 - j - 1, i];
                }
            }
            return ret;
        }

        private static char[,] Flip(char[,] arrayToFlip)
        {
            var flippedArray = new char[10, 10];

            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    flippedArray[i, j] = arrayToFlip[(10 - 1) - i, j];
                }
            }
            return flippedArray;
        }
    }
}
