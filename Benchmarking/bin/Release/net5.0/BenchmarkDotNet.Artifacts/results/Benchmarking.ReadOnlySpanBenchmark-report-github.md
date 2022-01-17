``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19041.1415 (2004/May2020Update/20H1)
11th Gen Intel Core i7-1165G7 2.80GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 5.0.11 (5.0.1121.47308), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET 5.0.11 (5.0.1121.47308), X64 RyuJIT


```
|                        Method |      Mean |    Error |    StdDev |   Median |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------ |----------:|---------:|----------:|---------:|-------:|------:|------:|----------:|
|             GetDateFromString | 100.50 ns | 3.911 ns | 11.283 ns | 96.36 ns | 0.0153 |     - |     - |      96 B |
| GetDateFromStringReadOnlySpan |  62.23 ns | 3.333 ns |  9.826 ns | 58.55 ns |      - |     - |     - |         - |
