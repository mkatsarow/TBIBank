using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TBIApp.Data.Models;
using TBIApp.Services.Services.Contracts;
using TBIBankApp.Mappers.Contracts;
using TBIBankApp.Models;

namespace TBIBankApp.Controllers
{
    public class ManagerController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IUserService userService;
        private readonly ILogger<ManagerController> logger;
        private readonly IRegisterViewModelMapper registerViewModelMapper;

        public ManagerController(UserManager<User> userManager, 
                                 SignInManager<User> signInManager, 
                                 IUserService userService, 
                                 ILogger<ManagerController> logger, 
                                 IRegisterViewModelMapper registerViewModelMapper)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.registerViewModelMapper = registerViewModelMapper ?? throw new ArgumentNullException(nameof(registerViewModelMapper));
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RegisterUserAsync(RegisterViewModel Input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var registerDTO = this.registerViewModelMapper.MapFrom(Input);

                    await userService.CreateAsync(registerDTO);
                }
                
            }
            catch (Exception ex)
            {

                this.logger.LogError("Error occurred while registering new user.", ex);
            }

            return Ok();
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CheckForUserAndEmailAsync(UserViewModel user)
        {
            try
            {
                if (await userService.CheckForEmailAsync(user.Email))
                {
                    return new JsonResult("true email");
                }
                if (await userService.CheckForUserNameAsync(user.UserName))
                {
                    return new JsonResult("true user");
                }

            }
            catch (Exception ex)
            {

                this.logger.LogError($"Error occurred while checking for email with id {user.Email}.", ex);
            }

            return new JsonResult("false");
        }
    }
}