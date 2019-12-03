using System.Collections.Generic;
using TBIApp.Data.Models;
using TBIApp.Services.Models;

namespace TBIApp.Services.Mappers.Contracts
{
    public interface IUserDTOMapper
    {
        ICollection<UserDTO> MapFrom(ICollection<User> entities);
        ICollection<User> MapFrom(ICollection<UserDTO> entities);
        UserDTO MapFrom(User entity);
        User MapFrom(UserDTO entity);
    }
}