using System.ComponentModel.DataAnnotations;

namespace GymApp.Models
{
    public class UserRoleModel
    {
        [Key]
        public string UserID { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
