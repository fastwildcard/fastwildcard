using System;
using System.Linq;
using System.Text;

namespace FastWildcard.Performance.Benchmarks
{
    public static class IterationBuilder
    {
        public static string BuildPattern(int patternLength, int singleCharacterCount, int multiCharacterCount)
        {
            if (patternLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(patternLength));
            if (singleCharacterCount < 0)
                throw new ArgumentOutOfRangeException(nameof(singleCharacterCount));
            if (multiCharacterCount < 0)
                throw new ArgumentOutOfRangeException(nameof(multiCharacterCount));

            var patternBuilder = new StringBuilder(new Bogus.Randomizer().AlphaNumeric(patternLength));

            var random = new Random();
            
            var singleCharacterLocations = Enumerable.Range(0, singleCharacterCount)
                .Select(x => random.Next(0, patternLength))
                .ToList();
            singleCharacterLocations.ForEach(x => patternBuilder[x] = '?');

            var multiCharacterLocations = Enumerable.Range(0, multiCharacterCount)
                .Select(x => random.Next(0, patternLength))
                .ToList();
            multiCharacterLocations.ForEach(x => patternBuilder[x] = '*');

            var pattern = patternBuilder.ToString();
            return pattern;
        }

        public static string BuildTestString(int stringLength)
        {
            if (stringLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(stringLength));

            var str = new Bogus.Randomizer().AlphaNumeric(stringLength);
            return str;
        }
    }
}
