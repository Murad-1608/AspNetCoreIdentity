using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Areas.Admin.Models;
using WebUI.Extensions;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;

        public RolesController(RoleManager<AppRole> roleManager,
                               UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await roleManager.Roles.Select(x => new RoleViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return View(roles);
        }

        public IActionResult RoleCreate()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleCreateViewModel request)
        {
            var result = await roleManager.CreateAsync(new AppRole { Name = request.Name });

            if (!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors);
                return View();
            }
            TempData["SuccessedMessage"] = "Rol əlavə olundu";

            return RedirectToAction("index");
        }

        public async Task<IActionResult> RoleUpdate(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                throw new Exception("Yenilənəcək rol tapılmadı");
            }


            RoleUpdateViewModel viewModel = new()
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RoleUpdate(RoleUpdateViewModel request)
        {
            var role = await roleManager.FindByIdAsync(request.Id);

            role.Name = request.Name;

            var result = await roleManager.UpdateAsync(role);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors);
                return View();
            }
            TempData["SuccessedMessage"] = "Rol yeniləndi";
            return RedirectToAction("index");

        }

        public async Task<IActionResult> RoleDelete(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                throw new Exception("Silinəcək rol tapılmadı");
            }

            var result = await roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(x => x.Description).First());
            }
            TempData["SuccessedMessage"] = "Rol silindi";
            return RedirectToAction("index");
        }
    }
}
