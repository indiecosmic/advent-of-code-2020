using System;

namespace AdventOfCode
{
    public class Day25
    {
        public static long Part1(string[] input = null)
        {
            input ??= Input.ReadAllLines(nameof(Day25));

            var publicKey1 = long.Parse(input[0]);
            var publicKey2 = long.Parse(input[1]);

            var loopSize1 = CalculateLoopSize(publicKey1);
            var loopSize2 = CalculateLoopSize(publicKey2);

            var encryptionKey = CalculateEncryptionKey(publicKey1, loopSize2);
            var confirm = CalculateEncryptionKey(publicKey2, loopSize1);
            if (encryptionKey != confirm)
                throw new Exception();

            return encryptionKey;
        }

        public static long CalculateLoopSize(long publicKey)
        {
            const long subjectNumber = 7;
            long value = 1;
            var loopSize = 0;
            while (true)
            {
                value *= subjectNumber;
                value %= 20201227;
                loopSize++;
                if (value == publicKey)
                    return loopSize;
            }
        }

        public static long CalculateEncryptionKey(long subjectNumber, long loopSize)
        {
            long value = 1;
            for (var i = 0; i < loopSize; i++)
            {
                value *= subjectNumber;
                value %= 20201227;
            }

            return value;
        }
    }
}
