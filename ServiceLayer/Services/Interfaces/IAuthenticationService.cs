using ServiceLayer.Models;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
    }
}
