using System.ComponentModel.DataAnnotations;

namespace Orhedge.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Korisničko ime nije uneseno")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Lozinka nije unesena")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
