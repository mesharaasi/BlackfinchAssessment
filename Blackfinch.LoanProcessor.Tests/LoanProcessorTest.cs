using Blackfinch.LoanProcessor.Interface;
using Blackfinch.LoanProcessor.Models;

namespace Blackfinch.LoanProcessor.Tests
{
    public class LoanProcessorTest
    {
        /// <summary>
        /// Run load report
        /// </summary>
        [Fact]
        public void RunLoanReport()
        { 
            //Instantiate loan processor class
            ILoanProcessor loanProcessor = new Blackfinch.LoanProcessor.Services.LoanProcessor();

            // Sample data
            loanProcessor.AddLoan(new LoanRequest { LoanAmount = 250000, AssetValue = 300000, CreditScore = 800 });
            loanProcessor.AddLoan(new LoanRequest { LoanAmount = 180000, AssetValue = 250000, CreditScore = 900 });
            loanProcessor.AddLoan(new LoanRequest { LoanAmount = 500000, AssetValue = 600000, CreditScore = 900 });

            // Report generation
            LoanProcessorResponse response = loanProcessor.DisplayReport();
            LoanReport? loanReport = response.loanReport;
            Assert.NotNull(loanReport);
            string _applicationStatus = (loanProcessor.LoanRequests[0].IsSuccessful ? "Approved" : "Declined");
            //Check and compare the results with static values
            Assert.Equal("Declined", _applicationStatus);
            Assert.Equal("3", loanReport.TotalApplications.ToString());
            Assert.Equal("2", loanReport.SuccessApplicationsCount.ToString());
            Assert.Equal("1", loanReport.DeclinedApplicationsCount.ToString());
            Assert.Equal("680000", loanReport.TotalLoanValue.ToString());
            Assert.Equal("79.55", Math.Round(loanReport.MeanLoanToValue.GetValueOrDefault(),2).ToString());
        }
    }
}