using System;
using FluentAssertions;
using Xunit;

namespace FastWildcard.Tests
{
    public class StringComparisonTests
    {
        [Theory]
        [InlineData("abc", "a?c")]
        public void IsMatch_Ordinal_MatchesOrdinal(string str, string pattern)
        {
            var matchSettings = new MatchSettings {StringComparison = StringComparison.Ordinal};

            var result = FastWildcard.IsMatch(str, pattern, matchSettings);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("Abc", "a?c")]
        public void IsMatch_Ordinal_DoesNotMatchDifferentCase(string str, string pattern)
        {
            var matchSettings = new MatchSettings {StringComparison = StringComparison.Ordinal};

            var result = FastWildcard.IsMatch(str, pattern, matchSettings);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("abc", "a?c")]
        [InlineData("Abc", "a?c")]
        public void IsMatch_OrdinalIgnoreCase_MatchesOrdinalAndDifferentCase(string str, string pattern)
        {
            var matchSettings = new MatchSettings {StringComparison = StringComparison.OrdinalIgnoreCase};

            var result = FastWildcard.IsMatch(str, pattern, matchSettings);

            result.Should().BeTrue();
        }
    }
}
