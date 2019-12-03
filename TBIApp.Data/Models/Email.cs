using System;
using System.Collections.Generic;

namespace TBIApp.Data.Models
{
    public class Email
    {
        public string Id { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string GmailEmailId { get; set; }
        public EmailStatusesEnum Status { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        public LoanApplication LoanApplication { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string Body { get; set; }
        public string RecievingDateAtMailServer { get; set; }
        public DateTime RegisteredInDataBase { get; set; }
        public DateTime LastStatusUpdate { get; set; }
        public bool IsOpne { get; set; }
        public DateTime LastOpen { get; set; }

    }
}
