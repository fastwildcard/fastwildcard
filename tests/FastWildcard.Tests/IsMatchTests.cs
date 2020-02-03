using System;
using System.ComponentModel.DataAnnotations;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace FastWildcard.Tests
{
    public class IsMatchTests
    {
        public bool DoMatch(string str, string pattern) =>

            FastWildcard.IsMatch(str, pattern);

            //System.Text.RegularExpressions.Regex.IsMatch(str, "^" + System.Text.RegularExpressions.Regex.Escape(pattern).Replace(@"\*", ".*").Replace(@"\?", ".") + "$");

#if !NETCOREAPP2_1
            //Microsoft.VisualBasic.CompilerServices.LikeOperator.LikeString(str, pattern, Microsoft.VisualBasic.CompareMethod.Text);
#else
            //FastWildcard.IsMatch(str, pattern);
#endif

#if NETCOREAPP
            //new System.Management.Automation.WildcardPattern(pattern).IsMatch(str);
#else
            //FastWildcard.IsMatch(str, pattern);
#endif

        private readonly ITestOutputHelper _output;

        public IsMatchTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [Trait("Category", "Inputs")]
        [InlineData(null, "a?c")]
        public void SingleCharacterWildcard_WithNullStrInput_ReturnsFalse(string str, string pattern)
        {
            var resultAction = new Action(() => DoMatch(str, pattern));

            resultAction.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [Trait("Category", "Inputs")]
        [InlineData("abcde", null)]
        [InlineData("abcde", "")]
        public void SingleCharacterWildcard_WithInvalidPatternInputs_ThrowsException(string str, string pattern)
        {
            var resultAction = new Action(() => DoMatch(str, pattern));

            resultAction.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [Trait("Category", "Logic")]
        [InlineData("ab", "a?c")]
        [InlineData("abc", "a?")]
        [InlineData("abc", " ")]
        public void SingleCharacterWildcard_WithMatchAndLengthEdgeCases_ReturnsFalse(string str, string pattern)
        {
            var result = DoMatch(str, pattern);

            result.Should().BeFalse();
        }

        [Theory]
        [Trait("Category", "Logic")]
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
        [Trait("Category", "Logic")]
        [InlineData("bbcde", "a?cde")]
        [InlineData("bacde", "?bcde")]
        [InlineData("bbcde", "abcd?")]
        public void SingleCharacterWildcard_WithNoMatch_ReturnsFalse(string str, string pattern)
        {
            var result = DoMatch(str, pattern);

            result.Should().BeFalse();
        }

        [Theory]
        [Trait("Category", "Logic")]
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
        [Trait("Category", "Logic")]
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
        [InlineData("aabbccaabbddaabbee", "a*b*a*e")]
        [InlineData("aabbccaabbddaabbee", "a*b*a*ee")]
        public void MultiCharacterWildcard_WithMatch_ReturnsTrue(string str, string pattern)
        {
            var result = DoMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
        [Trait("Category", "Logic")]
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
        [Trait("Category", "Logic")]
        [InlineData("abbde", "a*cde")]
        [InlineData("bbcde", "a*cde")]
        [InlineData("bacde", "*bcde")]
        [InlineData("bbcde", "abcd*")]
        [InlineData("abc", "a*bc*de")]
        public void MultiCharacterWildcard_WithNoMatch_ReturnsFalse(string str, string pattern)
        {
            var result = DoMatch(str, pattern);

            result.Should().BeFalse();
        }

        [Theory]
        [Trait("Category", "Logic")]
        [InlineData("ab", "a?*")]
        [InlineData("abcde", "a?c*e")]
        [InlineData("abcde", "a*c?e")]
        [InlineData("abcde", "a?*de")]
        [InlineData("pricing structure5vpzz6cbemxpkd9ze0ld1xManager utilisation Cambridgeshireiz2", "*5vpzz6cbemxpkd9ze0l?1x*i?2")]
        public void MixedWildcard_WithMatch_ReturnsTrue(string str, string pattern)
        {
            var result = DoMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
        [Trait("Category", "Logic")]
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
        [Trait("Category", "Logic")]
        [InlineData("abcde", "abcde")]
        [InlineData(" ", " ")]
        public void NoWildcard_WithMatch_ReturnsTrue(string str, string pattern)
        {
            var result = DoMatch(str, pattern);

            result.Should().BeTrue();
        }

        [Theory]
        [Trait("Category", "Logic")]
        [InlineData("abbde", "abcde")]
        [InlineData(" ", "  ")]
        [InlineData("  ", " ")]
        public void NoWildcard_WithNoMatch_ReturnsFalse(string str, string pattern)
        {
            var result = DoMatch(str, pattern);

            result.Should().BeFalse();
        }

        [Theory, AutoData]
        [Trait("Category", "Logic")]
        public void GeneratedPattern_ThatMatches_ReturnsTrue(
            [Range(1, 100)] int patternLength,
            [Range(0, 100)] int singleCharacterCount,
            [Range(0, 100)] int multiCharacterCount
        )
        {
            var (pattern, _, _) = IterationBuilder.BuildPattern(patternLength, singleCharacterCount, multiCharacterCount);
            _output.WriteLine(pattern);
            
            var str = IterationBuilder.BuildTestString(pattern, 0, 100);
            _output.WriteLine(str);

            var result = FastWildcard.IsMatch(str, pattern);

            result.Should().BeTrue();
        }
    }
}
