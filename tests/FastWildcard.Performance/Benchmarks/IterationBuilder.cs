using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastWildcard.Performance.Benchmarks
{
    public static class IterationBuilder
    {
        public static (string result, int newSingleCount, int newMultiCount) BuildPattern(int patternLength, int singleCountSuggestion, int multiCountSuggestion)
        {
            const int wildcardPercentage = 10;

            if (patternLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(patternLength));
            if (singleCountSuggestion < 0)
                throw new ArgumentOutOfRangeException(nameof(singleCountSuggestion));
            if (multiCountSuggestion < 0)
                throw new ArgumentOutOfRangeException(nameof(multiCountSuggestion));

            var adjustedSingleCount = Math.Min(singleCountSuggestion, patternLength) / wildcardPercentage;
            var adjustedMultiCount = Math.Min(multiCountSuggestion, patternLength) / wildcardPercentage;

            var patternBuilder = new StringBuilder(new Bogus.Randomizer().AlphaNumeric(patternLength));

            var random = new Random();

            var singleCharacterLocations = new List<int>();
            if (singleCountSuggestion > 0)
            {
                singleCharacterLocations = Enumerable.Range(0, adjustedSingleCount)
                    .Select(x => random.Next(0, patternLength))
                    .ToList();
                singleCharacterLocations.ForEach(x => patternBuilder[x] = '?');
            }

            var multiCharacterLocations = new List<int>();
            if (multiCountSuggestion > 0)
            {
                multiCharacterLocations = Enumerable.Range(0, adjustedMultiCount)
                    .Select(x => random.Next(0, patternLength))
                    .Where(x => !singleCharacterLocations.Contains(x))
                    .ToList();
                multiCharacterLocations.ForEach(x => patternBuilder[x] = '*');
            }

            var pattern = patternBuilder.ToString();
            return (pattern, singleCharacterLocations.Count, multiCharacterLocations.Count);
        }

        public static string BuildTestString(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(pattern));

            var randomizer = new Bogus.Randomizer();

            const int noMatchPercentage = 50;
            if (WeightedMatch(noMatchPercentage))
            {
                return randomizer.String();
            }

            const int charMatchPercentage = 95;
            var strBuilder = new StringBuilder(pattern.Length * 2);
            foreach (var patternCh in pattern)
            {
                if (patternCh == '?')
                {
                    strBuilder.Append(randomizer.AlphaNumeric(1));
                }
                else if (patternCh == '*')
                {
                    strBuilder.Append(randomizer.Words());
                }
                else
                {
                    if (WeightedMatch(charMatchPercentage))
                    {
                        strBuilder.Append(patternCh);
                    }
                }
            }

            var str = strBuilder.ToString();
            return str;

            bool WeightedMatch(int percentage)
            {
                return randomizer.Bool(percentage / 100f);
            }
        }
    }
}
