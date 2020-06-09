using System.ComponentModel.DataAnnotations;

namespace Orhedge.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

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
        [RegularExpression(@"\d+\/\d{2}")]
        public string Index { get; set; }

        [Required]
        public string RegistrationCode { get; set; } // This should be in hidden input field

    }
}
