using ServiceLayer.DTO.Registration;
using ServiceLayer.Models;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Interfaces
{
    public interface IStudentManagmentService
    {
        Task GenerateRegistrationEmail(RegisterFormDTO registerFormData);
        Task<bool> IsStudentRegistered(string email);
        Task<bool> ValidateRegistrationCode(string code);
        Task RegisterStudent(RegisterUserDTO registerData);
    }
}
