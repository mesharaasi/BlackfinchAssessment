using Blackfinch.LoanProcessor.Interface;
using Blackfinch.LoanProcessor.Models;

namespace Blackfinch.LoanProcessor.Services
{
    /// <summary>
    /// Class contains functions to process loan
    /// </summary>
    public class LoanProcessor : ILoanProcessor
    {
        //List of application requests
        public List<LoanRequest> LoanRequests { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public LoanProcessor()
        {
            LoanRequests = new List<LoanRequest>();
        }

        /// <summary>
        /// Function to add loan request object to List
        /// </summary>
        /// <param name="loan">loan request object</param>
        public void AddLoan(LoanRequest loan)
        {
            LoanRequests.Add(loan);
        }

        /// <summary>
        /// Function to calculate average LTV of all submitted applications
        /// </summary>
        /// <returns>Average LTV value</returns>
        private decimal CalculateMeanLTV()
        {
            //If list contains empty loan application then set average value to zero.
            if (LoanRequests.Count == 0) return 0;

            decimal totalLTV = 0;
            //Iterate through list of loan requests and add loan to value of each application
            foreach (var loan in LoanRequests)
            {
                totalLTV += loan.GetLoanToValue();
            }
            //return averate mean to value
            return totalLTV / LoanRequests.Count;
        }

        /// <summary>
        /// Function to get the application status(true/false based on the business rules
        /// true: Approved, false: Declined
        /// </summary>
        /// <param name="loanRequest">loan request object of each application</param>
        /// <returns>application status of each loan request</returns>
        private bool FindApplicationStatus(LoanRequest loanRequest)
        {
            //Get the loan to value
            decimal _loantovalue = loanRequest.GetLoanToValue();

            //If the value of the loan is more than £1.5 million or less than £100,000
            //then the application must be declined
            if (loanRequest.LoanAmount > 1500000 || loanRequest.LoanAmount < 100000)
                return false;
            // If loan is more than 1 million and
            if (loanRequest.LoanAmount >= 1000000)
            {
                //If LTV is greater than 60 or credit score less than 950 then decline the application
                if (_loantovalue <= 60 && loanRequest.CreditScore >= 950)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            // If loan is less than 1 million and
            if (loanRequest.LoanAmount < 1000000)
            {
                //If the LTV is less than 60%, the credit score of the applicant must be 750 or more
                if (_loantovalue < 60)
                {
                    if (loanRequest.CreditScore >= 750)
                        return true;
                    else
                        return false;
                }

                //If the LTV is less than 80%, the credit score of the applicant must be 800 or more
                if (_loantovalue < 80)
                {
                    if (loanRequest.CreditScore >= 800)
                        return true;
                    else
                        return false;
                }
                //If the LTV is less than 90%, the credit score of the applicant must be 900 or more
                if (_loantovalue < 90)
                {
                    if (loanRequest.CreditScore >= 900)
                        return true;
                    else
                        return false;
                }
            }

            //If the LTV is 90 % or more, the application must be declined
            return false;
        }

        /// <summary>
        /// Function to return loan report data
        /// </summary>
        /// <returns>loan report object</returns>
        public LoanProcessorResponse DisplayReport()
        {
            LoanProcessorResponse response = new LoanProcessorResponse();
            response.Success = true;
            response.Message = "Successful";

            //Instantiate loan report object
            LoanReport? loanReport = new LoanReport();
            decimal _totalLoanValue = 0;
            int _successApplicationsCount = 0;

            try
            {
                //Iterate through Loan requests list
                foreach (var loan in LoanRequests)
                {
                    //Get the application status for each loan request and update the request object
                    //loan.ApplicationStatus = GetApplicationStatus(loan);
                    loan.IsSuccessful = FindApplicationStatus(loan);
                    //If application status is 'successful' then increament success application count
                    //and sum requested loan value
                    if (loan.IsSuccessful)
                    {
                        _successApplicationsCount++;
                        _totalLoanValue += loan.LoanAmount;
                    }
                }
                //Set the loan report object properties
                loanReport.TotalApplications = LoanRequests.Count;
                loanReport.SuccessApplicationsCount = _successApplicationsCount;
                loanReport.DeclinedApplicationsCount = (LoanRequests.Count - _successApplicationsCount);
                loanReport.TotalLoanValue = _totalLoanValue;
                loanReport.MeanLoanToValue = Math.Round(CalculateMeanLTV(), 2);
                //set the response object
                response.loanReport = loanReport;
            }
            catch (Exception ex)
            {
                //capture the error 
                response.Success = false;
                response.Message = ex.Message;

            }
            finally
            {
                //set loan report to null
                loanReport = null;
            }

            //return the loan report object
            return response;
        }
    }
}
