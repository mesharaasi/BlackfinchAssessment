using Blackfinch.LoanProcessor.Interface;
using Blackfinch.LoanProcessor.Models;
using Blackfinch.LoanProcessor.Services;
class Program
{
    /// <summary>
    /// Main method to take input from user and returns output in the required format on console.
    /// </summary>
    /// <param name="args">console arguments</param>
    static void Main(string[] args)
    {
        Console.WriteLine("\nLoan Report:");

        //Local variables
        //Instantiate loan processor class
        ILoanProcessor loanProcessor = new LoanProcessor();
        LoanRequest loanRequest = new LoanRequest();
        bool inputRequest = false;

        try
        {
            //Loop to accept input of the required fields until user enter "N" in the first question
            while (!inputRequest)
            {
                //Requesting user input for
                Console.WriteLine("Do you want to add input (Y/N)?: ");
                var input = Console.ReadLine();
                //If user input is "Y" then continue accepting input for Loan Amount, Asset Value & Credit Score
                //If user input is "N" then set the bool to true to stop the process and exit the loop
                if (input?.ToUpper() == "Y")
                {
                    //Call local method to accept input and return loan request object
                    //Add loan request object to list
                    loanRequest = InputLoanRequest();
                    loanProcessor.AddLoan(loanRequest);
                }
                else if (input?.ToUpper() == "N")
                {
                    inputRequest = true;
                }
            }
            //Check if there is no data in the list then exit the application
            if (loanProcessor.LoanRequests.Count == 0)
            {
                Console.WriteLine("Empty input data");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
                System.Environment.Exit(0);
            }

            // Calling loan processor library function to get the report data after calculations
            LoanProcessorResponse response = loanProcessor.DisplayReport();

            //Check if the loan processor response object is live and success property to true
            if (response != null && response.Success)
            {
                //Set loan report from response 
                LoanReport? loanReport = response.loanReport;
                //Iterate through loan requests list and display data
                foreach (var loan in loanProcessor.LoanRequests)
                {
                    //set the application status based on the boolean value
                    string _applicationStatus = (loan.IsSuccessful ? "Approved" : "Declined");
                    Console.WriteLine($"Loan Amount: {loan.LoanAmount:C}, Property Value: {loan.AssetValue:C}, " +
                                      $"Credit Score: {loan.CreditScore.ToString():F2},LTV: {loan.GetLoanToValue():F2}%, " +
                                      $"Application Status: {_applicationStatus:F2}");
                }

                Console.WriteLine($"\nThe total number of applicants to date: " +
                                    $"{loanReport?.TotalApplications.ToString():F2}");
                Console.WriteLine($"\nThe total number of applicants to date with success status: " +
                                    $"{loanReport?.SuccessApplicationsCount.ToString():F2}");
                Console.WriteLine($"\nThe total number of applicants to date with declined status: " +
                                    $"{loanReport?.DeclinedApplicationsCount.ToString():F2}");
                Console.WriteLine($"\nThe total value of loans written to date: " +
                                    $"{loanReport?.TotalLoanValue:C}");
                Console.WriteLine($"\nAverage LTV of all applications: " +
                                    $"{loanReport?.MeanLoanToValue:F2}%");
            }
            else
                //If response object returns false then display error message
                Console.WriteLine($"\nError occurred: {response?.Message:F2}");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
        catch (Exception ex) {

            //Capture exception and display in the console
            Console.WriteLine("\nFollowing exception occurred in the application:");
            Console.WriteLine(ex.Message);
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();

        }
        finally{
        }
    }

    /// <summary>
    /// Static function to accept input from console
    /// </summary>
    /// <returns>loan request object</returns>
    static LoanRequest InputLoanRequest()
    {
        //Local variables
        //Declare loan request object
        LoanRequest loanRequest = new LoanRequest();
        bool validInput = false;

        //Loop to accept loan amount input until user enter a valid data
        while (!validInput)
        {
            Console.WriteLine("Input Loan Amount: ");
            var input = Console.ReadLine();
            //Check if the input data is valid
            if (input != null && decimal.TryParse(input, out _))
            {
                //Assign the loan value to loan request object and exit the loop
                validInput = true;
                loanRequest.LoanAmount = decimal.Parse(input);
            }
        }

        validInput = false;
        //Loop to accept asset value input until user enter a valid data
        while (!validInput)
        {
            Console.WriteLine();
            Console.WriteLine("Input Asset Value: ");
            var input = Console.ReadLine();
            //Check if the input data is valid
            if (input != null && decimal.TryParse(input, out _))
            {
                //Accept the asset value only if greater than requested loan amount
                if (decimal.Parse(input) <= loanRequest.LoanAmount)
                    Console.WriteLine("Loan amount is higher than asset value");
                else
                {
                    //Assign the asset value to loan request object and exit the loop
                    validInput = true;
                    loanRequest.AssetValue = decimal.Parse(input);
                }
            }
        }

        validInput = false;
        //Loop to accept credit score input until user enter a valid data
        while (!validInput)
        {
            Console.WriteLine();
            Console.WriteLine("Input Credit Score: ");
            var input = Console.ReadLine();
            if (input != null && decimal.TryParse(input, out _))
            {
                //Assign the credit score value to loan request object and exit the loop
                validInput = true;
                loanRequest.CreditScore = decimal.Parse(input);
            }
        }

        //return loan request object
        return loanRequest;
    }
}