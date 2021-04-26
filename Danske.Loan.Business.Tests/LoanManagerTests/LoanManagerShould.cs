using Danske.Loan.Business.Managers;
using Danske.Loan.Business.Managers.Models;
using FluentAssertions;
using System;
using Xunit;

namespace Danske.Loan.Business.Tests.LoanManagerTests
{
    public class LoanManagerShould
    {
        [Fact]
        public void GetExpectedAop()
        {
            // Arrange  [AAA (Arrange, Act, Assert)]
            Initialize(out var loanTerms);

            // From the PDF Statement:
            /// Loan amount = 500.000 kr. for 10 years
            /// Expected interest rate is 136.393,09 kr.
            /// Expected administration fee is 5.000 kr.
            // This is the expected AOP that comes from the expected calculation:
            /// So total would be 500.000 + 136.393,09 + 5000 = 636.393,09 + 5000 = 641.393,09
            /// So AOP is 641.393,09 / 10 / 500.000 = 0.128278618 (Windows calculator)
            var expectedAop = 0.128278618;

            /// SUT a.k.a. "System under test" https://en.wikipedia.org/wiki/System_under_test
            var sut = new LoanManager();

            // Act
            var Aop = sut.GetAOP(loanTerms);

            // Assert
            /// BeApproximately checks with the precision specified
            /// (The approximation is needed because of the 2 decimals)
            /// The total amount paid as interest rate that the manager calculates has
            /// a lot more precision than the 2 decimals of 136.393 ',09'
            /// 0.000000001 is because 0.128278618 has 9 decimals (same amount as 0.000000001)
            Aop.Should().BeApproximately(expectedAop, 0.000000001);
        }

        [Fact]
        public void GetExpectedMonthlyPaymentAmount_When_GetMonthlyCost_IsCalled()
        {
            // Arrange
            Initialize(out var loanTerms);

            // From the PDF Statement
            /// Loan amount of 500.000 kr. for 10 years should show that the monthly payment is 5.303,28 kr.
            var expectedMonthlyPaymentAmount = 5303.28;

            var sut = new LoanManager();

            // Act
            var monthlyCost = Math.Round(sut.GetMonthlyCost(loanTerms), 2);

            // Assert
            expectedMonthlyPaymentAmount.Should().Be(monthlyCost);
        }

        [Fact]
        public void GetExpectedTotalAmountPaidInInterest_When_GetInterestTotal_IsCalled()
        {
            // Arrange
            Initialize(out var loanTerms);

            // From the PDF Statement
            /// Furthermore, it shows that the total amount paid as interest rate is 136.393,09 kr.
            var expectedTotalAmountPaidInInterest = 136393.09;

            var sut = new LoanManager();

            // Act
            var totalAmountPaidInInterest = Math.Round(sut.GetInterestTotal(loanTerms), 2);

            // Assert
            totalAmountPaidInInterest.Should().Be(expectedTotalAmountPaidInInterest);
        }

        [Fact]
        public void GetExpectedAdministrationFee_When_GetAdministrativeFeesTotal_IsCalled()
        {
            // Arrange
            Initialize(out var loanTerms);

            // From the PDF Statement
            /// Furthermore, it shows that the total amount paid as interest rate is 136.393,09 kr. 
            /// and that administration fee was 5.000 kr.
            var expectedAdministrationFee = 5000;

            var sut = new LoanManager();

            // Act
            var administrationFee = sut.GetAdministrativeFeesTotal(loanTerms);

            // Assert
            administrationFee.Should().Be(expectedAdministrationFee);
        }

        [Fact]
        public void GetMaxAdministrationFee_When_GetAdministrativeFeesTotal_IsCalled_WithAHighEnoughAmount()
        {
            // Arrange
            Initialize(out var loanTerms);
            loanTerms.Amount = 1500000.00;

            // From the PDF Statement
            /// Administration fee (one-time): 1% or 10000 kr. whichever is lowest
            /// So for 1.5 millon kr., it would be 10.000 kr as it's the maximum amount
            var expectedAdministrationFee = 10000;

            var sut = new LoanManager();

            // Act
            var administrationFee = sut.GetAdministrativeFeesTotal(loanTerms);

            // Assert
            administrationFee.Should().Be(expectedAdministrationFee);
        }

        private void Initialize(out LoanTerms loanTerms)
        {
            loanTerms = new LoanTerms
            {
                Amount = 500000.00,
                DurationInYears = 10,
                TimesPerYear = 12,
                AnnualInterestRate = 0.05,
                AdministrationFeePercentage = 1,
                AdministrationFeeMax = 10000,
                CultureInfo = "da-DK"
            };
        }
    }
}
