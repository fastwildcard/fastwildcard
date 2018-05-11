using System.Management.Automation;
using System.Text.RegularExpressions;
using FastWildcard.Performance.Matchers;
using FluentAssertions;
using Xunit;

namespace FastWildcard.Tests
{
    public class MatcherTests
    {
        [Theory]
        [InlineData("a?c", "abc")]
        [InlineData("a?c?e", "abcde")]
        [InlineData("a???e", "abcde")]
        [InlineData("?bcde", "abcde")]
        [InlineData("abcd?", "abcde")]
        public void SingleCharacterWildcard_WithMatch_ReturnsTrue(string pattern, string str)
        {
            var regexMatcher = new RegexMatcher(pattern, RegexOptions.None);
            var wildcardMatchMatcher = new WildcardMatchMatcher();
            var fastWildcardMatcher = new FastWildcardMatcher();
            var automationWildcardMatcher = new AutomationWildcardMatcher(pattern, WildcardOptions.None);

            var regexMatcherResult = regexMatcher.Match(str);
            var wildcardMatchMatcherResult = wildcardMatchMatcher.Match(pattern, str);
            var fastWildcardMatcherResult = fastWildcardMatcher.Match(str, pattern);
            var automationWildcardMatcherResult = automationWildcardMatcher.Match(str);


            regexMatcherResult.Should().BeTrue();
            wildcardMatchMatcherResult.Should().BeTrue();
            fastWildcardMatcherResult.Should().BeTrue();
            automationWildcardMatcherResult.Should().BeTrue();
        }

        [Theory]
        [InlineData("a?cde", "abcdef")]
        [InlineData("a?cde", "bbcde")]
        [InlineData("?bcde", "bacde")]
        [InlineData("abcd?", "bbcde")]
        public void SingleCharacterWildcard_WithNoMatch_ReturnsFalse(string pattern, string str)
        {
            var regexMatcher = new RegexMatcher(pattern, RegexOptions.None);
            var wildcardMatchMatcher = new WildcardMatchMatcher();
            var fastWildcardMatcher = new FastWildcardMatcher();
            var automationWildcardMatcher = new AutomationWildcardMatcher(pattern, WildcardOptions.None);

            var regexMatcherResult = regexMatcher.Match(str);
            var wildcardMatchMatcherResult = wildcardMatchMatcher.Match(pattern, str);
            var fastWildcardMatcherResult = fastWildcardMatcher.Match(str, pattern);
            var automationWildcardMatcherResult = automationWildcardMatcher.Match(str);

            regexMatcherResult.Should().BeFalse();
            wildcardMatchMatcherResult.Should().BeFalse();
            fastWildcardMatcherResult.Should().BeFalse();
            automationWildcardMatcherResult.Should().BeFalse();
        }

        [Theory]
        [InlineData("a*c", "abc")]
        [InlineData("a*e", "abcde")]
        [InlineData("*bcde", "abcde")]
        [InlineData("abcd*", "abcde")]
        public void MultiCharacterWildcard_WithMatch_ReturnsTrue(string pattern, string str)
        {
            var regexMatcher = new RegexMatcher(pattern, RegexOptions.None);
            var wildcardMatchMatcher = new WildcardMatchMatcher();
            var fastWildcardMatcher = new FastWildcardMatcher();
            var automationWildcardMatcher = new AutomationWildcardMatcher(pattern, WildcardOptions.None);

            var regexMatcherResult = regexMatcher.Match(str);
            var wildcardMatchMatcherResult = wildcardMatchMatcher.Match(pattern, str);
            var fastWildcardMatcherResult = fastWildcardMatcher.Match(str, pattern);
            var automationWildcardMatcherResult = automationWildcardMatcher.Match(str);

            regexMatcherResult.Should().BeTrue();
            wildcardMatchMatcherResult.Should().BeTrue();
            fastWildcardMatcherResult.Should().BeTrue();
            automationWildcardMatcherResult.Should().BeTrue();
        }

        [Theory]
        [InlineData("a*cde", "abbde")]
        [InlineData("a*cde", "bbcde")]
        [InlineData("*bcde", "bacde")]
        [InlineData("abcd*", "bbcde")]
        public void MultiCharacterWildcard_WithNoMatch_ReturnsFalse(string pattern, string str)
        {
            var regexMatcher = new RegexMatcher(pattern, RegexOptions.None);
            var wildcardMatchMatcher = new WildcardMatchMatcher();
            var fastWildcardMatcher = new FastWildcardMatcher();
            var automationWildcardMatcher = new AutomationWildcardMatcher(pattern, WildcardOptions.None);

            var regexMatcherResult = regexMatcher.Match(str);
            var wildcardMatchMatcherResult = wildcardMatchMatcher.Match(pattern, str);
            var fastWildcardMatcherResult = fastWildcardMatcher.Match(str, pattern);
            var automationWildcardMatcherResult = automationWildcardMatcher.Match(str);

            regexMatcherResult.Should().BeFalse();
            wildcardMatchMatcherResult.Should().BeFalse();
            fastWildcardMatcherResult.Should().BeFalse();
            automationWildcardMatcherResult.Should().BeFalse();
        }
    }
}
