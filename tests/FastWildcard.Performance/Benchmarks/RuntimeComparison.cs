using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using FastWildcard.Performance.Matchers;

namespace FastWildcard.Performance.Benchmarks
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(RuntimeMoniker.NetCoreApp21)]
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.Net472)]
    [SimpleJob(RuntimeMoniker.Net461)]
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
