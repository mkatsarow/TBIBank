using System.Threading.Tasks;
using TBIApp.Services.Models;

namespace TBIApp.Services.Services.Contracts
{
    public interface IAttachmentService
    {
        Task<AttachmentDTO> CreateAsync(AttachmentDTO attachmentDTO);
    }
}