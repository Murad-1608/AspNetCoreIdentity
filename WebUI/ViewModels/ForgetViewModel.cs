using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewModels
{
    public class ForgetViewModel
    {
        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Email boş ola bilməz")]
        [EmailAddress(ErrorMessage = "Email formatı yanlışdır")]
        public string Email { get; set; }
    }
}
