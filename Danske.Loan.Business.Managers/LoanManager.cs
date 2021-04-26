using Danske.Loan.Business.Managers.Interfaces;
using Danske.Loan.Business.Managers.Models;
using System;

namespace Danske.Loan.Business.Managers
{
    public class LoanManager : ILoanManager
    {
        public LoanManager()
        {
        }

        /// <summary>
        /// Calculates the annual percentage yield (APY), which takes compound interest into account
        /// </summary>
        /// <param name="rateOfPeriodInterest">Rate of interest for the period (on this case monthly) = AnnualInterestRate / TimesPerYear i.e. 0.00416 = 0.05 / 12 </param>
        /// <param name="numberOfPayments">Total of payments to repay the loan</param>
        /// <returns></returns>
        private static double GetAPY(double rateOfPeriodInterest, int numberOfPayments)
        {
            return Math.Pow((double)(1 + rateOfPeriodInterest), numberOfPayments) - 1;
        }

        /// <summary>
        /// The AOP - Yearly cost as a percentage of the loan amount
        /// </summary>
        /// <param name="loanTerms">Loan Terms</param>
        /// <returns></returns>
        public double GetAOP(LoanTerms loanTerms)
        {
            var totalLoanCost = loanTerms.Amount + GetInterestTotal(loanTerms) + GetAdministrativeFeesTotal(loanTerms);
            var annualCost = totalLoanCost / loanTerms.DurationInYears;
            return annualCost / loanTerms.Amount;
        }

        /// <summary>
        /// The total monthly cost of the loan
        /// </summary>
        /// <param name="loanTerms">Loan Terms</param>
        /// <returns></returns>
        public double GetMonthlyCost(LoanTerms loanTerms)
        {
            var Apy = GetAPY(loanTerms.PeriodInterestRate, loanTerms.NumberOfPayments);
            return loanTerms.Amount * (loanTerms.PeriodInterestRate * (Apy + 1)) / Apy;
        }

        /// <summary>
        /// Total amount paid in interest rate for the full duration of the loan
        /// </summary>
        /// <param name="loanTerms">Loan Terms</param>
        /// <returns></returns>
        public double GetInterestTotal(LoanTerms loanTerms) =>
            (GetMonthlyCost(loanTerms) * loanTerms.NumberOfPayments) - loanTerms.Amount;


        /// <summary>
        /// Total amount paid in administrative fees (excl. interest and installments)
        /// </summary>
        /// <param name="loanTerms">Loan Terms</param>
        /// <returns></returns>
        public double GetAdministrativeFeesTotal(LoanTerms loanTerms) =>
            Math.Min(loanTerms.Amount * loanTerms.AdministrationFeePercentage / 100, loanTerms.AdministrationFeeMax);
    }
}
