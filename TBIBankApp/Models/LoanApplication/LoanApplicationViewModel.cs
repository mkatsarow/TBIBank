using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBIApp.Data.Models;

namespace TBIBankApp.Models.LoanApplication
{
    public class LoanApplicationViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EGN { get; set; }
        public LoanApplicationStatus Status { get; set; }
        public string CardId { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }
       // public Email Email { get; set; }

    }
}
