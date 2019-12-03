using System.Collections.Generic;
using TBIApp.Services.Models;
using TBIBankApp.Models.Emails;

namespace TBIBankApp.Mappers.Contracts
{
    public interface IEmailViewModelMapper
    {
        EmailViewModel MapFrom(EmailDTO entity);
        EmailDTO MapFrom(EmailViewModel entity);
        IList<EmailViewModel> MapFrom(ICollection<EmailDTO> entities);
        IList<EmailDTO> MapFrom(ICollection<EmailViewModel> entities);
    }
}