using System;
using System.Collections.Generic;
using System.Text;
using TBIApp.Data.Models;
using TBIApp.Services.Models;

namespace TBIApp.Services.Mappers.Contracts
{
    public interface IReportDiagramDTOMapper
    {
        ReportDiagram MapFrom(ReportDiagramDTO entity);
        ReportDiagramDTO MapFrom(ReportDiagram entity);
        IList<ReportDiagram> MapFrom(ICollection<ReportDiagramDTO> entities);
        IList<ReportDiagramDTO> MapFrom(ICollection<ReportDiagram> entities);
    }
}
