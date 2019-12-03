using System.Collections.Generic;
using TBIApp.Services.Models;
using TBIBankApp.Models.LoanApplication;

namespace TBIBankApp.Mappers.Contracts
{
    public interface IApplicationViewModelMapper
    {
        IList<LoanApplicationViewModel> MapFrom(ICollection<LoanApplicationDTO> entities);
        IList<LoanApplicationDTO> MapFrom(ICollection<LoanApplicationViewModel> entities);
        LoanApplicationViewModel MapFrom(LoanApplicationDTO entity);
        LoanApplicationDTO MapFrom(LoanApplicationViewModel entity);
    }
}