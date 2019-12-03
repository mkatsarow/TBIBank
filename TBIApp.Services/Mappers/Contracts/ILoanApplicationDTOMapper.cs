using System.Collections.Generic;
using TBIApp.Data.Models;
using TBIApp.Services.Models;

namespace TBIApp.Services.Mappers.Contracts
{
    public interface ILoanApplicationDTOMapper
    {
        IList<LoanApplicationDTO> MapFrom(ICollection<LoanApplication> entities);
        IList<LoanApplication> MapFrom(ICollection<LoanApplicationDTO> entities);
        LoanApplicationDTO MapFrom(LoanApplication entity);
        LoanApplication MapFrom(LoanApplicationDTO entity);
    }
}