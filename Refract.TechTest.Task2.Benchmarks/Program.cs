using BenchmarkDotNet.Running;

namespace Refract.TechTest.Task2.Benchmarks
{
    public static class Program
    {

        public static void Main()
        {
            BenchmarkRunner.Run<Benchmarks>();
        }

    }
}
