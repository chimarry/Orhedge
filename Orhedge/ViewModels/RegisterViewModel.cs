using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Orhedge.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [HiddenInput]
        public string RegistrationCode { get; set; } // This should be in hidden input field

    }
}
