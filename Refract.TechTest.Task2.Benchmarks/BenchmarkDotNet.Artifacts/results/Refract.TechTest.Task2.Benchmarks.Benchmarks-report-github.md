``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.201
  [Host]     : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  DefaultJob : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT


```
|             Method |     Mean |   Error |  StdDev |      Min |      Max | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------- |---------:|--------:|--------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
|       TaskWithLoop | 242.2 ns | 4.81 ns | 7.20 ns | 230.1 ns | 258.8 ns |  1.00 |    0.00 | 0.0362 |     - |     - |     152 B |
| TaskWithEnumerable | 234.6 ns | 2.00 ns | 1.67 ns | 232.2 ns | 237.6 ns |  0.96 |    0.04 | 0.0362 |     - |     - |     152 B |
|       TaskWithGoto | 232.7 ns | 4.67 ns | 6.07 ns | 223.6 ns | 243.5 ns |  0.96 |    0.03 | 0.0362 |     - |     - |     152 B |
|  TaskWithRecursion | 384.7 ns | 6.66 ns | 5.91 ns | 374.0 ns | 396.3 ns |  1.58 |    0.07 | 0.0877 |     - |     - |     368 B |
