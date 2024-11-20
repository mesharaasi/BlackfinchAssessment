using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackfinch.LoanProcessor.Models;

namespace Blackfinch.LoanProcessor.Interface
{
    public interface ILoanProcessor
    {

        //List of application requests
        List<LoanRequest> LoanRequests { set; get; }

        void AddLoan(LoanRequest loan);
        LoanProcessorResponse DisplayReport();
    }
}
