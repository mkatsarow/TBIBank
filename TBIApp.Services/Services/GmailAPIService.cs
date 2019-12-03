using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TBIApp.Data.Models;
using TBIApp.Services.Models;
using TBIApp.Services.Services.Contracts;



namespace TBIApp.Services.Services
{
    public class GmailAPIService : IGmailAPIService
    {
        private readonly IEmailService emailService;

        public GmailAPIService(IEmailService emailService)
        {
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "Gmail API .NET Quickstart";

        public async Task SyncEmails()
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

            var service = new GmailService(new BaseClientService.Initializer()
            {

                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });


            var emailListResponse = await GetNewEmails(service);

            if (emailListResponse != null && emailListResponse.Messages != null)
            {

                foreach (var email in emailListResponse.Messages)
                {
                    var emailInfoRequest = service.Users.Messages.Get("ivomishotelerik@gmail.com", email.Id);

                    var emailInfoResponse = await emailInfoRequest.ExecuteAsync();


                    if (emailInfoResponse != null)
                    {

                        string dateRecieved = emailInfoResponse.Payload.Headers
                            .FirstOrDefault(x => x.Name == "Date")
                            .Value;

                        string sender = emailInfoResponse.Payload.Headers
                           .FirstOrDefault(x => x.Name == "From")
                           .Value;

                        //sender = ParseSender(sender);

                        string subject = emailInfoResponse.Payload.Headers
                            .FirstOrDefault(x => x.Name == "Subject")
                            .Value;

                        //Body
                        var str = new StringBuilder();
                        var itemToResolve = emailInfoResponse.Payload.Parts[0];

                        if (itemToResolve.MimeType == "text/plain")
                        {
                            //str.Append(DecodeBody(itemToResolve));
                            str.Append(itemToResolve.Body.Data);
                        }
                        else
                        {
                            //str.Append(DecodeBody(itemToResolve.Parts[0]));
                            str.Append(itemToResolve.Parts[0].Body.Data);

                        }

                        //Body
                        string body = str.ToString();

                        ICollection<AttachmentDTO> attachmentsOfEmail = new List<AttachmentDTO>();

                        if (!(itemToResolve.MimeType == "text/plain"))
                        {
                            attachmentsOfEmail = ParseAttachments(emailInfoResponse);
                        }

                        var emailDTO = new EmailDTO
                        {
                            RecievingDateAtMailServer = dateRecieved,
                            Sender = sender,
                            Subject = subject,
                            Body = body,
                            Attachments = attachmentsOfEmail,
                            Status = EmailStatusesEnum.NotReviewed
                        };

                        await emailService.CreateAsync(emailDTO);



                    }
                }
            }
        }

        public ICollection<AttachmentDTO> ParseAttachments(Message emailInfoResponse)
        {
            IList<AttachmentDTO> result = new List<AttachmentDTO>();

            foreach (var attachment in emailInfoResponse.Payload.Parts.Skip(1))
            {
                var attachmentName = attachment.Filename;
                var attachmentSize = double.Parse(attachment.Body.Size.Value.ToString());

                var attachmentDTO = new AttachmentDTO
                {
                    FileName = attachmentName,
                    SizeKb = Math.Round(attachmentSize / 1024, 2),
                    SizeMb = Math.Round(attachmentSize / 1024 / 1024, 2)
                };

                result.Add(attachmentDTO);
            }

            return result;

        }

        public string DecodeBody(MessagePart message)
        {
            string codedBody = message.Body.Data.Replace("-", "+");
            codedBody = codedBody.Replace("_", "/");
            byte[] data = Convert.FromBase64String(codedBody);
            var result = Encoding.UTF8.GetString(data);

            return result;
        }

        public async Task<ListMessagesResponse> GetNewEmails(GmailService service)
        {
            var emailListRequest = service.Users.Messages.List("ivomishotelerik@gmail.com");

            emailListRequest.LabelIds = "INBOX";
            emailListRequest.IncludeSpamTrash = false;

            return await emailListRequest.ExecuteAsync();
        }

        public string ParseSender(string sender)
        {
            var index = 0;
            var lastIndex = 0;
            for (int i = 0; i < sender.Length; i++)
            {
                if (sender[i] == '<')
                {
                    index = i + 1;
                }
                if (sender[i] == '>')
                {
                    lastIndex = i;
                }
            }

            var result = sender.Substring(index, lastIndex - index);

            return result;

        }
    }
}
