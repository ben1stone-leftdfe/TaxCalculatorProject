using NUnit.Framework;
using System.Collections.Generic;
using Tax_Calculator.Domain;
using Tax_Calculator.Service.Calculators;

namespace Tax_Calculator.Service.UnitTests
{
    public class TaxCalculatorTests
    {
        private List<TaxBand> TaxBands;

        [SetUp]
        public void Setup()
        {
            TaxBands = new List<TaxBand>()
            {
                new TaxBand { Band = 12500, PercentageDeduction = 20},
                new TaxBand { Band = 50000, PercentageDeduction = 40}
            };
        }

        [TestCase(100, 0)]
        [TestCase(20000, 1500)]
        [TestCase(60000, 11500)]
        public void WhenCalculatingDeductionsUsingTaxBandsAndATestValue_ThenTheCorrectDeductionIsReturned(double test, double expected)
        {
            var TaxCalculator = new TaxCalculator(TaxBands);

            var result = TaxCalculator.CalculateDeduction(test);

            Assert.AreEqual(expected, result);
        }
    }
}