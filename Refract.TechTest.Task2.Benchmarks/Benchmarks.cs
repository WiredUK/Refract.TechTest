using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using Refract.TechTest.Task2;

namespace Refract.TechTest.Task2.Benchmarks
{
    [HtmlExporter]
    [MinColumn, MaxColumn]
    [MemoryDiagnoser]
    public class Benchmarks
    {
        private const int MaxN = 10;
        private readonly NumberGenerator _numberGenerator = new();

        [Benchmark(Baseline = true)]
        public void TaskWithLoop()
        {
            var numbers = _numberGenerator.GetNumbersWithLoop(MaxN);

            OutputResult(numbers);
        }

        [Benchmark]
        public void TaskWithEnumerable()
        {
            var numbers = _numberGenerator.GetNumbersUsingEnumerable(MaxN);

            OutputResult(numbers);
        }

        [Benchmark]
        public void TaskWithGoto()
        {
            var numbers = _numberGenerator.GetNumbersWithGoto(MaxN);

            OutputResult(numbers);
        }

        [Benchmark]
        public void TaskWithRecursion()
        {
            var numbers = _numberGenerator.GetNumbersUsingRecursion(MaxN);

            OutputResult(numbers);
        }

        private static void OutputResult(IEnumerable<int> numbers)
        {
            // This is only needed to ensure we are executing the enumerable
            var _ = string.Join(", ", numbers);
        }
    }
}