using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using TBIApp.MailClient.Client.Contracts;
using TBIApp.MailClient.Mappers.Contracts;
using TBIApp.MailClient.ParseManagers.Contracts;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.MailClient.Client
{
    public class GmailAPIService : IGmailAPIService
    {
        //When you change GmailService.Scope to another, remember to delete credential.json file (which is placed in (TBIBankApp), then remove folder (token.json) placed in (TBIBankAPP) and request new credential from https://developers.google.com/gmail/api/quickstart/dotnet. 

        static string[] Scopes = { GmailService.Scope.GmailModify };
        static string ApplicationName = "Gmail API .NET Quickstart";
        static string gmailAccountName = "ivomishotelerik@gmail.com";

        private readonly IEmailService emailService;
        private readonly IMessageToEmailDTOMapper messageToEmailDTOPmapper;

        public GmailAPIService(IEmailService emailService, IGmailParseManager gmailParseManager, IMessageToEmailDTOMapper messageToEmailDTOPmapper)
        {
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            this.messageToEmailDTOPmapper = messageToEmailDTOPmapper ?? throw new ArgumentNullException(nameof(messageToEmailDTOPmapper));
        }



        public async Task<int> SyncEmails()
        {
            //Create a GmailService which crucial for accecing Gmail API.
            GmailService service = await this.GetServiceAsync();

            //Get all the messages from INBOX which are mark with UNREAD label.
            ListMessagesResponse emailListResponse = await GetNewEmailsAsync(service);

            if (emailListResponse != null && emailListResponse.Messages != null)
            {
                foreach (var email in emailListResponse.Messages)
                {

                    var emailInfoRequest = service.Users.Messages.Get(gmailAccountName, email.Id);

                    //After executeAsync we recieve one email with all his data and attachments. 
                    var currentMessage = await emailInfoRequest.ExecuteAsync();

                    var emailDTO = this.messageToEmailDTOPmapper.MapToDTO(currentMessage);

                    await emailService.CreateAsync(emailDTO);

                    await this.MarkAsReadAsync(service, email.Id);
                }
                return emailListResponse.Messages.Count;
            }
            return 0;
        }

        public async Task<GmailService> GetServiceAsync()
        {
            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential =
                    GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            await Task.Delay(0);

            var service = new GmailService(new BaseClientService.Initializer()
            {

                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }

        //Get all emails from mail client which is mark as UNREAD, if you want to specify additional labels user "SPAM", "URGETNT", "TRASH" or your custom labels.
        //For more detailed information visit https://developers.google.com/gmail/api/guides

        public async Task<ListMessagesResponse> GetNewEmailsAsync(GmailService service)
        {
            var emailListRequest = service.Users.Messages.List(gmailAccountName);

            emailListRequest.LabelIds = new string[] { "UNREAD", "INBOX" };

            emailListRequest.IncludeSpamTrash = false;

            return await emailListRequest.ExecuteAsync();
        }

        //Removes all the labels from email with provided ID and label "UNREAD". 
        public async Task MarkAsReadAsync(GmailService service, string emailId)
        {
            var markAsReadEmail = new ModifyMessageRequest { RemoveLabelIds = new List<string> { "UNREAD" } };

            await service.Users.Messages.Modify(markAsReadEmail, gmailAccountName, emailId).ExecuteAsync();

        }
    }
}
