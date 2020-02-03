using System;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using FastWildcard.Performance.Matchers;
using FastWildcard.Tests;

namespace FastWildcard.Performance.Benchmarks
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
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
#if !NETCOREAPP2_1
        private LikeMatcher _likeMatcher;
#endif
#if NETCOREAPP
        private PowerShellMatcher _powerShellMatcher;
        private PowerShellMatcher _powerShellMatcherCompiled;
#endif
        private RegexMatcher _regexMatcher;
        private RegexMatcher _regexMatcherCompiled;
        private WildcardMatchMatcher _wildcardMatchMatcher;

        [GlobalSetup]
        public void GlobalSetup()
        {
            (_pattern, _, _) = IterationBuilder.BuildPattern(PatternLength, SingleCharacterCount, MultiCharacterCount);

            _str = IterationBuilder.BuildTestString(_pattern);

            _fastWildcardMatcher = new FastWildcardMatcher();
#if !NETCOREAPP2_1
            _likeMatcher = new LikeMatcher();
#endif
#if NETCOREAPP
            _powerShellMatcher = new PowerShellMatcher(_pattern, System.Management.Automation.WildcardOptions.None);
            _powerShellMatcherCompiled = new PowerShellMatcher(_pattern, System.Management.Automation.WildcardOptions.Compiled);
#endif
            _regexMatcher = new RegexMatcher(_pattern, RegexOptions.None);
            _regexMatcherCompiled = new RegexMatcher(_pattern, RegexOptions.Compiled);
            _wildcardMatchMatcher = new WildcardMatchMatcher();
        }

        [Benchmark]
        public bool FastWildcard() => _fastWildcardMatcher.Match(_str, _pattern);

#if !NETCOREAPP2_1
        [Benchmark]
        public bool VisualBasicLike() => _likeMatcher.Match(_str, _pattern);
#endif

#if NETCOREAPP
        [Benchmark]
        public bool PowerShell() => _powerShellMatcher.Match(_str);
#endif

#if NETCOREAPP
        [Benchmark]
        public bool PowerShellCompiled() => _powerShellMatcherCompiled.Match(_str);
#endif

        [Benchmark]
        public bool Regex() => _regexMatcher.Match(_str);

        [Benchmark]
        public bool RegexCompiled() => _regexMatcherCompiled.Match(_str);

        [Benchmark]
        public bool WildcardMatch() => _wildcardMatchMatcher.Match(_pattern, _str);
    }
}
