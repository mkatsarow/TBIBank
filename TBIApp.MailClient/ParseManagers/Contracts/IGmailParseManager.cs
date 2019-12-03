using System.Collections.Generic;
using Google.Apis.Gmail.v1.Data;
using TBIApp.Services.Models;

namespace TBIApp.MailClient.ParseManagers.Contracts
{
    public interface IGmailParseManager
    {
        Dictionary<string, string> GetHeaders(Message email);
        ICollection<AttachmentDTO> GetAttachments(Message email);
        string GetHtmlBody(Message email);
    }
}