using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day15_Tests
    {
        [Theory]
        [InlineData(1, new[] {1, 3, 2}, 2020)]
        [InlineData(10, new[] {2, 1, 3}, 2020)]
        [InlineData(27, new[] {1, 2, 3}, 2020)]
        [InlineData(78, new[] {2, 3, 1}, 2020)]
        [InlineData(438, new[] {3, 2, 1}, 2020)]
        [InlineData(1836, new[] {3, 1, 2}, 2020)]
        [InlineData(175594, new[]{0,3,6}, 30000000)]
        public void ShouldReturnNumber(int expected, int[] startingNumbers, int n)
        {
            var actual = Day15.GetNthNumberSpoken(startingNumbers, n);
            Assert.Equal(expected, actual);
        }
    }
}
