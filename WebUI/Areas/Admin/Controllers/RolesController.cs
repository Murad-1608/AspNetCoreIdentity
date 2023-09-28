using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RoleCreate(RoleCreateViewModel request)
        {
            var result = await roleManager.CreateAsync(new AppRole { Name = request.Name });

            if (!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors);
                return View();
            }
            return RedirectToAction("index");
        }
    }
}
