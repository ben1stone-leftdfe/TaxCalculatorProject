using System.Collections.Generic;
using Tax_Calculator.Domain;

namespace Tax_Calculator.Service.Calculators
{
    public interface IGoodCalculator
    {
        public CalculatorOutput Calculate(double grossPay, List<TaxBand> bands, List<TaxBand> natInsuranceBands);
    }
}
