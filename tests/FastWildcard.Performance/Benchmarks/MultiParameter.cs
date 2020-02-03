using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using FastWildcard.Performance.Matchers;
using FastWildcard.Tests;

namespace FastWildcard.Performance.Benchmarks
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    public class MultiParameter
    {
        [Params(10, 100, 500, 1000)]
        public int PatternLength { get; set; }

        [Params(0, 3, 20, 50)]
        public int SingleCharacterCount { get; set; }

        [Params(0, 1, 5, 10)]
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
