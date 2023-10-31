using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Techblog.Models.ViewModels;
using Techblog.Repositories;

namespace Techblog.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminUserController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> userManager;

        public AdminUserController(IUserRepository userRepository, UserManager<IdentityUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await userRepository.GetAll();
            var usersViewModel = new UserViewModel();
            usersViewModel.Users = new List<User>();

            foreach (var user in users) 
            {
                usersViewModel.Users.Add(new Models.ViewModels.User
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    EmailAddress = user.Email
                });
            }
            return View(usersViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> List(UserViewModel request)
        {
            var identityUser = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email
            };

            var identityresult = await userManager.CreateAsync(identityUser, request.Password);

            if(identityresult is not null)
            {
                if(identityresult.Succeeded)
                {
                    var roles = new List<string> { "User" };

                    if(request.adminCheckBox)
                    {
                        roles.Add("Admin");
                    }

                   identityresult = await userManager.AddToRolesAsync(identityUser, roles);

                    if(identityresult is not null && identityresult.Succeeded)
                    {
                        return RedirectToAction("List", "AdminUser");
                    }
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
           var user = await userManager.FindByIdAsync(Id.ToString());

            if( user is not null)
            {
                var identityresult = await userManager.DeleteAsync(user);

                if(identityresult is not null && identityresult.Succeeded)
                {
                    return RedirectToAction("List", "AdminUser");
                }
            }
            return View();
        }
    }
}
