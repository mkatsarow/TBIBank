using System.Threading.Tasks;

namespace TBIApp.Services.Services.Contracts
{
    public interface IcheckCardIdService
    {
        Task<bool> IsRealAsync(string cardId);
    }
}