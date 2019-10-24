using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;

namespace FastWildcard.Performance.Benchmarks.Configs
{
    public class CompareRuntimesConfig : ManualConfig
    {
        public CompareRuntimesConfig()
        {
            Add(Job.Default.With(CsProjCoreToolchain.NetCoreApp30));
            Add(Job.Default.With(CsProjCoreToolchain.NetCoreApp21));
            Add(Job.Default.With(CsProjClassicNetToolchain.From("net48")));
            Add(Job.Default.With(CsProjClassicNetToolchain.Net472));
            Add(Job.Default.With(CsProjClassicNetToolchain.Net461));
        }
    }
}
