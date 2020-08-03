using System;
using System.Collections.Generic;
using System.Text;
using Tax_Calculator.Domain;

namespace Tax_Calculator.Web
{
    public interface IConfigService
    {
        public List<TaxBand> GetTaxBands();

        public List<TaxBand> GetNatInsuranceBands();
    }
}
