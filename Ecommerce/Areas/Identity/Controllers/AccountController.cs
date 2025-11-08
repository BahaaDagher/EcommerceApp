using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Threading.Tasks;

namespace Ecommerce.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender; 

        public AccountController(UserManager<ApplicationUser> userManager ,SignInManager<ApplicationUser> signInManager , IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            ApplicationUser user = new ApplicationUser()
            {
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
            };  
            var result = await _userManager.CreateAsync(user , registerVM.Password); 
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registerVM);
            }
            var token  = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action(nameof(ConfirmEmail) , "Account" ,new {Area = "Identity" ,token, userId = user.Id} , Request.Scheme);
            await _emailSender.SendEmailAsync(registerVM.Email, "Ecommerce 520 Confirm Email",
                $"<h1> confirm your email by clicking <a href='{link}'> here</a>  </h1>"); 
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> ConfirmEmail(string token , string userId)
        {
            var  user  = await _userManager.FindByIdAsync(userId);
            if (user is null )
            {
                TempData["Error"] = "Invalid User";
            }
            var result  = await _userManager.ConfirmEmailAsync(user , token);
            if (!result.Succeeded)
            {
                TempData["Error"] = "Email Confirmation Failed";
            }
            else
            {
                TempData["Success"] = "Email Confirmed Successfully";
            }
             return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }
            var user  =  await _userManager.FindByNameAsync(loginVm.UserNameOrEmail) ?? await _userManager.FindByEmailAsync(loginVm.UserNameOrEmail);
            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                return View(loginVm);
            }
            //await _userManager.CheckPasswordAsync(user , loginVm.Password); 
            var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password , loginVm.RememberMe , true);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Your account is locked out. Please try again later.");
                    return View(loginVm);
                }
                else if (!user.EmailConfirmed)
                {
                    ModelState.AddModelError(string.Empty, "You need to confirm your email before logging in.");
                    return View(loginVm);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                    return View(loginVm);
                }
            }
            return RedirectToAction("Index" , "Home" , new { area = "Customer" });
        }
        public IActionResult ResendEmailConfirmation()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResendEmailConfirmation( ResendEmailConfirmationVM resendEmailConfirmationVM)
        {
            var user  =  await _userManager.FindByNameAsync(resendEmailConfirmationVM.UserNameOrEmail) ?? await _userManager.FindByEmailAsync(resendEmailConfirmationVM.UserNameOrEmail);
            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Invalid UserName Or Email");
                return View(resendEmailConfirmationVM);
            }
            if (user.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, "Email is already confirmed");
                return View(resendEmailConfirmationVM);
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action(nameof(ConfirmEmail), "Account", new { Area = "Identity", token, userId = user.Id }, Request.Scheme);
            await _emailSender.SendEmailAsync(user.Email, "Ecommerce 520 Confirm Email",
                $"<h1> confirm your email by clicking <a href='{link}'> here</a>  </h1>");
            return RedirectToAction("Login");

        }

    }
}
