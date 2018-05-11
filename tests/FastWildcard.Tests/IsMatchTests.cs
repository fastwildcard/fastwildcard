using System;
using FluentAssertions;
using Xunit;

namespace FastWildcard.Tests
{
    public class IsMatchTests
    {
        [Theory]
        [InlineData("abcde", null)]
        [InlineData("abcde", "")]
        public void SingleCharacterWildcard_WithInvalidInputs_ThrowsException(string str, string pattern)
        {
            var resultAction = new Action(() => FastWildcard.IsMatch(str, pattern));

            resultAction.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(null, "a?c")]
        [InlineData("", "a?c")]
        public void SingleCharacterWildcard_WithBlankInputs_ReturnsFalse(string str, string pattern)
        {
            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("ab", "a?c")]
        [InlineData("abc", "a?")]
        [InlineData("abc", " ")]
        public void SingleCharacterWildcard_WithMatchAndLengthEdgeCases_ReturnsFalse(string str, string pattern)
        {
            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("a", "?")]
        [InlineData("abc", "a?c")]
        [InlineData("abcde", "a?c?e")]
        [InlineData("abcde", "a???e")]
        [InlineData("abcde", "?bcde")]
        [InlineData("abcde", "abcd?")]
        public void SingleCharacterWildcard_WithMatch_ReturnsTrue(string str, string pattern)
        {
            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("bbcde", "a?cde")]
        [InlineData("bacde", "?bcde")]
        [InlineData("bbcde", "abcd?")]
        public void SingleCharacterWildcard_WithNoMatch_ReturnsFalse(string str, string pattern)
        {
            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("", "*")]
        public void MultiCharacterWildcard_WithMatchAndLengthEdgeCases_ReturnsTrue(string str, string pattern)
        {
            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("abc", "a*c")]
        [InlineData("abcde", "a*e")]
        [InlineData("abcde", "*bcde")]
        [InlineData("abcde", "abcd*")]
        public void MultiCharacterWildcard_WithMatch_ReturnsTrue(string str, string pattern)
        {
            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("abbde", "a*cde")]
        [InlineData("bbcde", "a*cde")]
        [InlineData("bacde", "*bcde")]
        [InlineData("bbcde", "abcd*")]
        public void MultiCharacterWildcard_WithNoMatch_ReturnsFalse(string str, string pattern)
        {
            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("abcde", "a?c*e")]
        [InlineData("abcde", "a*c?e")]
        [InlineData("abcde", "a?*e")]
        [InlineData("abcde", "a*?e")]
        [InlineData("abcde", "a?*")]
        [InlineData("abcde", "a*?")]
        [InlineData("abcde", "?*e")]
        [InlineData("abcde", "*?e")]
        [InlineData("abcde", "*?")]
        [InlineData("abcde", "?*")]
        public void MixedWildcard_WithMatch_ReturnsTrue(string str, string pattern)
        {
            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("abcde", "abcde")]
        [InlineData(" ", " ")]
        public void NoWildcard_WithMatch_ReturnsTrue(string str, string pattern)
        {
            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("abbde", "abcde")]
        [InlineData(" ", "  ")]
        [InlineData("  ", " ")]
        public void NoWildcard_WithNoMatch_ReturnsFalse(string str, string pattern)
        {
            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeFalse();
        }
    }
}
