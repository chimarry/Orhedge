using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Orhedge.ViewModels.Student
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "OldPasswordRequired")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "NewPasswordRequired")]
        [Compare(nameof(ConfirmPassword), ErrorMessage = "PassNoMatch")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "ConfirmPasswordRequired")]
        public string ConfirmPassword { get; set; }
    }
}
