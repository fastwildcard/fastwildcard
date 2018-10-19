using BenchmarkDotNet.Running;
using FastWildcard.Performance.Benchmarks;

namespace FastWildcard.Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
