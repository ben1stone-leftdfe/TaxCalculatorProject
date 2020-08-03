using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Tax_Calculator.Domain;
using Tax_Calculator.Service.Calculators;
using Tax_Calculator.Service.Validators;
using Tax_Calculator.Web;

namespace Tax_Calculator.API.Controllers
{
    [ApiController]
    
    public class TaxCalculator : ControllerBase
    {
        private readonly IConfigService _config;
        private readonly IGrossPayValidator _grossPayValidator;
        private readonly IGoodCalculator _goodCalculator;

        public TaxCalculator(IConfigService config, IGrossPayValidator grossPayValidator, IGoodCalculator goodCalculator)
        {
            _config = config;
            _grossPayValidator = grossPayValidator;
            _goodCalculator = goodCalculator;
        }

        [HttpGet("api/tax-calculator/{grossPayInput}")]
        public CalculatorOutput Calulate(string grossPayInput)
        {
            var validator = _grossPayValidator.IsValid(grossPayInput);

            if (!validator.IsValid)
            {
                throw new ArgumentException(validator.Errors[0]);
            }

            var taxBands = _config.GetTaxBands();
            var natInsuranceBands = _config.GetNatInsuranceBands();

            return _goodCalculator.Calculate(validator.GrossPay, taxBands, natInsuranceBands);
        }
    }
}
