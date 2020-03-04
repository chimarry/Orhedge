using System.ComponentModel.DataAnnotations;

namespace Orhedge.ViewModels
{
    public class RegisterFormViewModel
    {

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"\d+\/\d{2}")]
        public string IndexNumber { get; set; }
        public int Privilege { get; set; }
    }
}
