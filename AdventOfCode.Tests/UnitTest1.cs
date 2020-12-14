using Xunit;

namespace AdventOfCode.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void ShouldApplyMask()
        {
            const string mask = "000000000000000000000000000000X1001X";
            const string value = "42";
            var actual = Day14.ApplyAddressMask(value, mask);
            Assert.Equal("000000000000000000000000000000X1101X", actual);
        }

        [Fact]
        public void ShouldFindAllVariants()
        {
            const string value = "000000000000000000000000000000X1101X";
            var actual = Day14.GetAddressVariants(value);
            var expected = new[]
            {
                "000000000000000000000000000000011010",
                "000000000000000000000000000000011011",
                "000000000000000000000000000000111010",
                "000000000000000000000000000000111011"
            };
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldCalculateSum()
        {
            var instructions = new[]
            {
                "mask = 000000000000000000000000000000X1001X",
                "mem[42] = 100",
                "mask = 00000000000000000000000000000000X0XX",
                "mem[26] = 1"
            };
            var actual = Day14.Part2(instructions);
            Assert.Equal(208, actual);
        }
    }
}
