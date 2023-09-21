using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewModels
{
    public class SignUpViewModel
    {
        [Display(Name = "Istifadəçi adı: ")]
        [Required(ErrorMessage = "Istifadəçi adı boş ola bilməz")]
        public string UserName { get; set; }


        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Email boş ola bilməz")]
        [EmailAddress(ErrorMessage = "Email formatı yanlışdır")]
        public string Email { get; set; }


        [Display(Name = "Telefon: ")]
        [Required(ErrorMessage = "Telefon ola bilməz")]
        public string Phone { get; set; }


        [Display(Name = "Parol: ")]
        [Required(ErrorMessage = "Parol boş ola bilməz")]
        public string Password { get; set; }


        [Display(Name = "Təkrar parol: ")]
        [Required(ErrorMessage = "Təkrar parol boş ola bilməz")]
        [Compare(nameof(Password), ErrorMessage = "Parol eyni deyildir")]
        public string PasswordConfirm { get; set; }
    }
}
