using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;
using FastWildcard.Performance.Matchers;

namespace FastWildcard.Performance.Benchmarks
{
    public class MultipleRuntimes : ManualConfig
    {
        private const int InvocationCount = 1_000_000;

        public MultipleRuntimes()
        {
            Add(Job.Default.With(CsProjCoreToolchain.NetCoreApp22).WithInvocationCount(InvocationCount));
            Add(Job.Default.With(CsProjCoreToolchain.NetCoreApp21).WithInvocationCount(InvocationCount));
            Add(Job.Default.With(CsProjCoreToolchain.NetCoreApp20).WithInvocationCount(InvocationCount));
            Add(Job.Default.With(CsProjClassicNetToolchain.Net472).WithInvocationCount(InvocationCount));
            Add(Job.Default.With(CsProjClassicNetToolchain.Net461).WithInvocationCount(InvocationCount));

            Add(MemoryDiagnoser.Default);
        }
    }
    
    [Config(typeof(MultipleRuntimes))]
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

        [IterationSetup]
        public void IterationSetup()
        {
            (_pattern, _, _) = IterationBuilder.BuildPattern(PatternLength, SingleCharacterCount, MultiCharacterCount);

            _str = IterationBuilder.BuildTestString(_pattern);

            _fastWildcardMatcher = new FastWildcardMatcher();
        }

        [Benchmark]
        public bool FastWildcard() => _fastWildcardMatcher.Match(_str, _pattern);
    }
}
