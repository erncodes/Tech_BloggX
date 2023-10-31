using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Techblog.Models.ViewModels;

namespace Techblog.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController( UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {
                var identityuser = new IdentityUser
                {
                    UserName = registerViewModel.Username,
                    Email = registerViewModel.Email,
                };
                var identityResult = await userManager.CreateAsync(identityuser, registerViewModel.Password);
                if (identityResult.Succeeded)
                {
                    //Assign this new user a user role
                    var roleIdentityResult = await userManager.AddToRoleAsync(identityuser, "User");
                    if (roleIdentityResult.Succeeded)
                    {
                        return RedirectToAction("Register");
                    }
                }
            }
            return View();
            
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
           return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var signInM = await signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);

                if (signInM != null && signInM.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                    {
                        return Redirect(loginViewModel.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
          
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
