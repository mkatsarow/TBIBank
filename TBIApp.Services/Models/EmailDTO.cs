using System;
using System.Collections.Generic;
using TBIApp.Data.Models;

namespace TBIApp.Services.Models
{
    public class EmailDTO
    {
        public string Id { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string GmailEmailId { get; set; }
        public EmailStatusesEnum Status { get; set; }
        public ICollection<AttachmentDTO> Attachments { get; set; }
        public LoanApplication LoanApplication { get; set; }
        public string LoanApplicationStatus { get; set; }
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
