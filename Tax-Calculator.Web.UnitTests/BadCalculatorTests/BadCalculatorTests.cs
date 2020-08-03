using NUnit.Framework;
using System;
using Tax_Calculator.Web.Calculators;

namespace Tax_Calculator.Web.UnitTests.BadCalculatorTests
{
    public class BadCalculatorTests
    {
        [TestCase("100", 100)]
        [TestCase("20000", 17241.92)]
        [TestCase("60000", 43439.52)]
        public void WhenGettingNetPayUsing_ThenTheCorrectResultIsReturned(string test, double expected)
        {
            var BadCalculator = new BadCalculator();

            var result = BadCalculator.GetNetPay(test);

            Assert.AreEqual(Math.Round(expected, 2), Math.Round(result, 2));
        }

        [Test]
        public void WhenANegativeNumberIsSupplied_ThenArgumentExceptionIsThrown()
        {
            var BadCalculator = new BadCalculator();

            var ex = Assert.Throws(typeof(ArgumentException), new TestDelegate(() => BadCalculator.GetNetPay("-100")));

            Assert.AreEqual("Enter a positive number", ex.Message);
        }

        [Test]
        public void WhenAnInvalidStringIsSupplied_ThenArgumentExceptionIsThrown()
        {
            var BadCalculator = new BadCalculator();

            var ex = Assert.Throws(typeof(ArgumentException), new TestDelegate(() => BadCalculator.GetNetPay("Calculate my tax")));

            Assert.AreEqual("Enter a number", ex.Message);
        }
    }
}
