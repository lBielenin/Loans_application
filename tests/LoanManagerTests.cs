using Loans_application.Models;
using Loans_application.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Loan_application.Tests
{
    public class LoanManagerTests
    {
        [Fact]
        public void LoanManager_ManageLoans_DistributesLoansValidly()
        {
            Dictionary<int, int> expectedResultByIds = new() { { 1, 0 }, { 2, 1 }, { 3, 2 }, { 4, 1 } };
            var manager = new LoanManagerService();

            var result = manager.ManageLoans(TestData.GetLoans(), TestData.GetFacilities(), TestData.GetCovenants()).OrderBy(l => l.Loan.Id).ToList();

            result.ForEach(fac => 
                Assert.Equal(expectedResultByIds[fac.Loan.Id], fac.Loan.FacilityId)
                );
        }

        [Fact]
        public void LoanManager_ManageLoans_WithTooMuchLoans_StillShoudPerformCaluctionForRest ()
        {
            Dictionary<int, int> expectedResultByIds = new() { { 1, 0 }, { 2, 1 }, { 3, 2 }, { 4, 1 } };
            var loans = TestData.GetLoans();
            loans.Add(new() { Amount = 1234, FacilityId = 2, InterestRate = 0.00, DefaultLikelihood = 0.1, Id = 23 });

            var manager = new LoanManagerService();

            var result = manager.ManageLoans(loans, TestData.GetFacilities(), TestData.GetCovenants()).OrderBy(l => l.Loan.Id).ToList();

            result.ForEach(fac =>
                Assert.Equal(expectedResultByIds[fac.Loan.Id], fac.Loan.FacilityId)
                );
        }


        [Fact]
        public void LoanManager_ManageLoans_WithInvalidLoans_ShouldReturnEmptyList()
        {
            var loans = 
                new List<Loan> { new () { Amount = 1000000, DefaultLikelihood = 0.0, Id = 1, InterestRate = 0.0, State = "CAL" } };

            var manager = new LoanManagerService();

            var result = manager.ManageLoans(loans, TestData.GetFacilities(), TestData.GetCovenants()).OrderBy(l => l.Loan.Id).ToList();

            Assert.Empty(result);
        }

        [Theory]
        [InlineData(200, 0.2, 0.3, 312)]
        [InlineData(500, 0.15, 0.22, 701.5)]
        public void LoanFacilityPair_CalculatesCostsValidly(
            double amount, double loanInterestRate, double facilityInterestRate, double expectedResult)
        {
            LoanFacilityPair testPair =
                new
                (
                    new Loan() { Amount = amount, InterestRate = loanInterestRate },
                    new Facility { InterestRate = facilityInterestRate }
                );

            Assert.Equal(expectedResult, testPair.Cost);
        }
    }
}
