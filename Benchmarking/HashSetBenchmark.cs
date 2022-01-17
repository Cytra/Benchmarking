using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Benchmarking.Enum;

namespace Benchmarking
{
    [MemoryDiagnoser]
    public class HashSetBenchmark
    {
        private readonly InstallmentType[] _swedishFeeList =
        {
            InstallmentType.DeferredInterestApplied,
            InstallmentType.FeeApplied,
            InstallmentType.InterestApplied,
            InstallmentType.PenaltyApplied,
            InstallmentType.BranchChanged,
            InstallmentType.DueDateChanged,
            InstallmentType.DueDateChangedAdjustment,
            InstallmentType.FeeCharged,
            InstallmentType.InterestRateChanged,
            InstallmentType.PenaltyRateChanged,
            InstallmentType.TaxRateChanged,
            InstallmentType.TermsChanged,
        };

        private readonly HashSet<InstallmentType> _swedishFeeHashSet = new HashSet<InstallmentType>()
        {
            InstallmentType.DeferredInterestApplied,
            InstallmentType.FeeApplied,
            InstallmentType.InterestApplied,
            InstallmentType.PenaltyApplied,
            InstallmentType.BranchChanged,
            InstallmentType.DueDateChanged,
            InstallmentType.DueDateChangedAdjustment,
            InstallmentType.FeeCharged,
            InstallmentType.InterestRateChanged,
            InstallmentType.PenaltyRateChanged,
            InstallmentType.TaxRateChanged,
            InstallmentType.TermsChanged,
        };

        [Benchmark]
        public bool ListContains()
        {
            return _swedishFeeList.Contains(InstallmentType.AccountTerminated);
        }

        [Benchmark]
        public bool HashSetContains()
        {
            return _swedishFeeHashSet.Contains(InstallmentType.AccountTerminated);
        }
    }
}
