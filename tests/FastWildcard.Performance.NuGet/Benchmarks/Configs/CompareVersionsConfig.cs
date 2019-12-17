using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;

namespace FastWildcard.Performance.NuGet.Benchmarks.Configs
{
    public class CompareVersionsConfig : ManualConfig
    {
        public CompareVersionsConfig()
        {
            const string nugetPackageName = "FastWildcard";
            string[] nugetPackageVersions = {"2.0.1", "3.0.0", "3.1.0"};

            foreach (var version in nugetPackageVersions)
            foreach (var toolchain in new[]
            {
                CsProjClassicNetToolchain.Net461,
                CsProjCoreToolchain.NetCoreApp21,
                CsProjCoreToolchain.NetCoreApp31
            })
            {
                Add(Job.MediumRun
                    .With(toolchain)
                    .WithNuGet(nugetPackageName, version));
            }
        }
    }
}
