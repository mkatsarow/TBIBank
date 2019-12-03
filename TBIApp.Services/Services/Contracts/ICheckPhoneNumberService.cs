using System.Threading.Tasks;

namespace TBIApp.Services.Services.Contracts
{
    public interface ICheckPhoneNumberService
    {
        Task<bool> IsRealAsync(string phoneNumber);
    }
}