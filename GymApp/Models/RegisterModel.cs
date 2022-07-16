using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GymApp.Models
{
    public class RegisterModel
    {
        [Key]
        [Required]
        [EmailAddress]
        [Remote(action:"IsEmailInUse",controller:"Account")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        //validates that password and confirm password have the same input
        public string ConfirmPassword { get; set; }
    }
}
