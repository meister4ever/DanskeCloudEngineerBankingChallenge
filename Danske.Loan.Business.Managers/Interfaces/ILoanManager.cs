using Danske.Loan.Business.Managers.Models;

namespace Danske.Loan.Business.Managers.Interfaces
{
    public interface ILoanManager
    {
        double GetAOP(LoanTerms loanTerms);
        double GetMonthlyCost(LoanTerms loanTerms);
        double GetInterestTotal(LoanTerms loanTerms);
        double GetAdministrativeFeesTotal(LoanTerms loanTerms);
    }
}
