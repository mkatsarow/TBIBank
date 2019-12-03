using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TBIApp.Data.Models;
using TBIBankApp.Mappers.Contracts;
using TBIBankApp.Models;
using TBIApp.Services.Services.Contracts;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using TBIBankApp.Hubs;

namespace TBIBankApp.Controllers
{
    public class HomeController : Controller
    {

        static int logedInUsersCount = 0;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly IStatisticsService statisticsService;
        private readonly IReportDiagramViewModelMapper reportDiagramViewModelMapper;
        private readonly ILogger<HomeController> logger;
        private readonly IHubContext<NotificationHub> hubContext;



        public HomeController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ILogger<HomeController> logger,
            IUserService userService,
            IStatisticsService statisticsService,
            IReportDiagramViewModelMapper reportDiagramViewModelMapper,
            IHubContext<NotificationHub> hubContext)
        {
            this.hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.statisticsService = statisticsService ?? throw new ArgumentNullException(nameof(statisticsService));
            this.reportDiagramViewModelMapper = reportDiagramViewModelMapper;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Index()
        {
            await Task.Delay(0);
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Dashboard");
                }

            }
            catch (Exception ex)
            {
                this.logger.LogError("User is not authenticated!", ex);
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {

            var modelDTO = await statisticsService.GetStatisticsAsync();

            var vm = this.reportDiagramViewModelMapper.MapFrom(modelDTO);

            return View(vm);



        }

        [HttpPost]
        public async Task<IActionResult> CheckForUserNameAndPassowrdAsync(LoginViewModel Input)
        {
            try
            {
                var passValidation = await this.userService.ValidateCredentialAsync(Input.UserName, Input.Password);

                if (!passValidation)
                {
                    return new JsonResult("false");
                }

                var user = await userManager.FindByNameAsync(Input.UserName);

                if (user.IsChangedPassword && passValidation)
                {
                    if (logedInUsersCount >= 30)
                    {
                        return new JsonResult("maxlogedusers");
                    }
                    logedInUsersCount += 1;
                    await signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                    await this.userService.SetOnlineStatusOn(user.Id);
                    var count = await this.userService.UpdatedEmailsCountAsync(user);
                    await this.hubContext.Clients.All.SendAsync("UpdateOnline", user,count);

                    return new JsonResult("true");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Invalid credential. User missmatch his password", ex);
            }

            return View("ChangePassword", Input);

        }

        [HttpPost]
        public async Task SetNewPasswordAsync(string UserName, string currPassword, string newPassword)
        {
            try
            {
                var user = await userManager.FindByNameAsync(UserName);

                user.IsChangedPassword = true;

                await userManager.ChangePasswordAsync(user, currPassword, newPassword);

                await signInManager.PasswordSignInAsync(UserName, newPassword, false, lockoutOnFailure: false);

                await this.userService.SetOnlineStatusOn(user.Id);

                logedInUsersCount += 1;
            }
            catch (Exception ex)
            {

                this.logger.LogError($"Trying to change password with ivnalid credential for user - {UserName} at {DateTime.Now}.", ex);
            }

        }

        public async Task<IActionResult> PageNotFound()
        {
            await Task.Delay(0);
            return View();
        }

        [HttpGet]
        public async Task LogOutAsync()
        {
            var user = await this.userManager.GetUserAsync(User);

            logedInUsersCount -= 1;

            await this.userService.SetOnlineStatusOff(user.Id);

            await this.hubContext.Clients.All.SendAsync("LogOut", user.UserName);

            await signInManager.SignOutAsync();
        }
    }
}
