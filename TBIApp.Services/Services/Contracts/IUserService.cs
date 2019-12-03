using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TBIApp.Data.Models;
using TBIApp.Services.Models;

namespace TBIApp.Services.Services.Contracts
{
    public interface IUserService
    {
        Task ChangeLastLoginAsync(User user);
        Task<RegisterDTO> CreateAsync(RegisterDTO registerDTO);
        Task<bool> CheckForEmailAsync(string email);
        Task<bool> CheckForUserNameAsync(string userName);
        Task<bool> CheckForPasswordAsync(string password);
        Task<bool> ValidateCredentialAsync(string username, string password);
        Task<bool> SetOnlineStatusOff(string userId);
        Task<bool> SetOnlineStatusOn(string userId);
        Task<int> UpdatedEmailsCountAsync(User user);
    }
}
