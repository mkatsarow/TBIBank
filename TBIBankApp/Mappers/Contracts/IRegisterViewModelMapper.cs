using System.Collections.Generic;
using TBIApp.Services.Models;
using TBIBankApp.Models;

namespace TBIBankApp.Mappers.Contracts
{
    public interface IRegisterViewModelMapper
    {
        ICollection<RegisterViewModel> MapFrom(ICollection<RegisterDTO> entities);
        ICollection<RegisterDTO> MapFrom(ICollection<RegisterViewModel> entities);
        RegisterViewModel MapFrom(RegisterDTO entity);
        RegisterDTO MapFrom(RegisterViewModel entity);
    }
}