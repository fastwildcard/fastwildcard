using System.Text.RegularExpressions;
using FastWildcard.Performance.Matchers;
using FluentAssertions;
using Xunit;

namespace FastWildcard.Performance.Tests
{
    public class MatcherTests
    {
        [Theory]
        [InlineData("abc", "a?c")]
        [InlineData("abcde", "a?c?e")]
        public void SingleCharacterWildcard_WithMatch_ReturnsTrue(string str, string pattern)
        {
            var regexMatcher = new RegexMatcher(pattern, RegexOptions.None);
#if !NETCOREAPP
            var wildcardMatchMatcher = new WildcardMatchMatcher();
#endif
            var fastWildcardMatcher = new FastWildcardMatcher();

            var regexMatcherResult = regexMatcher.Match(str);
#if !NETCOREAPP
            var wildcardMatchMatcherResult = wildcardMatchMatcher.Match(pattern, str);
#endif
            var fastWildcardMatcherResult = fastWildcardMatcher.Match(str, pattern);

            regexMatcherResult.Should().BeTrue();
#if !NETCOREAPP
            wildcardMatchMatcherResult.Should().BeTrue();
#endif
            fastWildcardMatcherResult.Should().BeTrue();
        }

        [Theory]
        [InlineData("bbcde", "a?cde")]
        [InlineData("bbcde", "abcde")]
        public void SingleCharacterWildcard_WithNoMatch_ReturnsFalse(string str, string pattern)
        {
            var regexMatcher = new RegexMatcher(pattern, RegexOptions.None);
#if !NETCOREAPP
            var wildcardMatchMatcher = new WildcardMatchMatcher();
#endif
            var fastWildcardMatcher = new FastWildcardMatcher();

            var regexMatcherResult = regexMatcher.Match(str);
#if !NETCOREAPP
            var wildcardMatchMatcherResult = wildcardMatchMatcher.Match(pattern, str);
#endif
            var fastWildcardMatcherResult = fastWildcardMatcher.Match(str, pattern);

            regexMatcherResult.Should().BeFalse();
#if !NETCOREAPP
            wildcardMatchMatcherResult.Should().BeFalse();
#endif
            fastWildcardMatcherResult.Should().BeFalse();
        }

        [Theory]
        [InlineData("abc", "a*c")]
        [InlineData("abcde", "a*e")]
        public void MultiCharacterWildcard_WithMatch_ReturnsTrue(string str, string pattern)
        {
            var regexMatcher = new RegexMatcher(pattern, RegexOptions.None);
#if !NETCOREAPP
            var wildcardMatchMatcher = new WildcardMatchMatcher();
#endif
            var fastWildcardMatcher = new FastWildcardMatcher();

            var regexMatcherResult = regexMatcher.Match(str);
#if !NETCOREAPP
            var wildcardMatchMatcherResult = wildcardMatchMatcher.Match(pattern, str);
#endif
            var fastWildcardMatcherResult = fastWildcardMatcher.Match(str, pattern);

            regexMatcherResult.Should().BeTrue();
#if !NETCOREAPP
            wildcardMatchMatcherResult.Should().BeTrue();
#endif
            fastWildcardMatcherResult.Should().BeTrue();
        }

        [Theory]
        [InlineData("abbde", "a*cde")]
        public void MultiCharacterWildcard_WithNoMatch_ReturnsFalse(string str, string pattern)
        {
            var regexMatcher = new RegexMatcher(pattern, RegexOptions.None);
#if !NETCOREAPP
            var wildcardMatchMatcher = new WildcardMatchMatcher();
#endif
            var fastWildcardMatcher = new FastWildcardMatcher();

            var regexMatcherResult = regexMatcher.Match(str);
#if !NETCOREAPP
            var wildcardMatchMatcherResult = wildcardMatchMatcher.Match(pattern, str);
#endif
            var fastWildcardMatcherResult = fastWildcardMatcher.Match(str, pattern);

            regexMatcherResult.Should().BeFalse();
#if !NETCOREAPP
            wildcardMatchMatcherResult.Should().BeFalse();
#endif
            fastWildcardMatcherResult.Should().BeFalse();
        }
    }
}
