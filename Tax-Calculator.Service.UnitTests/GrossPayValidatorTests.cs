using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Tax_Calculator.Service.Validators;

namespace Tax_Calculator.Service.UnitTests
{
    public class GrossPayValidatorTests
    {
        private GrossPayValidator Validator;

        [SetUp]
        public void Setup()
        {
            Validator = new GrossPayValidator();
        }

        [Test]
        public void WhenGrossPayInputIsNegative_ThenIsValidReturnsFalse()
        {
            var result = Validator.IsValid("-1000");

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void WhenGrossPayInputIsNotANumber_ThenIsValidReturnsFalse()
        {
            var result = Validator.IsValid("1000fhgjd");

            Assert.IsFalse(result.IsValid);
        }
    }
}
