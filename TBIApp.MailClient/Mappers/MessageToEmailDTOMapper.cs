using System;
using System.Collections.Generic;
using Google.Apis.Gmail.v1.Data;
using Newtonsoft.Json;
using TBIApp.Data.Models;
using TBIApp.MailClient.Mappers.Contracts;
using TBIApp.MailClient.ParseManagers.Contracts;
using TBIApp.Services.Models;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.MailClient.Mappers
{
    public class MessageToEmailDTOMapper : IMessageToEmailDTOMapper
    {
        private readonly IGmailParseManager gmailParseManager;

        public MessageToEmailDTOMapper(IGmailParseManager gmailParseManager)
        {
            this.gmailParseManager = gmailParseManager ?? throw new ArgumentNullException(nameof(gmailParseManager));
        }
        
        public EmailDTO MapToDTO(Message email)
        {
            var headers = this.gmailParseManager.GetHeaders(email);
            var body = this.gmailParseManager.GetHtmlBody(email);
            var attachmentsOfEmail = this.gmailParseManager.GetAttachments(email);

            var emailDTO = new EmailDTO
            {
                RecievingDateAtMailServer = headers["dateRecieved"],
                GmailEmailId = headers["gmailEmailId"],
                Sender = headers["sender"],
                Subject = headers["subject"],
                Body = body,
                Attachments = attachmentsOfEmail,
                Status = EmailStatusesEnum.NotReviewed
            };
            
            return emailDTO;
        }
    }
}
