using Xunit;

namespace AdventOfCode.Tests
{
    public class Day25_Tests
    {
        [Theory]
        [InlineData(5764801, 8)]
        [InlineData(17807724, 11)]
        public void ShouldCalculateLoopSize(long publicKey, long expected)
        {
            var actual = Day25.CalculateLoopSize(publicKey);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(17807724, 8, 14897079)]
        [InlineData(5764801, 11, 14897079)]
        public void ShouldCalculateEncryptionKey(long subjectNumber, long loopSize, long expected)
        {
            var actual = Day25.CalculateEncryptionKey(subjectNumber, loopSize);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("5764801", "17807724", 14897079)]
        public void ShouldSolvePart1(string publicKey1, string publicKey2, long expected)
        {
            var actual = Day25.Part1(new[] { publicKey1, publicKey2 });
            Assert.Equal(expected, actual);
        }
    }
}
