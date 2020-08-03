using System;
using System.Collections.Generic;
using Tax_Calculator.Domain;

namespace Tax_Calculator.Service.Calculators
{
    public class NatInsuranceCalculator : Calculator
    {
        public NatInsuranceCalculator(List<TaxBand> bands) : base(bands)
        {
        }

        public override double CalculateDeduction(double grossPay)
        {
            var weeklyDeductions = 0.0;
            var estimatedWeeklyEarnings = ConvertYearlyToWeekly(grossPay);

            for (int i = 0; i < Bands.Count; i++)
            {
                if (i < Bands.Count - 1)
                {
                    if (estimatedWeeklyEarnings <= Bands[i].Band)
                    {
                        break;
                    }
                    else if (estimatedWeeklyEarnings > Bands[i].Band && estimatedWeeklyEarnings <= Bands[i + 1].Band)
                    {
                        weeklyDeductions += (estimatedWeeklyEarnings - Bands[i].Band) * (Bands[i].PercentageDeduction / 100);
                        break;
                    }
                    else
                    {
                        weeklyDeductions += (Bands[i + 1].Band - Bands[i].Band) * (Bands[i].PercentageDeduction / 100);
                    }
                }
                else
                {
                    weeklyDeductions += (estimatedWeeklyEarnings - Bands[i].Band) * (Bands[i].PercentageDeduction / 100);
                }
            }

            return ConvertWeeklyToYearly(weeklyDeductions);
        }
    }
}
