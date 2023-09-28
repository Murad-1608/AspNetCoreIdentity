using System.ComponentModel.DataAnnotations;

namespace WebUI.Areas.Admin.Models
{
    public class RoleCreateViewModel
    {
        [Display(Name = "Rol")]
        [Required(ErrorMessage = "Rol boş ola bilməz")]
        public string Name { get; set; }
    }
}
