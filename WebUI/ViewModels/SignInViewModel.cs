using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewModels
{
    public class SignInViewModel
    {
        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Email boş ola bilməz")]
        [EmailAddress(ErrorMessage = "Email formatı yanlışdır")]
        public string Email { get; set; }
        [Display(Name = "Parol: ")]
        [Required(ErrorMessage = "Parol boş ola bilməz")]
        public string Password { get; set; }
        [Display(Name = "Xatırla")]
        public bool RememberMe { get; set; }
    }
}
