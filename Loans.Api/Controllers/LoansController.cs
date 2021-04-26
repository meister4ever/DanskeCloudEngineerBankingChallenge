using Danske.Common.Extensions;
using Danske.Common.StringConstants;
using Danske.Loan.Api.Models.Requests;
using Danske.Loan.Api.Models.Responses;
using Danske.Loan.Business.Managers.Interfaces;
using Danske.Loan.Business.Managers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Loans.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanManager LoanManager;
        private readonly ILoanConfigurationManager LoanConfigurationManager;

        public LoansController(ILoanManager loanManager, ILoanConfigurationManager loanConfigurationManager)
        {
            LoanManager = loanManager;
            LoanConfigurationManager = loanConfigurationManager;
        }

        /// <summary>
        /// Gets the payment overview for a simplified loan
        /// </summary>
        /// <param name="request">The filter constraints on the data-set.</param>
        /// <response code="200">Successful Listing</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("PaymentOverview")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(LoanResponse))]
        public ActionResult<LoanResponse> GetPaymentOverview(
            [FromQuery] LoanRequest request)
        {
            var loanTerms = LoanConfigurationManager.GetLoanTerms(request.Amount, request.DurationInYears);

            return new LoanResponse
            {
                AOP = LoanManager.GetAOP(loanTerms).AsPercentageString(loanTerms.CultureInfo),
                MonthlyCost = LoanManager.GetMonthlyCost(loanTerms).AsCurrencyString(loanTerms.CultureInfo),
                InterestTotal = LoanManager.GetInterestTotal(loanTerms).AsCurrencyString(loanTerms.CultureInfo),
                AdministrativeFeesTotal = LoanManager.GetAdministrativeFeesTotal(loanTerms).AsCurrencyString(loanTerms.CultureInfo)
            };
        }
    }
}
