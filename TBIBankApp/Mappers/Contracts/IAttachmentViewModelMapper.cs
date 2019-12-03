using System.Collections.Generic;
using TBIApp.Services.Models;
using TBIBankApp.Models.Attachments;

namespace TBIBankApp.Mappers.Contracts
{
    public interface IAttachmentViewModelMapper
    {
        AttachmentViewModel MapFrom(AttachmentDTO entity);
        AttachmentDTO MapFrom(AttachmentViewModel entity);
        IList<AttachmentViewModel> MapFrom(ICollection<AttachmentDTO> entities);
        IList<AttachmentDTO> MapFrom(ICollection<AttachmentViewModel> entities);
    }
}