using System.Threading.Tasks;
using TBIApp.Services.Models;

namespace TBIApp.Services.Services.Contracts
{
    public interface IApplicationService
    {
        Task<LoanApplicationDTO> CreateAsync(LoanApplicationDTO newLoan);
        Task RemoveAsync(string id);
        Task ChangeStatusAsync(string id, string appStatus);
    }
}