using System;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using FastWildcard.Performance.Matchers;

namespace FastWildcard.Performance.Benchmarks
{
    [SimpleJob(RuntimeMoniker.Net461)]
    [MemoryDiagnoser]
    public class LibraryComparison
    {
        [Params(10, 50)]
        public int PatternLength { get; set; }

        [Params(2, 5)]
        public int SingleCharacterCount { get; set; }

        [Params(1, 3)]
        public int MultiCharacterCount { get; set; }

        private string _pattern;
        private string _str;
        private FastWildcardMatcher _fastWildcardMatcher;
        private RegexMatcher _regexMatcher;
        private RegexMatcher _regexMatcherCompiled;
        private WildcardMatchMatcher _wildcardMatchMatcher;

        [GlobalSetup]
        public void GlobalSetup()
        {
            (_pattern, _, _) = IterationBuilder.BuildPattern(PatternLength, SingleCharacterCount, MultiCharacterCount);

            _str = IterationBuilder.BuildTestString(_pattern);

            _fastWildcardMatcher = new FastWildcardMatcher();
            _regexMatcher = new RegexMatcher(_pattern, RegexOptions.None);
            _regexMatcherCompiled = new RegexMatcher(_pattern, RegexOptions.Compiled);
            _wildcardMatchMatcher = new WildcardMatchMatcher();
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
