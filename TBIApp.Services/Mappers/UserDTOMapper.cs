using System.Collections.Generic;
using System.Linq;
using TBIApp.Data.Models;
using TBIApp.Services.Mappers.Contracts;
using TBIApp.Services.Models;

namespace TBIApp.Services.Mappers
{
    public class UserDTOMapper : IUserDTOMapper
    {
        public User MapFrom(UserDTO entity)
        {
            return new User()
            {
                UserName = entity.UserName,
                PasswordHash = entity.Password,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                IsOnline = entity.IsOnline,
                IsChangedPassword = entity.IsChangedPassword,
                LastLogIn = entity.LastLogIn,
                

            };
        }
        public UserDTO MapFrom(User entity)
        {
            return new UserDTO()
            {   
                UserName = entity.UserName,
                Password = entity.PasswordHash,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                IsOnline = entity.IsOnline,
                IsChangedPassword = entity.IsChangedPassword,
                LastLogIn = entity.LastLogIn,
                UserEmailsCount = entity.UserEmails != null ? entity.UserEmails.Count : 0,
                
               
                //condition ? consequent : alternative

            };
        }
        public ICollection<UserDTO> MapFrom(ICollection<User> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

        public ICollection<User> MapFrom(ICollection<UserDTO> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }
    }
}
