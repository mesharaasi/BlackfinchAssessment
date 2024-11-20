namespace Blackfinch.LoanProcessor.Models
{
    /// <summary>
    /// Class represents loan processor response object
    /// </summary>
    public class LoanProcessorResponse
    {
        /// <summary>
        /// Stores boolean value (true/false) depending on data loading. 
        /// If data is loaded successfully then variable stores true else false.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Stores success or error message
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Stores Loan Report object
        /// </summary>
        public LoanReport? loanReport { get; set; }
    }
}
