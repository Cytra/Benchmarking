using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Benchmarking.Enum;

namespace Benchmarking.Tests
{
    [MemoryDiagnoser]
    public class HashSetBenchmark
    {
        private readonly PokerHand[] _pokerHands =
        {
            PokerHand.Flush,
            PokerHand.FourOfAKind,
            PokerHand.HighCard,
            PokerHand.Pair,
        };

        [Benchmark]
        public bool ListContains()
        {
            return _pokerHands.Contains(PokerHand.HighCard);
        }

        private readonly HashSet<PokerHand> _pokerHandsHashSet = new ()
        {
            PokerHand.Flush,
            PokerHand.FourOfAKind,
            PokerHand.HighCard,
            PokerHand.Pair,
        };

        [Benchmark]
        public bool HashSetContains()
        {
            return _pokerHandsHashSet.Contains(PokerHand.HighCard);
        }
    }
}
