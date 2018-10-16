using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoFixture.Xunit2;
using FastWildcard.Performance.Benchmarks;
using FluentAssertions;
using Xunit;

namespace FastWildcard.Performance.Tests
{
    public class IterationBuilderTests
    {
        [Theory, AutoData]
        public void BuildPattern_InvalidParameter_ThrowsException(
            [Range(-100, 0)] int patternLength,
            [Range(-100, -1)] int singleCharacterCount,
            [Range(-100, -1)] int multiCharacterCount
        )
        {
            Action action = () => IterationBuilder.BuildPattern(patternLength, singleCharacterCount, multiCharacterCount);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory, AutoData]
        public void BuildPattern_OfLength_ReturnsStringWithLength(
            [Range(1, 100)] int patternLength,
            [Range(0, 100)] int singleCharacterCount,
            [Range(0, 100)] int multiCharacterCount
        )
        {
            var (result, newSingleCount, newMultiCount) = IterationBuilder.BuildPattern(patternLength, singleCharacterCount, multiCharacterCount);

            result.Should().NotBeNullOrWhiteSpace()
                .And.HaveLength(patternLength)
                .And.Match(s => s.Count(ch => ch == '?') == newSingleCount)
                .And.Match(s => s.Count(ch => ch == '*') == newMultiCount);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void BuildTestString_InvalidParameter_ThrowsException(string pattern)
        {
            Action action = () => IterationBuilder.BuildTestString(pattern);

            action.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void BuildTestString_OfLength_ReturnsStringWithLength(
            [Range(1, 100)] int patternLength,
            [Range(0, 100)] int singleCharacterCount,
            [Range(0, 100)] int multiCharacterCount
        )
        {
            var (pattern, _, _) = IterationBuilder.BuildPattern(patternLength, singleCharacterCount, multiCharacterCount);
            
            var result = IterationBuilder.BuildTestString(pattern);

            result.Should().NotBeNullOrWhiteSpace();
        }
    }
}
