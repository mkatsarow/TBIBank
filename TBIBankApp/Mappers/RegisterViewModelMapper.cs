using System.Collections.Generic;
using System.Linq;
using TBIApp.Services.Models;
using TBIBankApp.Mappers.Contracts;
using TBIBankApp.Models;

namespace TBIBankApp.Mappers
{
    public class RegisterViewModelMapper : IRegisterViewModelMapper
    {
        public RegisterDTO MapFrom(RegisterViewModel entity)
        {
            return new RegisterDTO()
            {
                UserName = entity.UserName,
                Password = entity.Password,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Role = entity.Role
            };
        }
        public RegisterViewModel MapFrom(RegisterDTO entity)
        {
            return new RegisterViewModel()
            {
                UserName = entity.UserName,
                Password = entity.Password,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Role = entity.Role

            };
        }
        public ICollection<RegisterDTO> MapFrom(ICollection<RegisterViewModel> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

        public ICollection<RegisterViewModel> MapFrom(ICollection<RegisterDTO> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

    }
}
