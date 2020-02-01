﻿using System;
using FluentAssertions;
using Xunit;

namespace FastWildcard.Tests
{
    public class IsMatchTests
    {
        public bool DoMatch(string str, string pattern) =>
            FastWildcard.IsMatch(str, pattern);
            //System.Text.RegularExpressions.Regex.IsMatch(str, "^" + System.Text.RegularExpressions.Regex.Escape(pattern).Replace(@"\*", ".*").Replace(@"\?", ".") + "$");

        [Theory]
        [InlineData(null, "a?c")]
        public void SingleCharacterWildcard_WithNullStrInput_ReturnsFalse(string str, string pattern)
        {
            var resultAction = new Action(() => DoMatch(str, pattern));

            resultAction.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("abcde", null)]
        [InlineData("abcde", "")]
        public void SingleCharacterWildcard_WithInvalidPatternInputs_ThrowsException(string str, string pattern)
        {
            var resultAction = new Action(() => DoMatch(str, pattern));

            resultAction.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData("ab", "a?c")]
        [InlineData("abc", "a?")]
        [InlineData("abc", " ")]
        public void SingleCharacterWildcard_WithMatchAndLengthEdgeCases_ReturnsFalse(string str, string pattern)
        {
            var result = DoMatch(str, pattern);

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
            var result = DoMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("bbcde", "a?cde")]
        [InlineData("bacde", "?bcde")]
        [InlineData("bbcde", "abcd?")]
        public void SingleCharacterWildcard_WithNoMatch_ReturnsFalse(string str, string pattern)
        {
            var result = DoMatch(str, pattern);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("", "*")]
        [InlineData(" ", "*")]
        [InlineData(" ", "**")]
        [InlineData(" ", "***")]
        public void MultiCharacterWildcard_WithMatchAndLengthEdgeCases_ReturnsTrue(string str, string pattern)
        {
            var result = DoMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
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
            var result = DoMatch(str, pattern);

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
        [InlineData("abcde", "a*b*c*d*e")]
        public void MultiCharacterWildcard_WithBlank_ReturnsTrue(string str, string pattern)
        {
            var result = DoMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("abbde", "a*cde")]
        [InlineData("bbcde", "a*cde")]
        [InlineData("bacde", "*bcde")]
        [InlineData("bbcde", "abcd*")]
        [InlineData("abc", "a*bc*de")]
        [InlineData("aabbccaabbddaabbee", "a*b*a*e")]
        public void MultiCharacterWildcard_WithNoMatch_ReturnsFalse(string str, string pattern)
        {
            var result = DoMatch(str, pattern);

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
            var result = DoMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(" ", "?*")]
        [InlineData(" ", "*?")]
        [InlineData(" ", "*?*")]
        [InlineData("  ", "*?*?*")]
        public void MixedWildcard_WithMatchAndLengthEdgeCases_ReturnsTrue(string str, string pattern)
        {
            var result = DoMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("abcde", "abcde")]
        [InlineData(" ", " ")]
        public void NoWildcard_WithMatch_ReturnsTrue(string str, string pattern)
        {
            var result = DoMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("abbde", "abcde")]
        [InlineData(" ", "  ")]
        [InlineData("  ", " ")]
        public void NoWildcard_WithNoMatch_ReturnsFalse(string str, string pattern)
        {
            var result = DoMatch(str, pattern);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("aaa", "a*a")]  // Should the * be taking 0 or 1 character here?
        public void Unsupported(string str, string pattern)
        {
            var result = DoMatch(str, pattern);

            result.Should().BeFalse();
        }
    }
}
