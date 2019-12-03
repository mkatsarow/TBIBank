using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TBIBankApp.Models.Emails
{
    public class ChangeEmailStatusViewModel
    {
        public string EmailId { get; set; }
        public int EmailStatusEnumId { get; set; }
    }
}
