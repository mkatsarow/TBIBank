using System;
using System.Collections.Generic;
using System.Linq;
using TBIApp.Data.Models;
using TBIApp.Services.Mappers.Contracts;
using TBIApp.Services.Models;

namespace TBIApp.Services.Mappers
{
    public class EmailDTOMapper : IEmailDTOMapper
    {
        private readonly IAttachmentDTOMapper attachmentDTOMapper;

        public EmailDTOMapper(IAttachmentDTOMapper attachmentDTOMapper)
        {
            this.attachmentDTOMapper = attachmentDTOMapper ?? throw new ArgumentNullException(nameof(attachmentDTOMapper));
        }

        public Email MapFrom(EmailDTO entity)
        {
            return new Email()
            {
                Id = entity.Id,
                Sender = entity.Sender,
                Subject = entity.Subject,
                GmailEmailId = entity.GmailEmailId,
                Status = entity.Status,
                Attachments = this.attachmentDTOMapper.MapFrom(entity.Attachments),
                LoanApplication = entity.LoanApplication,
                UserId = entity.UserId,
                User = entity.User,
                Body = entity.Body,
                RecievingDateAtMailServer = entity.RecievingDateAtMailServer,
                RegisteredInDataBase = entity.RegisteredInDataBase,
                LastStatusUpdate = entity.LastStatusUpdate,
                IsOpne = entity.IsOpne,
                LastOpen = entity.LastOpen

            };
        }
        public EmailDTO MapFrom(Email entity)
        {
            return new EmailDTO()
            {
                Id = entity.Id,
                Sender = entity.Sender,
                Subject = entity.Subject,
                Status = entity.Status,
                GmailEmailId = entity.GmailEmailId,
                Attachments = this.attachmentDTOMapper
                                  .MapFrom(entity.Attachments) ?? new List<AttachmentDTO>(),
                LoanApplication = entity.LoanApplication,
                UserId = entity.UserId,
                User = entity.User,
                Body = entity.Body,
                RecievingDateAtMailServer = entity.RecievingDateAtMailServer,
                RegisteredInDataBase = entity.RegisteredInDataBase,
                LastStatusUpdate = entity.LastStatusUpdate,
                IsOpne = entity.IsOpne,
                LastOpen = entity.LastOpen,
                LoanApplicationStatus = entity.LoanApplication?.Status.ToString()
                
            };
        }
        public IList<Email> MapFrom(ICollection<EmailDTO> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

        public IList<EmailDTO> MapFrom(ICollection<Email> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }


    }
}
