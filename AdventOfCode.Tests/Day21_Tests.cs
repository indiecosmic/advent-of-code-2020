using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day21_Tests
    {
        [Fact]
        public void ShouldFindNumberOfAllergyFreeIngredients()
        {
            var input = new[]
            {
                "mxmxvkd kfcds sqjhc nhms (contains dairy, fish)",
                "trh fvjkl sbzzf mxmxvkd (contains dairy)",
                "sqjhc fvjkl (contains soy)",
                "sqjhc mxmxvkd sbzzf (contains fish)"
            };
            var actual = Day21.Part1(input);
            Assert.Equal(5, actual);
        }
    }
}
