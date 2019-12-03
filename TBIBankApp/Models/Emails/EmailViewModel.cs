using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBIApp.Data.Models;
using TBIBankApp.Models.Attachments;

namespace TBIBankApp.Models.Emails
{
    public class EmailViewModel
    {
        public string Id { get; set; }
        public string ReferenceNumber { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string GmailEmailId { get; set; }
        public string Body { get; set; }
        public string Status { get; set; }
        public string LoanApplicationStatus { get; set; }
        public ICollection<AttachmentViewModel> Attachments { get; set; }
        public int AttachmentCount { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime RegisteredInDataBase { get; set; }
        public DateTime LastStatusUpdate { get; set; }
        public bool IsOpne { get; set; }
        public DateTime LastOpen { get; set; }
    }
}
