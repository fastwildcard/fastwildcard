using System;
using System.Linq;
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
        private string _pattern;
        private string _str;
        private FastWildcardMatcher _fastWildcardMatcher;
        private RegexMatcher _regexMatcher;
        private RegexMatcher _regexMatcherCompiled;
        private WildcardMatchMatcher _wildcardMatchMatcher;

        [IterationSetup]
        public void IterationSetup()
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

            _fastWildcardMatcher = new FastWildcardMatcher();
            _regexMatcher = new RegexMatcher(_pattern, RegexOptions.None);
            _regexMatcherCompiled = new RegexMatcher(_pattern, RegexOptions.Compiled);
            _wildcardMatchMatcher = new WildcardMatchMatcher();

            _str = new Bogus.Randomizer().AlphaNumeric(StringLength);
        }

        [Benchmark]
        public bool FastWildcard() => _fastWildcardMatcher.Match(_str, _pattern);

        [Benchmark]
        public bool Regex() => _regexMatcher.Match(_str);

        [Benchmark]
        public bool RegexCompiled() => _regexMatcherCompiled.Match(_str);

        [Benchmark]
        public bool WildcardMatch() => _wildcardMatchMatcher.Match(_pattern, _str);
    }
}
