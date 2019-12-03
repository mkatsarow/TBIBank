using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TBIApp.Data.Models;
using TBIApp.Services.Mappers.Contracts;
using TBIApp.Services.Models;

namespace TBIApp.Services.Mappers
{
    public class AttachmentDTOMapper : IAttachmentDTOMapper
    {
        public AttachmentDTO MapFrom(Attachment entity)
        {
            return new AttachmentDTO()
            {
                FileName = entity.FileName,
                SizeMb = entity.SizeMb,
                SizeKb = entity.SizeKb

            };
        }
        public Attachment MapFrom(AttachmentDTO entity)
        {
            return new Attachment()
            {
                FileName = entity.FileName,
                SizeMb = entity.SizeMb,
                SizeKb = entity.SizeKb

            };
        }
        public IList<Attachment> MapFrom(ICollection<AttachmentDTO> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

        public IList<AttachmentDTO> MapFrom(ICollection<Attachment> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }
    }
}
