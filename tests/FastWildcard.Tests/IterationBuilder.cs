using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastWildcard.Tests
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
                multiCharacterLocations
                    .OrderByDescending(x => x)
                    .ToList()
                    .ForEach(x =>
                    {
                        patternBuilder[x] = '*';
                        var randomLengthToRemove = random.Next(0, (int) (patternLength * 0.1));
                        var startIndex = x + 1;
                        var charsToRemove = Math.Min(patternBuilder.Length - startIndex, randomLengthToRemove);
                        patternBuilder.Remove(startIndex, charsToRemove);
                    });
            }

            var pattern = patternBuilder.ToString();

            return (pattern, singleCharacterLocations.Count, multiCharacterLocations.Count);
        }

        public static string BuildTestString(string pattern, int noMatchPercentage = 1, int charMatchPercentage = 99)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(pattern));

            var randomizer = new Bogus.Randomizer();

            if (WeightedMatch(noMatchPercentage))
            {
                return randomizer.String();
            }

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
