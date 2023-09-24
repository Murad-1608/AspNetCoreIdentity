using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewModels
{
    public class PasswordChangeViewModel
    {
        [Display(Name = "Indiki parol: ")]
        [Required(ErrorMessage = "Indiki parol boş ola bilməz")]
        public string PasswordOld { get; set; }
        [MinLength(6,ErrorMessage ="Minimum uzunluq 6 simvol olmalıdır")]
        [Display(Name = "Yeni parol: ")]
        [Required(ErrorMessage = "Yeni parol boş ola bilməz")]
        public string PasswordNew { get; set; }
        [Display(Name = "Təkrar parol: ")]
        [Required(ErrorMessage = "Təkrar parol boş ola bilməz")]
        [Compare(nameof(PasswordNew), ErrorMessage = "Parol eyni deyildir")]
        public string PasswordConfirm { get; set; }
    }
}
