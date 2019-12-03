using System.Collections.Generic;
using TBIApp.Services.Models;
using TBIBankApp.Models;

namespace TBIBankApp.Mappers.Contracts
{
    public interface IReportDiagramViewModelMapper
    {
        ICollection<ReportDiagramViewModel> MapFrom(ICollection<ReportDiagramDTO> entities);
        ICollection<ReportDiagramDTO> MapFrom(ICollection<ReportDiagramViewModel> entities);
        ReportDiagramViewModel MapFrom(ReportDiagramDTO entity);
        ReportDiagramDTO MapFrom(ReportDiagramViewModel entity);
    }
}