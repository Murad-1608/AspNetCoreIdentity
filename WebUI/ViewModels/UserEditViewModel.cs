using System.ComponentModel.DataAnnotations;
using WebUI.Models;

namespace WebUI.ViewModels
{
    public class UserEditViewModel
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

        [Display(Name = "Doğum tarixi: ")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Şəhər: ")]
        public string? City { get; set; }

        [Display(Name = "Profil şəkili: ")]
        public IFormFile? Picture { get; set; }

        [Display(Name = "Cinsiyyət: ")]
        public Gender? Gender { get; set; }
    }
}
