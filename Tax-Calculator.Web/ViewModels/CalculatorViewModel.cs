using System.ComponentModel.DataAnnotations;
using Tax_Calculator.Domain;

namespace Tax_Calculator.Web.ViewModels
{
    public class CalculatorViewModel
    {
        [Required]
        public string GrossPayInput { get; set; }

        public double GrossPay { get; set; }
        public double? NetPay { get; set; }

        public CalculatorOutput CalculatorOutput { get; set; } 
    }
}
