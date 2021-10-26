using Loans_application.Models;
using System.Collections.Generic;

namespace Loan_application.Tests
{
    public static class TestData
    {
        public static List<Loan> GetLoans() =>
            new()
            {
                new() { Amount = 100, DefaultLikelihood = 0.2, Id = 1, State = "CA", InterestRate = 0.15 },
                new() { Amount = 150, DefaultLikelihood = 0.2, Id = 2, State = "MAS", InterestRate = 0.20 },
                new() { Amount = 200, DefaultLikelihood = 0.4, Id = 3, State = "TX", InterestRate = 0.15 },
                new() { Amount = 100, DefaultLikelihood = 0.2, Id = 4, State = "NY", InterestRate = 0.20 },
            };
        public static List<Facility> GetFacilities() =>
            new()
            {
                new() { Id = 0, Amount = 100, BankId = 666, InterestRate = 0.2 },
                new() { Id = 1, Amount = 1000, BankId = 666, InterestRate = 0.2 },
                new() { Id = 2, Amount = 200, BankId = 666, InterestRate = 0.2 },
            };

        public static List<Covenant> GetCovenants() =>
            new()
            {
                new() { BankId = 1, BannedState = "TX", MaxDefaultLikelihood = 03, FacilityId = 1 }
            };

    }
}
