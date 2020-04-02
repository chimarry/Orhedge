using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Orhedge.ViewModels.Student
{
    public class EditProfileViewModel
    {
        [Required(ErrorMessage = "UsernameRequired")]
        public string Username { get; set; }
        [Required(ErrorMessage = "DescriptionRequired")]
        public string Description { get; set; }

        // Used only when transfering data from view to controller
        public IFormFile Photo { get; set; } 
    }
}
