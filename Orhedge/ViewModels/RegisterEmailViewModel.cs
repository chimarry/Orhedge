using System.ComponentModel.DataAnnotations;

namespace Orhedge.ViewModels
{
    public class RegisterEmailViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
