using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TBIApp.Data.Models;
using TBIApp.Services.Services.Contracts;
using TBIBankApp.Hubs;
using TBIBankApp.Mappers.Contracts;
using TBIBankApp.Models.LoanApplication;

namespace TBIBankApp.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailService emailService;
        private readonly IApplicationService applicationService;
        private readonly IApplicationViewModelMapper applicationViewModelMapper;
        private readonly ICheckEgnService checkEgnService;
        private readonly IcheckCardIdService checkCardIdService;
        private readonly ICheckPhoneNumberService checkPhoneNumberService;
        private readonly IEncryptService encryptService;
        private readonly IHubContext<NotificationHub> hubContext;


        public ApplicationController(UserManager<User> userManager, IHubContext<NotificationHub> hubContext,IEmailService emailService,IApplicationService applicationService, IApplicationViewModelMapper applicationViewModelMapper, ICheckEgnService checkEgnService, IcheckCardIdService icheckCardId, ICheckPhoneNumberService checkPhoneNumber, IEncryptService encryptService)
        {
            this.hubContext = hubContext;
            this.userManager = userManager;
            this.emailService = emailService;
            this.applicationService = applicationService;
            this.applicationViewModelMapper = applicationViewModelMapper;
            this.checkEgnService = checkEgnService;
            this.checkCardIdService = icheckCardId;
            this.checkPhoneNumberService = checkPhoneNumber;
            this.encryptService = encryptService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost] 
        //[ValidateAntiForgeryToken]
        public async Task<string> CreateAsync(LoanApplicationViewModel vm)
        {
            if (!ModelState.IsValid)
            {

                throw new ArgumentException("Invalid application VM!");
            }
            //logger logsmth

            var isRealEgn = await checkEgnService.IsRealAsync(vm.EGN);
            if(!isRealEgn)
            {
                return "false egn";
            }
            var isRealPhoneNumber = await checkPhoneNumberService.IsRealAsync(vm.PhoneNumber);
            if(!isRealPhoneNumber)
            {
                return "false phonenumber";
            }
            var isRealCardId = await checkCardIdService.IsRealAsync(vm.CardId);
            if(!isRealCardId)
            {
                return "false cardId";
            }
            vm.CardId = encryptService.EncryptString(vm.CardId);
            vm.LastName = encryptService.EncryptString(vm.LastName);
            vm.EGN = encryptService.EncryptString(vm.EGN);
            var application = this.applicationViewModelMapper.MapFrom(vm);
            await this.applicationService.CreateAsync(application);


            //Redirect to smth
            return "true";
        }
        [HttpGet]
        public async Task ChangeStatusAsync(string id, string appStatus)
        {
            try
            {
                var currentUser = await this.userManager.GetUserAsync(User);

                await applicationService.ChangeStatusAsync(id, appStatus);
                await emailService.ChangeStatusAsync(id, EmailStatusesEnum.Closed, currentUser);
                await this.hubContext.Clients.All.SendAsync("UpdateChart", "Closed","Open");

            }
            catch
            {
                throw new Exception();
            }
        }
    }
}