using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymApp.Models
{
    public class EditRoleModel
    {
        public EditRoleModel()
        {
            Users = new List<string>();
        }
        [Key]
        public string Id { get; set; }
        [Display(Name ="Role Name")]
        [Required(ErrorMessage ="Role Name is required!")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; }

    }
}
