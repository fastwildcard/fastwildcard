using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using FastWildcard.Performance.Matchers;

namespace FastWildcard.Performance.Benchmarks
{
    [ClrJob]
    public class PlatformComparison
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
#if !NETCOREAPP
        private WildcardMatchMatcher _wildcardMatchMatcher;
#endif

        [IterationSetup]
        public void IterationSetup()
        {

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
