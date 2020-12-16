using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day16_Tests
    {
        [Fact]
        public void ShouldCalculateFields()
        {
            var input = new[]
            {
                "class: 0-1 or 4-19",
                "row: 0-5 or 8-19",
                "seat: 0-13 or 16-19",
                "",
                "your ticket:",
                "11,12,13",
                "",
                "nearby tickets:",
                "3,9,18",
                "15,1,5",
                "5,14,9"
            };
            var actual = Day16.CalculateFields(input);
            Assert.Collection(actual.OrderBy(a => a.name),
                    field => Assert.Equal(("class", 12), field),
                    field => Assert.Equal(("row", 11), field),
                    field => Assert.Equal(("seat", 13), field)
                    );
        }
    }
}
