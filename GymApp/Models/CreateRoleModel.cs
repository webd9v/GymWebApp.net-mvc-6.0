using System.ComponentModel.DataAnnotations;

namespace GymApp.Models
{
    public class CreateRoleModel
    {
        [Key]
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
