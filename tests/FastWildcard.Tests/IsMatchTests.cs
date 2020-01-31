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
        [InlineData(" ", "*")]
        [InlineData(" ", "**")]
        [InlineData(" ", "***")]
        public void MultiCharacterWildcard_WithMatchAndLengthEdgeCases_ReturnsTrue(string str, string pattern)
        {
            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("aaa", "a*a")]
        [InlineData("abc", "a*c")]
        [InlineData("abcde", "a*e")]
        [InlineData("abcde", "a**e")]
        [InlineData("abcde", "*bcde")]
        [InlineData("abcde", "abcd*")]
        [InlineData("abcdefg", "*bc*fg")]
        [InlineData("abc/def/ghi", "abc*/ghi")]
        [InlineData("abc/def/ghi", "abc/*/ghi")]
        [InlineData("abc/def/ghi", "*/ghi")]
        [InlineData("aabbccaabbddaabbee", "a*b*a*ee")]
        public void MultiCharacterWildcard_WithMatch_ReturnsTrue(string str, string pattern)
        {
            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("aa", "a*a")]
        [InlineData("abc", "a*bc")]
        [InlineData("abc", "*abc")]
        [InlineData("abc", "abc*")]
        [InlineData("abc", "*a*")]
        [InlineData("abc", "*b*")]
        [InlineData("abc", "*c*")]
        [InlineData("abc", "a*bc*de")]
        [InlineData("abcde", "a*b*c*d*e")]
        public void MultiCharacterWildcard_WithBlank_ReturnsTrue(string str, string pattern)
        {
            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("abbde", "a*cde")]
        [InlineData("bbcde", "a*cde")]
        [InlineData("bacde", "*bcde")]
        [InlineData("bbcde", "abcd*")]
        [InlineData("aabbccaabbddaabbee", "a*b*a*e")]
        public void MultiCharacterWildcard_WithNoMatch_ReturnsFalse(string str, string pattern)
        {
            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("ab", "a?*")]
        [InlineData("abcde", "a?c*e")]
        [InlineData("abcde", "a*c?e")]
        [InlineData("abcde", "a?*de")]
        [InlineData("aabbccaabbddaabbee", "a*b?cca*b*ee")]
        public void MixedWildcard_WithMatch_ReturnsTrue(string str, string pattern)
        {
            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(" ", "?*")]
        [InlineData(" ", "*?")]
        [InlineData(" ", "*?*")]
        [InlineData("  ", "*?*?*")]
        public void MixedWildcard_WithMatchAndLengthEdgeCases_ReturnsTrue(string str, string pattern)
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

        [Theory]
        [InlineData(500)]
        [InlineData(5000)]
        [InlineData(50000)]
        [InlineData(500000)]
        public void Performance_MatchLength_PatternLengthOfInput(int matchLength)
        {
            var str = new string('a', matchLength);
            var pattern = str;

            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(500)]
        [InlineData(5000)]
        [InlineData(50000)]
        [InlineData(500000)]
        public void Performance_MatchLength_PatternLengthWithWildcard(int matchLength)
        {
            var str = new string('a', matchLength);
            var pattern = "a*a";

            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(500)]
        [InlineData(5000)]
        [InlineData(50000)]
        [InlineData(500000)]
        public void Performance_NoMatchLength_PatternLengthOf1Char(int noMatchLength)
        {
            var str = new string('a', noMatchLength);
            var pattern = new string('b', 1);

            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(500)]
        [InlineData(5000)]
        [InlineData(50000)]
        [InlineData(500000)]
        public void Performance_NoMatchLength_PatternLengthWithWildcard(int noMatchLength)
        {
            var str = new string('a', noMatchLength);
            var pattern = "b*b";

            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeFalse();
        }
    }
}
