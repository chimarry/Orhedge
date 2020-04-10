using ServiceLayer.DTO;
using ServiceLayer.DTO.Student;
using ServiceLayer.ErrorHandling;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{

    public enum PassChangeStatus { Success, InvalidOldPass, PassNoMatch, Error };
    public interface IStudentManagmentService
    {
        Task<ResultMessage<bool>> GenerateRegistrationEmail(RegisterFormDTO registerFormData);

        Task<bool> IsStudentRegistered(string email);

        Task<bool> ValidateRegistrationCode(string code);

        Task<ResultMessage<RegistrationDTO>> RegisterStudent(RegisterUserDTO registerData);
        Task RegisterRootUser(RegisterRootDTO rootData);

        /// <summary>
        /// Edits student username, description and Profile photo
        /// </summary>
        /// <param name="id">Student id</param>
        /// <param name="profile">Edit data</param>
        Task<ResultMessage<StudentDTO>> EditStudentProfile(int id, ProfileUpdateDTO profile);
        Task<PassChangeStatus> UpdateStudentPassword(int id, UpdatePasswordDTO passwordDTO);
        Task<bool> ValidatePassword(int id, string password);
    }
}
