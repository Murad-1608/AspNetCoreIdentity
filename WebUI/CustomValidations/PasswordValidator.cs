using Microsoft.AspNetCore.Identity;
using WebUI.Models;

namespace WebUI.CustomValidations
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            var errors = new List<IdentityError>();

            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new() { Code = "PasswordContainUserName", Description = "Parolda istifadəçi adınız olmamalıdır" });
            }

            if (password.ToLower().StartsWith("123"))
            {
                errors.Add(new() { Code = "PasswordContain123", Description = "Parol 123 ilə başlaya bilməz" });
            }

            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
