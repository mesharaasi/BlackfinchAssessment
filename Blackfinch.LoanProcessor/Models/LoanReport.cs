namespace Blackfinch.LoanProcessor.Models
{
    /// <summary>
    /// Class contains properties of loan report to display in UI
    /// </summary>
    public class LoanReport
    {
        /// <summary>
        /// Total applications
        /// </summary>
        public int? TotalApplications { get; set; }

        /// <summary>
        /// Count of success applications
        /// </summary>
        public int? SuccessApplicationsCount { get; set; }

        /// <summary>
        /// Count of declined applications
        /// </summary>
        public int? DeclinedApplicationsCount { get; set; }

        /// <summary>
        /// Total value of loans of all approved application
        /// </summary>
        public decimal? TotalLoanValue { get; set; }

        /// <summary>
        /// Mean loan to value of all the submitted applications
        /// </summary>
        public decimal? MeanLoanToValue { get; set; }

    }
}
