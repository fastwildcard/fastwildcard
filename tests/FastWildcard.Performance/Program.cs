using BenchmarkDotNet.Running;
using FastWildcard.Performance.Benchmarks;

namespace FastWildcard.Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<StandardLength>();
            BenchmarkRunner.Run<CrossFramework>();
        }
    }
}
