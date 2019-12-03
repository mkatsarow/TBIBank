using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBIApp.Services.Models;
using TBIBankApp.Mappers.Contracts;
using TBIBankApp.Models.LoanApplication;

namespace TBIBankApp.Mappers
{
    public class ApplicationViewModelMapper : IApplicationViewModelMapper
    {
        public LoanApplicationDTO MapFrom(LoanApplicationViewModel entity)
        {
            return new LoanApplicationDTO()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                EGN = entity.EGN,
                Status = entity.Status,
                CardId = entity.CardId,
                PhoneNumber = entity.PhoneNumber,
                EmailId = entity.EmailId,
            };
        }
        public LoanApplicationViewModel MapFrom(LoanApplicationDTO entity)
        {
            return new LoanApplicationViewModel()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                EGN = entity.EGN,
                Status = entity.Status,
                CardId = entity.Id,
                PhoneNumber = entity.PhoneNumber,
                EmailId = entity.EmailId,

            };
        }
        public IList<LoanApplicationViewModel> MapFrom(ICollection<LoanApplicationDTO> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

        public IList<LoanApplicationDTO> MapFrom(ICollection<LoanApplicationViewModel> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }


    }
}
