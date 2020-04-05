using System.ComponentModel.DataAnnotations;

namespace Orhedge.ViewModels.Student
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare(nameof(ConfirmPassword))]
        public string ConfirmPassword { get; set; }
    }
}
