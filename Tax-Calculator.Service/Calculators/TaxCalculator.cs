using System;
using System.Collections.Generic;
using Tax_Calculator.Domain;

namespace Tax_Calculator.Service.Calculators
{
    public class TaxCalculator : Calculator
    {
        public TaxCalculator(List<TaxBand> bands) : base(bands)
        {
        }

        public override double CalculateDeduction(double grossPay)
        {
            var totalDeductions = 0.0;

            for (int i = 0; i < Bands.Count; i++)
            {
                if (i < Bands.Count - 1)
                {
                    if (grossPay <= Bands[i].Band)
                    {
                        break;
                    }
                    else if (grossPay > Bands[i].Band && grossPay <= Bands[i + 1].Band)
                    {
                        totalDeductions += (grossPay - Bands[i].Band) * (Bands[i].PercentageDeduction / 100);
                        break;
                    }
                    else
                    {
                        totalDeductions += (Bands[i + 1].Band - Bands[i].Band) * (Bands[i].PercentageDeduction / 100);
                    }
                }
                else
                {
                    totalDeductions += (grossPay - Bands[i].Band) * (Bands[i].PercentageDeduction / 100);
                }
            }

            return totalDeductions;
        }
    }
}
