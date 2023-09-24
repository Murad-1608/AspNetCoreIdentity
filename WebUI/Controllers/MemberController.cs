using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Extensions;
using WebUI.Models;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        public MemberController(SignInManager<AppUser> signInManager,
                                UserManager<AppUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);

            UserViewModel viewModel = new UserViewModel()
            {
                Email = currentUser.Email,
                PhoneNumber = currentUser.PhoneNumber,
                UserName = currentUser.UserName
            };
            return View(viewModel);
        }

        public async Task<IActionResult> PasswordChange()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);

            var checkPassword = await userManager.CheckPasswordAsync(currentUser, request.PasswordOld);

            if (!checkPassword)
            {
                ModelState.AddModelError(string.Empty, "İndiki parolu düzgün qeyd edin");
                return View();
            }

            var result = await userManager.ChangePasswordAsync(currentUser, request.PasswordOld, request.PasswordNew);

            if (!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors.Select(x => x.Description).ToList());
                return View();
            }

            await userManager.UpdateSecurityStampAsync(currentUser);
            await signInManager.SignOutAsync();
            await signInManager.PasswordSignInAsync(currentUser, request.PasswordNew, true, false);

            TempData["SuccessedMessage"] = "Parol uğurla dəyişdirilmişdir";
            return RedirectToAction("passwordchange");

        }
        public async Task LogOut()
        {
            await signInManager.SignOutAsync();
        }
    }
}
