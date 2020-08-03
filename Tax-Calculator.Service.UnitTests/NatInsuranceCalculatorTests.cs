using NUnit.Framework;
using System;
using System.Collections.Generic;
using Tax_Calculator.Domain;
using Tax_Calculator.Service.Calculators;

namespace Tax_Calculator.Service.UnitTests
{
    public class NatInsuranceCalculatorTests
    {
        private List<TaxBand> NatInsuranceBands;

        [SetUp]
        public void Setup()
        {
            NatInsuranceBands = new List<TaxBand>()
            {
                new TaxBand { Band = 183, PercentageDeduction = 12},
                new TaxBand { Band = 962, PercentageDeduction = 2}
            };
        }

        [TestCase(9516, 0)]
        [TestCase(20000, 1258.08)]
        [TestCase(60000, 5060.48)]
        public void Test1(double test, double expected)
        {
            var NatInsuranceCalculator = new NatInsuranceCalculator(NatInsuranceBands);

            var result = NatInsuranceCalculator.CalculateDeduction(test);

            Assert.AreEqual(Math.Round(expected, 2), Math.Round(result, 2));
        }
    }
}
