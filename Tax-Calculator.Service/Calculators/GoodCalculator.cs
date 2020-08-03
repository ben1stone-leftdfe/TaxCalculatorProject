using System.Collections.Generic;
using Tax_Calculator.Domain;

namespace Tax_Calculator.Service.Calculators
{
    public class GoodCalculator : IGoodCalculator
    {
        public CalculatorOutput Calculate(double grossPay, List<TaxBand> bands, List<TaxBand> natInsuranceBands)
        {
            Calculator TaxCalculator = new TaxCalculator(bands);
            Calculator NatInsuranceCalculator = new NatInsuranceCalculator(natInsuranceBands);

            var taxDeduction = TaxCalculator.CalculateDeduction(grossPay);
            var natInsuranceDeducation = NatInsuranceCalculator.CalculateDeduction(grossPay);

            var output = new CalculatorOutput()
            {
                NetPay = grossPay - taxDeduction - natInsuranceDeducation,
                TaxDeducted = taxDeduction,
                NationalInsuranceDeducted = natInsuranceDeducation
            };

            return output;
        }
    }
}
