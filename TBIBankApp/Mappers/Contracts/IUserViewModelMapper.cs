using System.Collections.Generic;
using TBIApp.Services.Models;
using TBIBankApp.Models;

namespace TBIBankApp.Mappers.Contracts
{
    public interface IUserViewModelMapper
    {
        ICollection<UserViewModel> MapFrom(ICollection<UserDTO> entities);
        ICollection<UserDTO> MapFrom(ICollection<UserViewModel> entities);
        UserViewModel MapFrom(UserDTO entity);
        UserDTO MapFrom(UserViewModel entity);
    }
}