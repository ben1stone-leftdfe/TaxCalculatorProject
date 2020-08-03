using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Tax_Calculator.Domain;

namespace Tax_Calculator.Web
{
    public class ConfigService : IConfigService
    {
        private readonly IConfiguration _config;

        public ConfigService(IConfiguration config)
        {
            _config = config;
        }

        public List<TaxBand> GetNatInsuranceBands()
        {
            return _config.GetSection("NatInsuranceBands").Get<List<TaxBand>>();
        }

        public List<TaxBand> GetTaxBands()
        {
            return _config.GetSection("TaxBands").Get<List<TaxBand>>();
        }
    }
}
