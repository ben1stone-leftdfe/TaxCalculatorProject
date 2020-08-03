using System;
using System.Collections.Generic;
using System.Text;

namespace Tax_Calculator.Service.Validators
{
    public interface IGrossPayValidator
    {
        public GrossPayValidatorResponse IsValid(string grossPayInput);
    }
}
