using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBIApp.Services.Models;
using TBIBankApp.Mappers.Contracts;
using TBIBankApp.Models.Attachments;

namespace TBIBankApp.Mappers
{
    public class AttachmentViewModelMapper : IAttachmentViewModelMapper
    {
        public AttachmentViewModel MapFrom(AttachmentDTO entity)
        {
            return new AttachmentViewModel()
            {
                FileName = entity.FileName,
                SizeMb = entity.SizeMb,
                SizeKb = entity.SizeKb
            };
        }
        public AttachmentDTO MapFrom(AttachmentViewModel entity)
        {
            return new AttachmentDTO()
            {
                FileName = entity.FileName,
                SizeMb = entity.SizeMb,
                SizeKb = entity.SizeKb
            };
        }
        public IList<AttachmentViewModel> MapFrom(ICollection<AttachmentDTO> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

        public IList<AttachmentDTO> MapFrom(ICollection<AttachmentViewModel> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

    }
}
