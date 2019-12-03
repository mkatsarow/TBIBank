using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBIApp.Services.Models;
using TBIBankApp.Mappers.Contracts;
using TBIBankApp.Models;

namespace TBIBankApp.Mappers
{
    public class UserViewModelMapper : IUserViewModelMapper
    {
        public UserViewModel MapFrom(UserDTO entity)
        {
            return new UserViewModel()
            {
                UserName = entity.UserName,
                Password = entity.Password,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                IsOnline = entity.IsOnline,
                IsChangedPassword = entity.IsChangedPassword,
                LastLogIn = entity.LastLogIn,
                UserEmailsCount = entity.UserEmailsCount

            };
        }
        public UserDTO MapFrom(UserViewModel entity)
        {
            return new UserDTO()
            {
                UserName = entity.UserName,
                Password = entity.Password,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                IsOnline = entity.IsOnline,
                IsChangedPassword = entity.IsChangedPassword,
                LastLogIn = entity.LastLogIn,
                UserEmailsCount = entity.UserEmailsCount

            };
        }
        public ICollection<UserDTO> MapFrom(ICollection<UserViewModel> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

        public ICollection<UserViewModel> MapFrom(ICollection<UserDTO> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }
    }
}
