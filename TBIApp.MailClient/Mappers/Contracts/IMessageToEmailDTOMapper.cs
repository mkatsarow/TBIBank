using Google.Apis.Gmail.v1.Data;
using TBIApp.Services.Models;

namespace TBIApp.MailClient.Mappers.Contracts
{
    public interface IMessageToEmailDTOMapper
    {
        EmailDTO MapToDTO(Message email);
    }
}