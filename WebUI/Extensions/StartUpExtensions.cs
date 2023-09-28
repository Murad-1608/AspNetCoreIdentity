using Microsoft.AspNetCore.Identity;
using WebUI.CustomValidations;
using WebUI.Localizations;
using WebUI.Models;

namespace WebUI.Extensions
{
    public static class StartUpExtensions
    {
        public static void AddIdentityWithExt(this IServiceCollection services)
        {

            //Foget password
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromHours(2);
            });

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvwxyz1234567890_";

                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(4);
                options.Lockout.MaxFailedAccessAttempts = 3;


            })
              .AddErrorDescriber<LocalizationIdentityErrorDescriber>()
              .AddUserValidator<UserValidator>()
              .AddPasswordValidator<PasswordValidator>()
              .AddDefaultTokenProviders()
              .AddEntityFrameworkStores<AppDbContext>();


        }
    }
}
