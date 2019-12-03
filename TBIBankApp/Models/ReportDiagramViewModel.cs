using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBIApp.Data.Models;

namespace TBIBankApp.Models
{
    public class ReportDiagramViewModel
    {
        public int NotReviewedCount { get; set; }
        public double PercentNotReviewed { get; set; }
        public int InvalidCount { get; set; }
        public double PercentInvalid { get; set; }
        public int NewCount { get; set; }
        public double PercentNew { get; set; }
        public int OpenCount { get; set; }
        public double PercentOpen { get; set; }
        public int ClosedCount { get; set; }
        public double PercentClosed { get; set; }
        public int RejectedCount { get; set; }
        public double PercentRejected { get; set; }
        public int AcceptedCount { get; set; }
        public double PercentAccepted { get; set; }
        public ICollection<UserViewModel> OnlineUsers { get; set; }

    }
}
