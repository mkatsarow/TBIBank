using System.Collections.Generic;
using TBIApp.Data.Models;
using TBIApp.Services.Models;

namespace TBIApp.Services.Mappers.Contracts
{
    public interface IAttachmentDTOMapper
    {
        AttachmentDTO MapFrom(Attachment entity);
        Attachment MapFrom(AttachmentDTO entity);
        IList<AttachmentDTO> MapFrom(ICollection<Attachment> entities);
        IList<Attachment> MapFrom(ICollection<AttachmentDTO> entities);
    }
}