using ServiceLayer.Models;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Interfaces
{
    public interface IStudentManagmentService
    {
        Task GenerateRegistrationEmail(string email);
        Task<bool> IsStudentRegistered(string email);
        Task<bool> ValidateRegistrationCode(string code);
        Task RegisterStudent(RegisterData registerData);
    }
}
