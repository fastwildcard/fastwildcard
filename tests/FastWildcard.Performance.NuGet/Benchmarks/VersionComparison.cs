using System;
using BenchmarkDotNet.Attributes;
using FastWildcard.Performance.NuGet.Benchmarks.Configs;
using FastWildcard.Performance.NuGet.Benchmarks.Matchers;
using FastWildcard.Tests;

namespace FastWildcard.Performance.NuGet.Benchmarks
{
    [Config(typeof(CompareVersionsConfig))]
    [MemoryDiagnoser]
    public class VersionComparison
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
