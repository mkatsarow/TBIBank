using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using System.Threading.Tasks;

namespace TBIApp.MailClient.Client.Contracts
{
    public interface IGmailAPIService
    {
        Task<int> SyncEmails();
        Task<GmailService> GetServiceAsync();
        Task<ListMessagesResponse> GetNewEmailsAsync(GmailService service);
        Task MarkAsReadAsync(GmailService service, string emailId);
    }
}
