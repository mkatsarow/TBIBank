using System.Threading.Tasks;
using TBIApp.Services.Models;

namespace TBIApp.Services.Services.Contracts
{
    public interface IStatisticsService
    {
        Task<ReportDiagramDTO> GetStatisticsAsync();
    }
}