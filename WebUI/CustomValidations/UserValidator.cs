using Microsoft.AspNetCore.Identity;
using WebUI.Models;

namespace WebUI.CustomValidations
{
    public class UserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            List<IdentityError> errors = new List<IdentityError>();

            var isDigit = int.TryParse(user.UserName[0].ToString(), out _);

            if (isDigit)
            {
                errors.Add(new() { Code = "UserNameContainFirstLetterDigit", Description = "Isifadəçi adında ilk simvol rəqəm ola bilməz" });
            }

            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
