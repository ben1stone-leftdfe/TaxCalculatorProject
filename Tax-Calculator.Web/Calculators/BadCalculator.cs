using System;

namespace Tax_Calculator.Web.Calculators
{
    public class BadCalculator
    {
        public double GetNetPay(string grossPayInput)
        {
            double netPay = 0.0;
            double grossPay;
            double taxDeduction = 0.0;
            double nationalInsuranceDeduction = 0.0;

            double _20pcBand = 12500.00;
            double _40pcBand = 50000.00;
            double _45pcBand = 150000.00;

            var isNumber = double.TryParse(grossPayInput, out grossPay);

            if (isNumber == false)
                throw new ArgumentException("Enter a number");

            if (grossPay < 0)
                throw new ArgumentException("Enter a positive number");

            if (grossPay > _45pcBand)
            {
                taxDeduction = ((grossPay - _45pcBand) * 0.45) + ((_45pcBand - _40pcBand) * 0.40) + (37500 * 0.2);
            }
            else if (grossPay > _40pcBand)
            {
                taxDeduction = ((grossPay - _40pcBand) * 0.40) + (37500 * 0.2);
            }
            else if (grossPay > _20pcBand)
            {
                taxDeduction = ((grossPay - _20pcBand) * 0.2);
            }

            var estimatedWeeklyEarnings = grossPay / 52;

            if (estimatedWeeklyEarnings > 962)
            {
                nationalInsuranceDeduction = ((((estimatedWeeklyEarnings - 962) * 0.02) + ((962 - 183) * 0.12)) * 52);
            }
            else if (estimatedWeeklyEarnings > 183)
            {
                nationalInsuranceDeduction = ((estimatedWeeklyEarnings - 183) * 0.12) * 52;
            }

            netPay = grossPay - taxDeduction - nationalInsuranceDeduction;

            Console.WriteLine($"Net yearly pay = £{netPay}");

            return netPay;
        }
    }
}