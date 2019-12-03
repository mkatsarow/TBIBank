using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TBIApp.Data.Models;
using TBIApp.Services.Mappers.Contracts;
using TBIApp.Services.Models;

namespace TBIApp.Services.Mappers
{
    public class ReportDiagramDTOMapper : IReportDiagramDTOMapper
    {
        private readonly IUserDTOMapper userDTOMapper;

        public ReportDiagramDTOMapper(IUserDTOMapper userDTOMapper)
        {
            this.userDTOMapper = userDTOMapper;
        }

        public ReportDiagram MapFrom(ReportDiagramDTO entity)
        {
            return new ReportDiagram()
            {
                AcceptedCount = entity.AcceptedCount,
                NewCount = entity.NewCount,
                ClosedCount = entity.ClosedCount,
                InvalidCount = entity.InvalidCount,
                OpenCount = entity.OpenCount,
                NotReviewedCount = entity.NotReviewedCount,
                RejectedCount = entity.NotReviewedCount,
                PercentAccepted = entity.PercentAccepted,
                PercentClosed = entity.PercentClosed,
                PercentInvalid = entity.PercentInvalid,
                PercentNew = entity.PercentNew,
                PercentNotReviewed = entity.PercentNotReviewed,
                PercentOpen = entity.PercentOpen,
                PercentRejected = entity.PercentRejected,
                OnlineUsers = this.userDTOMapper.MapFrom(entity.OnlineUsers)
                
                
            };
        }
        public ReportDiagramDTO MapFrom(ReportDiagram entity)
        {
            return new ReportDiagramDTO()
            {
                AcceptedCount = entity.AcceptedCount,
                NewCount = entity.NewCount,
                ClosedCount = entity.ClosedCount,
                InvalidCount = entity.InvalidCount,
                OpenCount = entity.OpenCount,
                NotReviewedCount = entity.NotReviewedCount,
                RejectedCount = entity.RejectedCount,
                PercentAccepted = entity.PercentAccepted,
                PercentClosed = entity.PercentClosed,
                PercentInvalid = entity.PercentInvalid,
                PercentNew = entity.PercentNew,
                PercentNotReviewed = entity.PercentNotReviewed,
                PercentOpen = entity.PercentOpen,
                PercentRejected = entity.PercentRejected,
                OnlineUsers = this.userDTOMapper.MapFrom(entity.OnlineUsers)

            };
        }
        public IList<ReportDiagram> MapFrom(ICollection<ReportDiagramDTO> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

        public IList<ReportDiagramDTO> MapFrom(ICollection<ReportDiagram> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }
    }
}
