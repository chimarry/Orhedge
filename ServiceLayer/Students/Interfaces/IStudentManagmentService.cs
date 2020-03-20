using ServiceLayer.DTO.Registration;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Interfaces
{
    public interface IStudentManagmentService
    {
        Task GenerateRegistrationEmail(RegisterFormDTO registerFormData);

        Task<bool> IsStudentRegistered(string email);

        Task<bool> ValidateRegistrationCode(string code);

        Task RegisterStudent(RegisterUserDTO registerData);
        Task RegisterRootUser(RegisterRootDTO rootData);
    }
}
