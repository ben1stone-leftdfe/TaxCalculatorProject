using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using Tax_Calculator.Domain;
using Tax_Calculator.Service.Calculators;
using Tax_Calculator.Service.Validators;
using Tax_Calculator.Web.Controllers;
using Tax_Calculator.Web.ViewModels;

namespace Tax_Calculator.Web.UnitTests.Controllers
{
    public class GoodCalculatorActionTests
    {
        private IConfigService Config;
        private IGrossPayValidator GrossPayValidator;
        private IGoodCalculator GoodCalculator;

        private HomeController Controller;

        [SetUp]
        public void Setup()
        {
            Config = Substitute.For<IConfigService>();
            GrossPayValidator = Substitute.For<IGrossPayValidator>();
            GoodCalculator = Substitute.For<IGoodCalculator>();

            Config.GetTaxBands().Returns(GetTestTaxBands());
            Config.GetNatInsuranceBands().Returns(GetTestNIBands());

            GrossPayValidator.IsValid(GetValidInput().GrossPayInput).Returns(GetValidGrossPayResponse());
            GrossPayValidator.IsValid(GetInvalidInput().GrossPayInput).Returns(GetInvalidGrossPayResponse());

            GoodCalculator.Calculate(Arg.Any<double>(), Arg.Any<List<TaxBand>>(), Arg.Any<List<TaxBand>>());

            Controller = new HomeController(Config, GrossPayValidator, GoodCalculator);
        }

        [Test]
        public void WhenAnInvalidInputIsEntered_ThenTheGoodCalculatorCalculateMethodWillNotBeCalled()
        {
            var result = Controller.GoodCalculator(GetInvalidInput());

            GoodCalculator.DidNotReceive().Calculate(Arg.Any<double>(), Arg.Any<List<TaxBand>>(), Arg.Any<List<TaxBand>>());
        }

        [Test]
        public void WhenAnInvalidInputIsEntered_ThenTheTaxAndNIBandsWillNotBeLoaded()
        {
            var result = Controller.GoodCalculator(GetInvalidInput());

            Config.DidNotReceive().GetTaxBands();
            Config.DidNotReceive().GetNatInsuranceBands();
        }

        [Test]
        public void WhenAValidInputIsEntered_ThenTheGoodCalculatorCalculateMethodWillNotBeCalled()
        {
            var result = Controller.GoodCalculator(GetValidInput());

            GoodCalculator.Received(1).Calculate(Arg.Any<double>(), Arg.Any<List<TaxBand>>(), Arg.Any<List<TaxBand>>());
        }

        [Test]
        public void WhenAValidInputIsEntered_ThenTheTaxAndNIBandsWillBeLoaded()
        {
            var result = Controller.GoodCalculator(GetValidInput());

            Config.Received(1).GetTaxBands();
            Config.Received(1).GetNatInsuranceBands();
        }

        private List<TaxBand> GetTestNIBands()
        {
            return new List<TaxBand>
            {
                new TaxBand { Band = 183, PercentageDeduction = 12},
                new TaxBand { Band = 962, PercentageDeduction = 2}
            };
        }

        private List<TaxBand> GetTestTaxBands()
        {
            return new List<TaxBand>
            {
                new TaxBand { Band = 12500, PercentageDeduction = 20},
                new TaxBand { Band = 50000, PercentageDeduction = 40}
            };
        }

        private CalculatorViewModel GetInvalidInput()
        {
            return new CalculatorViewModel
            {
                GrossPayInput = "-1000"
            };
        }

        private CalculatorViewModel GetValidInput()
        {
            return new CalculatorViewModel
            {
                GrossPayInput = "20000"
            };
        }

        private GrossPayValidatorResponse GetValidGrossPayResponse()
        {
            return new GrossPayValidatorResponse(10000, new List<string>(), true);
        }
        private GrossPayValidatorResponse GetInvalidGrossPayResponse()
        {
            return new GrossPayValidatorResponse(-10000, new List<string>(), false);
        }

    }
}
