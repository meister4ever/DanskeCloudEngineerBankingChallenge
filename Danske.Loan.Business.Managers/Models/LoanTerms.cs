namespace Danske.Loan.Business.Managers.Models
{
    public class LoanTerms
    {
        public double Amount { get; set; }
        public int DurationInYears { get; set; }
        public int TimesPerYear { get; set; }
        public int NumberOfPayments { get => TimesPerYear * DurationInYears; }
        public double AnnualInterestRate { get; set; }
        public double PeriodInterestRate { get => AnnualInterestRate / TimesPerYear; }
        public double AdministrationFeePercentage { get; set; }
        public double AdministrationFeeMax { get; set; }
        public string CultureInfo { get; set; }
    }
}
