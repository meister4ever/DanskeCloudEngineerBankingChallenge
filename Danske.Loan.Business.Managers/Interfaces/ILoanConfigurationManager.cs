using Danske.Loan.Business.Managers.Models;

namespace Danske.Loan.Business.Managers.Interfaces
{
    public interface ILoanConfigurationManager
    {
        LoanTerms GetLoanTerms(double amount, int duration);
    }
}
