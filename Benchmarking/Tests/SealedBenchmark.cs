using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarking.Tests
{
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class SealedBenchmark
    {
        private readonly Bear _bear = new();
        private readonly Husky _husky = new();

        [Benchmark]
        public void Sealed_VoidMethod() => _husky.DoNothing();
        [Benchmark]
        public void Open_VoidMethod() => _bear.DoNothing();
    }

    public class Animal
    {
        public virtual void DoNothing() { }
        public virtual int GetAge() => -1;
        public static string Walk() => "I'm walking..";
    }

    public class Bear : Animal
    {
        public override void DoNothing() { }
        public override int GetAge() => 4;
    }

    public sealed class Husky : Animal
    {
        public override void DoNothing() { }
        public override int GetAge() => 11;
    }
}