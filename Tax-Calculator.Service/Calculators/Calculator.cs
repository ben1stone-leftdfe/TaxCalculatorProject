using System;
using System.Collections.Generic;
using System.Text;
using Tax_Calculator.Domain;

namespace Tax_Calculator.Service.Calculators
{
    public abstract class Calculator
    {
        protected readonly List<TaxBand> Bands;

        protected Calculator(List<TaxBand> bands)
        {
            Bands = bands;
        }

        public abstract double CalculateDeduction(double grossPay);

        public double ConvertYearlyToWeekly(double pay)
        {
            return pay / 52;
        }

        public double ConvertWeeklyToYearly(double pay)
        {
            return pay * 52;
        }
    }
}
