using System;
using BenchmarkDotNet.Attributes;
using FastWildcard.Performance.Benchmarks.Configs;
using FastWildcard.Performance.Matchers;

namespace FastWildcard.Performance.Benchmarks
{
    [Config(typeof(CompareRuntimesConfig))]
    [MemoryDiagnoser]
    public class RuntimeComparison
    {
        [Params(100)]
        public int PatternLength { get; set; }

        [Params(20)]
        public int SingleCharacterCount { get; set; }

        [Params(5)]
        public int MultiCharacterCount { get; set; }

        private string _pattern;
        private string _str;
        private FastWildcardMatcher _fastWildcardMatcher;

        [GlobalSetup]
        public void GlobalSetup()
        {
            (_pattern, _, _) = IterationBuilder.BuildPattern(PatternLength, SingleCharacterCount, MultiCharacterCount);

            _str = IterationBuilder.BuildTestString(_pattern);

            _fastWildcardMatcher = new FastWildcardMatcher();
        }

        [Benchmark]
        public bool FastWildcard() => _fastWildcardMatcher.Match(_str, _pattern);
    }
}
