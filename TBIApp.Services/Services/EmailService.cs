using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TBIApp.Data;
using TBIApp.Data.Models;
using TBIApp.Services.Mappers.Contracts;
using TBIApp.Services.Models;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.Services.Services
{
    public class EmailService : IEmailService
    {
        private readonly TBIAppDbContext dbcontext;
        private readonly IEmailDTOMapper emailDTOMapper;
        private readonly IDecodeService decodeService;
        private readonly ILogger<EmailService> logger;
        private readonly UserManager<User> userManager;
        private readonly IEncryptService encryptService;

        public EmailService(TBIAppDbContext dbcontext,
                            IEmailDTOMapper emailDTOMapper,
                            IDecodeService decodeService,
                            ILogger<EmailService> logger,
                            UserManager<User> userManager,
        IEncryptService encryptService)
        {
            this.dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            this.emailDTOMapper = emailDTOMapper ?? throw new ArgumentNullException(nameof(emailDTOMapper));
            this.decodeService = decodeService ?? throw new ArgumentNullException(nameof(decodeService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.userManager = userManager;
            this.encryptService = encryptService ?? throw new ArgumentNullException(nameof(encryptService));
        }

        public async Task<EmailDTO> CreateAsync(EmailDTO emailDTO)
        {
            var email = this.emailDTOMapper.MapFrom(emailDTO);

            if (email == null)
            {
                throw new ArgumentNullException();
            }

            email.RegisteredInDataBase = DateTime.Now;

            this.dbcontext.Emails.Add(email);
            await this.dbcontext.SaveChangesAsync();

            return emailDTO;
        }

        public async Task<ICollection<EmailDTO>> GetAllAsync(int page)
        {
            var emails = await this.dbcontext.Emails
                .Skip((page - 1) * 15)
                .Take(150)
                .Include(a => a.Attachments)
                .ToListAsync();

            if (emails == null) throw new ArgumentNullException("No emails found!");

            return this.emailDTOMapper.MapFrom(emails);
        }

        public async Task<ICollection<EmailDTO>> GetCurrentPageEmailsAsync(int page, EmailStatusesEnum typeOfEmail, User user)
        {
            List<Email> emails;
            if (await userManager.IsInRoleAsync(user, "OPERATOR") && (typeOfEmail == EmailStatusesEnum.Open))
            {
                emails = await this.dbcontext.Emails
                    .Where(e => e.Status == typeOfEmail)
                    .OrderByDescending(e => e.RegisteredInDataBase)
                    .Skip((page - 1) * 15)
                    .Take(150)
                    .Include(a => a.Attachments)
                    .Include(e => e.User)
                    .Include(e=>e.LoanApplication)
                    .Where(e=>e.UserId==user.Id)
                    .ToListAsync();
            }
            else
            {
                emails = await this.dbcontext.Emails
                    .Where(e => e.Status == typeOfEmail)
                    .OrderByDescending(e => e.RegisteredInDataBase)
                    .Skip((page - 1) * 15)
                    .Take(150)
                    .Include(a => a.Attachments)
                    .Include(e => e.User)
                    .Include(e => e.LoanApplication)
                    .ToListAsync();
            }
            if (emails == null) throw new ArgumentNullException("No emails found!");

            emails = await DecodeEmails(emails);

            return this.emailDTOMapper.MapFrom(emails);
        }

        public async Task ChangeStatusAsync(string emailId, EmailStatusesEnum newEmaiLStatus, User currentUser)
        {
            var email = await this.dbcontext.Emails.FirstOrDefaultAsync(e => e.Id == emailId);

            if (email == null)
            {
                throw new ArgumentNullException("Email not found!");
            }

            logger.LogInformation($"Status of email with id {emailId} has been updated by {currentUser.Id} at {DateTime.Now}. From status: {email.Status} to status: {newEmaiLStatus}.");

            email.Status = newEmaiLStatus;

            email.LastStatusUpdate = DateTime.Now;

            email.UserId = currentUser.Id;

            email.IsOpne = false;

            this.dbcontext.Emails.Update(email);
            await this.dbcontext.SaveChangesAsync();
        }

        public async Task<List<Email>> DecodeEmails(List<Email> emails)
        {
            var decodeEmails = emails;

            for (int i = 0; i < decodeEmails.Count; i++)
            {
                emails[i].Body = this.encryptService.DecryptString(decodeEmails[i].Body);
                emails[i].Body = await this.decodeService.DecodeAsync(emails[i].Body);
                emails[i].Sender = this.encryptService.DecryptString(decodeEmails[i].Sender);
            }

            return emails;
        }

        public async Task<int> GetEmailsPagesByTypeAsync(EmailStatusesEnum statusOfEmail)
        {
            var totalEmails = await this.dbcontext.Emails.Where(e => e.Status == statusOfEmail).CountAsync();

            return (totalEmails % 15 == 0 ? totalEmails / 15 : totalEmails / 15 + 1);
        }

        public async Task<int> GetAllEmailsPagesAsync()
        {
            var totalEmails = await this.dbcontext.Emails.CountAsync();

            return totalEmails % 15 == 0 ? totalEmails / 15 : totalEmails / 15 + 1;
        }

        public async Task<bool> IsOpenAsync(string id)
        {
            var email = await this.dbcontext.Emails.FirstOrDefaultAsync(e => e.Id == id);

            if (email is null) throw new ArgumentNullException();

            return email.IsOpne;
        }

        public async Task LockButtonAsync(string id)
        {
            var email = await this.dbcontext.Emails.FirstOrDefaultAsync(e => e.Id == id);

            if (email is null) throw new ArgumentNullException();

            email.IsOpne = true;

            await this.dbcontext.SaveChangesAsync();
        }

        public async Task UnLockButtonAsync(string id)
        {
            var email = await this.dbcontext.Emails.FirstOrDefaultAsync(e => e.Id == id);

            if (email is null) throw new ArgumentNullException();

            email.IsOpne = false;

            await this.dbcontext.SaveChangesAsync();
        }

        public async Task<Email> GetEmailAsync(string id)
        {
            return await this.dbcontext.Emails.FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
