using System.Collections.Generic;
using TBIApp.Data.Models;
using TBIApp.Services.Models;

namespace TBIApp.Services.Mappers.Contracts
{
    public interface IEmailDTOMapper
    {
        EmailDTO MapFrom(Email entity);
        Email MapFrom(EmailDTO entity);
        IList<EmailDTO> MapFrom(ICollection<Email> entities);
        IList<Email> MapFrom(ICollection<EmailDTO> entities);
    }
}