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
        [InlineData("abc", "a?c")]
        [InlineData("abcde", "a?c?e")]
        public void SingleCharacterWildcard_WithMatch_ReturnsTrue(string str, string pattern)
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
        [InlineData("bbcde", "a?cde")]
        [InlineData("bbcde", "abcde")]
        public void SingleCharacterWildcard_WithNoMatch_ReturnsFalse(string str, string pattern)
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
        [InlineData("abc", "a*c")]
        [InlineData("abcde", "a*e")]
        public void MultiCharacterWildcard_WithMatch_ReturnsTrue(string str, string pattern)
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
        [InlineData("abbde", "a*cde")]
        public void MultiCharacterWildcard_WithNoMatch_ReturnsFalse(string str, string pattern)
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
