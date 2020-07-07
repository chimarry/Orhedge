using ServiceLayer.Models;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Enables user to log in.
        /// </summary>
        /// <param name="loginRequest">Must be not null value</param>
        /// <returns>Proper response after logging in</returns>
        Task<LoginResponse> Login(LoginRequest loginRequest);
    }
}
