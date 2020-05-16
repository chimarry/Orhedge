using Microsoft.AspNetCore.Http;
using Orhedge.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Orhedge.ViewModels.Student
{
    public class EditProfileViewModel
    {
        [Required(ErrorMessage = "UsernameRequired")]
        public string Username { get; set; }
        public string Description { get; set; }

        // Used only when transfering data from view to controller
        [PhotoFile("jpg","jpeg", ErrorMessage = "InvalidImageFile")]
        public IFormFile Photo { get; set; } 
    }
}
