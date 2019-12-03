using System.Collections.Generic;
using System.Threading.Tasks;
using TBIApp.Data.Models;
using TBIApp.Services.Models;

namespace TBIApp.Services.Services.Contracts
{
    public interface IEmailService
    {
        Task<EmailDTO> CreateAsync(EmailDTO emailDTO);
        Task<ICollection<EmailDTO>> GetAllAsync(int page);
        Task<ICollection<EmailDTO>> GetCurrentPageEmailsAsync(int page, EmailStatusesEnum typeOfEmail, User user);
        Task ChangeStatusAsync(string emailId, EmailStatusesEnum newEmaiLStatus, User currentUser);
        Task<int> GetEmailsPagesByTypeAsync(EmailStatusesEnum statusOfEmail);
        Task<int> GetAllEmailsPagesAsync();
        Task<bool> IsOpenAsync(string id);
        Task LockButtonAsync(string id);
        Task UnLockButtonAsync(string id);
        Task<Email> GetEmailAsync(string id);
    }
}