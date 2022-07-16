using System.ComponentModel.DataAnnotations;

namespace GymApp.Models
{
    public class LoginModel
    {
        [Key]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name="Remember Me")]
        public bool RememberMe { get; set; }
    }
}
