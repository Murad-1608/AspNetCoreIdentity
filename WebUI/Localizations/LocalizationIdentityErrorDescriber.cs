using Microsoft.AspNetCore.Identity;

namespace WebUI.Localizations
{
    public class LocalizationIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new()
            {
                Code = "DublicateUserName",
                Description = $"{userName} başqa istifadəçi tərəfindən" +
                $" alınmışdır"
            };
        }


        public override IdentityError DuplicateEmail(string email)
        {
            return new()
            {
                Code = "DublicateEmail",
                Description = $"{email} başqa istifadəçi tərəfindən" +
                $" alınmışdır"
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new()
            {
                Code = "PasswordTooShort",
                Description = $"Parol ən az 6 simvol olmalıdır"
            };
        }
    }
}
