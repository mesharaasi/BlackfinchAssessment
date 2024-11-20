namespace Blackfinch.LoanProcessor.Models
{
    /// <summary>
    /// Class holds input data
    /// </summary>
    public class LoanRequest
    {
        /// <summary>
        /// Loan Amount
        /// </summary>
        public decimal LoanAmount
        { get; set; }

        /// <summary>
        /// Asset Value
        /// </summary>
        public decimal AssetValue
        { get; set; }

        /// <summary>
        /// Credit Score
        /// </summary>
        public decimal CreditScore
        { get; set; }

        /// <summary>
        /// Successful flag
        /// </summary>
        public bool IsSuccessful
        { get; set; }

        /// <summary>
        /// Calculate loan to value for each submitted loan request
        /// </summary>
        /// <returns>loan to value of each loan request</returns>
        public decimal GetLoanToValue()
        {
            return Math.Round(LoanAmount / AssetValue * 100, 2);
        }

    }
}

