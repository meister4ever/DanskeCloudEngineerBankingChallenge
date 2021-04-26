using Danske.Common.StringConstants;
using Danske.Loan.Business.Managers.Interfaces;
using Danske.Loan.Business.Managers.Models;
using Microsoft.Extensions.Configuration;

namespace Danske.Loan.Business.Managers
{
    public class LoanConfigurationManager : ILoanConfigurationManager
    {
        private readonly IConfiguration Configuration;

        public LoanConfigurationManager(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public LoanTerms GetLoanTerms(double amount, int duration)
        {
            // Gets from the configuration file appsettings.json the AnnualInterestRatePercentage, if not present, gets the default value.
            var annualInterestRatePercentage = Configuration.GetValue(ConfigurationFileConstants.AnnualInterestRatePercentage,
                                                                    DefaultTerms.AnnualInterestRatePercentage);

            // Gets from the configuration file appsettings.json the CompoundInterestRateCalculationPeriod, if not present, gets the default value.
            var compoundInterestRateCalculationPeriod = Configuration.GetValue(ConfigurationFileConstants.CompoundInterestRateCalculationPeriod,
                                                                    DefaultTerms.CompoundInterestRateCalculationPeriod);

            // Gets from the configuration file appsettings.json the AdminitrationFeePercentage, if not present, gets the default value.
            var adminitrationFeePercentage = Configuration.GetValue(ConfigurationFileConstants.AdminitrationFeePercentage,
                                                        DefaultTerms.AdminitrationFeePercentage);

            var adminitrationFeeMax = Configuration.GetValue(ConfigurationFileConstants.AdminitrationFeeMax,
                                                        DefaultTerms.AdminitrationFeeMax);

            var culture = Configuration.GetValue(ConfigurationFileConstants.Culture,
                                            DefaultTerms.Culture);

            var timesPerYear = YearPeriods.GetYearPeriods(compoundInterestRateCalculationPeriod);
            var annualInterestRate = annualInterestRatePercentage / 100;

            return new LoanTerms
            {
                Amount = amount,
                DurationInYears = duration,
                AnnualInterestRate = annualInterestRate,
                TimesPerYear = timesPerYear,
                AdministrationFeePercentage = adminitrationFeePercentage,
                AdministrationFeeMax = adminitrationFeeMax,
                CultureInfo = culture
            };
        }
    }
}
