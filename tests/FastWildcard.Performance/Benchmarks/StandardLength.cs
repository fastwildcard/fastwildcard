using System;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using FastWildcard.Performance.Matchers;

namespace FastWildcard.Performance.Benchmarks
{
    [ClrJob]
    public class StandardLength
    {
        private const int StringLength = 25;
        private const int SingleCharacterMatchCount = 2;
        private const int SingleCharacterStart = 0;
        private const int SingleCharacterEnd = 10;
        private const int MultiCharacterMatchCount = 2;
        private const int MultiCharacterStart = 12;
        private const int MultiCharacterEnd = 24;
        private readonly string _pattern;
        private readonly string _str;
        private readonly RegexMatcher _regexMatcher;
        private readonly RegexMatcher _regexMatcherCompiled;
        private readonly WildcardMatchMatcher _wildcardMatchMatcher;
        private readonly AutomationWildcardMatcher _automationWildcardMatcher;
        private readonly AutomationWildcardMatcher _automationWildcardMatcherCompiled;
        private readonly FastWildcardMatcher _fastWildcardMatcher;

        public StandardLength()
        {
            var patternBuilder = new StringBuilder(new Bogus.Randomizer().AlphaNumeric(StringLength));

            var random = new Random();
            
            var singleCharacterLocations = Enumerable.Range(0, SingleCharacterMatchCount)
                .Select(x => random.Next(SingleCharacterStart, SingleCharacterEnd))
                .ToList();
            singleCharacterLocations.ForEach(x => patternBuilder[x] = '?');

            var multiCharacterLocations = Enumerable.Range(0, MultiCharacterMatchCount)
                .Select(x => random.Next(MultiCharacterStart, MultiCharacterEnd))
                .ToList();
            multiCharacterLocations.ForEach(x => patternBuilder[x] = '*');

            _pattern = patternBuilder.ToString();
            _regexMatcher = new RegexMatcher(_pattern, RegexOptions.None);
            _regexMatcherCompiled = new RegexMatcher(_pattern, RegexOptions.Compiled);
            _wildcardMatchMatcher = new WildcardMatchMatcher();
            _automationWildcardMatcher = new AutomationWildcardMatcher(_pattern, WildcardOptions.None);
            _automationWildcardMatcherCompiled = new AutomationWildcardMatcher(_pattern, WildcardOptions.Compiled);
            _fastWildcardMatcher = new FastWildcardMatcher();

            _str = new Bogus.Randomizer().AlphaNumeric(StringLength);
        }

        [Benchmark]
        public bool Regex() => _regexMatcher.Match(_str);

        [Benchmark]
        public bool RegexCompiled() => _regexMatcherCompiled.Match(_str);

        [Benchmark]
        public bool WildcardMatch() => _wildcardMatchMatcher.Match(_pattern, _str);

        [Benchmark]
        public bool AutomationWildcardPattern() => _automationWildcardMatcher.Match(_str);

        [Benchmark]
        public bool AutomationWildcardPatternCompiled() => _automationWildcardMatcherCompiled.Match(_str);

        [Benchmark]
        public bool FastWildcard() => _fastWildcardMatcher.Match(_pattern, _str);
    }
}
