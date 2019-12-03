using System.Collections.Generic;
using System.Linq;
using TBIApp.Data.Models;
using TBIApp.Services.Mappers.Contracts;
using TBIApp.Services.Models;

namespace TBIApp.Services.Mappers
{
    public class LoanApplicationDTOMapper : ILoanApplicationDTOMapper
    {
        public LoanApplicationDTO MapFrom(LoanApplication entity)
        {
            return new LoanApplicationDTO()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                EGN = entity.EGN,
                Status = entity.Status,
                CardId = entity.CardId,
                PhoneNumber = entity.PhoneNumber,
                EmailId = entity.EmailId,
            };
        }
        public LoanApplication MapFrom(LoanApplicationDTO entity)
        {
            return new LoanApplication()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                EGN = entity.EGN,
                Status = entity.Status,
                CardId = entity.CardId,
                PhoneNumber = entity.PhoneNumber,
                EmailId = entity.EmailId,

            };
        }
        public IList<LoanApplicationDTO> MapFrom(ICollection<LoanApplication> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

        public IList<LoanApplication> MapFrom(ICollection<LoanApplicationDTO> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }


    }
}
