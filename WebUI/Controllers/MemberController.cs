using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
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
        private readonly IFileProvider fileProvider;
        public MemberController(SignInManager<AppUser> signInManager,
                                UserManager<AppUser> userManager,
                                IFileProvider fileProvider)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.fileProvider = fileProvider;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);

            UserViewModel viewModel = new UserViewModel()
            {
                Email = currentUser.Email,
                PhoneNumber = currentUser.PhoneNumber,
                UserName = currentUser.UserName,
                PictureUrl = currentUser.Picture
            };
            return View(viewModel);
        }



        public async Task<IActionResult> UserEdit()
        {
            ViewBag.GenderList = new SelectList(Enum.GetNames(typeof(Gender)));

            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);

            UserEditViewModel viewModel = new()
            {
                UserName = currentUser.UserName,
                Email = currentUser.Email,
                Phone = currentUser.PhoneNumber,
                BirthDate = currentUser.BirthDate,
                City = currentUser.City,
                Gender = currentUser.Gender
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);

            currentUser.UserName = request.UserName;
            currentUser.Email = request.Email;
            currentUser.PhoneNumber = request.Phone;
            currentUser.BirthDate = request.BirthDate;
            currentUser.Gender = request.Gender;
            currentUser.City = request.City;


            if (request.Picture != null && request.Picture.Length > 0)
            {
                var wwwrootFolder = fileProvider.GetDirectoryContents("wwwroot");

                var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension((request.Picture.FileName))}";

                var newPicturePath = Path.Combine(wwwrootFolder.First(x => x.Name == "userpictures").PhysicalPath, randomFileName);

                using var stream = new FileStream(newPicturePath, FileMode.Create);

                await request.Picture.CopyToAsync(stream);

                currentUser.Picture = randomFileName;
            }

            var updateToUserResult = await userManager.UpdateAsync(currentUser);

            if (!updateToUserResult.Succeeded)
            {
                ModelState.AddModelErrorList(updateToUserResult.Errors);
                return View();
            }

            await userManager.UpdateSecurityStampAsync(currentUser);
            await signInManager.SignOutAsync();
            await signInManager.SignInAsync(currentUser, true);

            TempData["SuccessedMessage"] = "Məlumatlar uğurla yeniləndi";

            return RedirectToAction("useredit", request);
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

        public IActionResult AccessDenied(string ReturnUrl)
        {
            
            return View();
        }
    }
}
