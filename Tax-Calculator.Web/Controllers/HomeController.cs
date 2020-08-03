using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tax_Calculator.Service.Calculators;
using Tax_Calculator.Service.Validators;
using Tax_Calculator.Web.Calculators;
using Tax_Calculator.Web.Models;
using Tax_Calculator.Web.ViewModels;

namespace Tax_Calculator.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfigService _config;
        private readonly IGrossPayValidator _grossPayValidator;
        private readonly IGoodCalculator _goodCalculator;

        public HomeController(IConfigService config, IGrossPayValidator grossPayValidator, IGoodCalculator goodCalculator)
        {
            _config = config;
            _grossPayValidator = grossPayValidator;
            _goodCalculator = goodCalculator;
        }

        public IActionResult Index()
        {
            var vm = new CalculatorViewModel();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> GoodCalculator(CalculatorViewModel vm)
        {
            var validator = _grossPayValidator.IsValid(vm.GrossPayInput);

            if (!validator.IsValid)
            {
                foreach (var error in validator.Errors)
                {
                    ModelState.AddModelError("Error", error);
                }

                return View("~/Views/Home/Index.cshtml", vm);
            }

            var taxBands = _config.GetTaxBands();
            var natInsuranceBands = _config.GetNatInsuranceBands();

            vm.GrossPay = validator.GrossPay;

            vm.CalculatorOutput = _goodCalculator.Calculate(vm.GrossPay, taxBands, natInsuranceBands);

            return View("~/Views/Home/Index.cshtml", vm);
        }

        [HttpPost]
        public IActionResult BadCalculator(CalculatorViewModel vm)
        {
            var Calculator = new BadCalculator();

            vm.CalculatorOutput.NetPay = Calculator.GetNetPay(vm.GrossPayInput);
            
            return View("~/Views/Home/Index.cshtml", vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
