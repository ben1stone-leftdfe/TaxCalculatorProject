using System;
using System.Collections.Generic;
using System.Text;

namespace Tax_Calculator.Service.Validators
{
    public class GrossPayValidator : IGrossPayValidator
    {
        public GrossPayValidatorResponse IsValid(string grossPayInput)
        {
            double grossPay;
            var errors = new List<string>();

            var isValid = double.TryParse(grossPayInput, out grossPay);

            if (isValid )
            {
                if (grossPay < 0)
                {
                    isValid = false;
                    errors.Add("Enter a positive number");
                }
            }
            else
            {
                errors.Add("Enter a number");
            }

            return new GrossPayValidatorResponse(grossPay, errors, isValid);
        }
    }

    public class GrossPayValidatorResponse
    {
        public double GrossPay { get; set; }
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; }

        public GrossPayValidatorResponse(double grossPay, List<string> errors, bool isValid)
        {
            GrossPay = grossPay;
            Errors = errors;
            IsValid = isValid;
        }
    }
}
