``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19041.1083 (2004/May2020Update/20H1)
11th Gen Intel Core i7-1165G7 2.80GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=5.0.203
  [Host]     : .NET 5.0.6 (5.0.621.22011), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET 5.0.6 (5.0.621.22011), X64 RyuJIT


```
| Method |      Mean |     Error |    StdDev |
|------- |----------:|----------:|----------:|
| Sha256 |  5.809 μs | 0.0545 μs | 0.0455 μs |
|    Md5 | 16.864 μs | 0.1265 μs | 0.1184 μs |
