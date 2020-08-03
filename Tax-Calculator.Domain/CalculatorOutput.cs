namespace Tax_Calculator.Domain
{
    public class CalculatorOutput
    {
        public double? TaxDeducted { get; set; }
        public double? NationalInsuranceDeducted { get; set; }
        public double? NetPay { get; set; }
    }
}
