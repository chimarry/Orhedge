using ServiceLayer.Models;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
    }
}
