using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using FastWildcard.Performance.Matchers;

namespace FastWildcard.Performance.Benchmarks
{
    [ClrJob]
    public class LibraryComparison
    {
        [Params(10, 500)]
        public int PatternLength { get; set; }

        [Params(0, 100)]
        public int SingleCharacterCount { get; set; }

        [Params(0, 20)]
        public int MultiCharacterCount { get; set; }

        [Params(10, 1000)]
        public int StringLength { get; set; }

        private string _pattern;
        private string _str;
        private FastWildcardMatcher _fastWildcardMatcher;
        private RegexMatcher _regexMatcher;
        private RegexMatcher _regexMatcherCompiled;
#if !NETCOREAPP
        private WildcardMatchMatcher _wildcardMatchMatcher;
#endif

        [IterationSetup]
        public void IterationSetup()
        {
            var patternLength = Math.Min(PatternLength, StringLength);
            _pattern = IterationBuilder.BuildPattern(patternLength,
                Math.Min(SingleCharacterCount, patternLength),
                Math.Min(MultiCharacterCount, patternLength));

            _str = IterationBuilder.BuildTestString(StringLength);

            _fastWildcardMatcher = new FastWildcardMatcher();
            _regexMatcher = new RegexMatcher(_pattern, RegexOptions.None);
            _regexMatcherCompiled = new RegexMatcher(_pattern, RegexOptions.Compiled);
#if !NETCOREAPP
            _wildcardMatchMatcher = new WildcardMatchMatcher();
#endif
        }

        [Benchmark]
        public bool FastWildcard() => _fastWildcardMatcher.Match(_str, _pattern);

        [Benchmark]
        public bool Regex() => _regexMatcher.Match(_str);

        [Benchmark]
        public bool RegexCompiled() => _regexMatcherCompiled.Match(_str);

#if !NETCOREAPP
        [Benchmark]
        public bool WildcardMatch() => _wildcardMatchMatcher.Match(_pattern, _str);
#endif
    }
}
