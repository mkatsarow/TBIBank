using System.Threading.Tasks;

namespace TBIApp.Services.Services.Contracts
{
    public interface IDecodeService
    {
        Task<string> DecodeAsync(string message);
    }
}