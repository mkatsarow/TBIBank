using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TBIApp.Data;
using TBIApp.Data.Models;
using TBIApp.Services.Mappers.Contracts;
using TBIApp.Services.Models;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.Services.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly TBIAppDbContext dbcontext;
        private readonly IReportDiagramDTOMapper reportDigramMapper;

        public StatisticsService(TBIAppDbContext dbcontext, 
                                 IReportDiagramDTOMapper reportDigramMapper)
        {

            this.dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            this.reportDigramMapper = reportDigramMapper ?? throw new ArgumentNullException(nameof(reportDigramMapper));
        }

        public async Task<ReportDiagramDTO> GetStatisticsAsync()
        {
            var totalcount = await this.dbcontext.Emails.CountAsync();

            var repDiagram = new ReportDiagram
            {
                InvalidCount = this.dbcontext.Emails.Where(e => e.Status == EmailStatusesEnum.InvalidApplication).Count(),
                NotReviewedCount = this.dbcontext.Emails.Where(e => e.Status == EmailStatusesEnum.NotReviewed).Count(),
                NewCount = this.dbcontext.Emails.Where(e => e.Status == EmailStatusesEnum.New).Count(),
                OpenCount = this.dbcontext.Emails.Where(e => e.Status == EmailStatusesEnum.Open).Count(),
                ClosedCount = this.dbcontext.Emails.Where(e => e.Status == EmailStatusesEnum.Closed).Count(),
                RejectedCount = this.dbcontext.LoanApplications.Where(a => a.Status == LoanApplicationStatus.Rejected).Count(),
                AcceptedCount = this.dbcontext.LoanApplications.Where(a => a.Status == LoanApplicationStatus.Accepted).Count(),
                OnlineUsers = await this.dbcontext.Users.Where(u => u.IsOnline == true).Include(x=>x.UserEmails).ToListAsync()

            };

            return this.reportDigramMapper.MapFrom(repDiagram);
        }
    }
}
