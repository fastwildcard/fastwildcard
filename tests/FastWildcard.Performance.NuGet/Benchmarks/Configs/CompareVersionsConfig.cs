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
            var ltsRuntime = CsProjCoreToolchain.NetCoreApp21;
            var currentRuntime = CsProjCoreToolchain.NetCoreApp22;

            foreach (var version in nugetPackageVersions)
            foreach (var toolchain in new[] {ltsRuntime, currentRuntime})
            {
                Add(Job.MediumRun
                    .With(toolchain)
                    .WithNuGet(nugetPackageName, version));
            }
        }
    }
}
