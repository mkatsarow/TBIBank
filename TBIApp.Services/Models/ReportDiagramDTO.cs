using System.Collections.Generic;

namespace TBIApp.Services.Models
{
    public class ReportDiagramDTO
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
        public ICollection<UserDTO> OnlineUsers { get; set; }

    }

}
