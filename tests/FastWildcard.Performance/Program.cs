using BenchmarkDotNet.Running;
using FastWildcard.Performance.Benchmarks;

namespace FastWildcard.Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<LibraryComparison>();
            BenchmarkRunner.Run<MultiParameter>();
            BenchmarkRunner.Run<PlatformComparison>();
        }
    }
}
