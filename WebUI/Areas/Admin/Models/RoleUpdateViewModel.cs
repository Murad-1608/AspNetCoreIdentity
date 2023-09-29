using System.ComponentModel.DataAnnotations;

namespace WebUI.Areas.Admin.Models
{
    public class RoleUpdateViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "Rol boş ola bilməz")]
        public string Name { get; set; }
    }
}
