using System;
using System.Threading.Tasks;
using TBIApp.Data;
using TBIApp.Services.Mappers.Contracts;
using TBIApp.Services.Models;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.Services.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly TBIAppDbContext dbcontext;
        private readonly IAttachmentDTOMapper attachmentDTOMapper;

        public AttachmentService(TBIAppDbContext dbcontext, 
                                 IAttachmentDTOMapper attachmentDTOMapper)
        {
            this.dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            this.attachmentDTOMapper = attachmentDTOMapper ?? throw new ArgumentNullException(nameof(attachmentDTOMapper));
        }

        public async Task<AttachmentDTO> CreateAsync(AttachmentDTO attachmentDTO)
        {
            var attachment = this.attachmentDTOMapper.MapFrom(attachmentDTO);

            if (attachment == null) throw new ArgumentNullException();

            this.dbcontext.Attachments.Add(attachment);

            await this.dbcontext.SaveChangesAsync();

            return this.attachmentDTOMapper.MapFrom(attachment);
        }
    }
}
