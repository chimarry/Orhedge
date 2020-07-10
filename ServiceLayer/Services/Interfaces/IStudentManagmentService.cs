using ServiceLayer.DTO;
using ServiceLayer.DTO.Student;
using ServiceLayer.ErrorHandling;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{

    /// <summary>
    /// Statuses that roughly indicate result of password changing
    /// </summary>
    public enum PassChangeStatus { Success, InvalidOldPass, PassNoMatch, Error };

    public interface IStudentManagmentService
    {
        /// <summary>
        /// Generates and sends registration email on address that student speficied.
        /// </summary>
        /// <param name="registerFormData">Data related to the student</param>
        /// <returns>True if success, false if not</returns>
        Task<ResultMessage<bool>> GenerateRegistrationEmail(RegisterFormDTO registerFormData);

        /// <summary>
        /// Checks if student with specific email address is registered in application, 
        /// and returns a flag.
        /// </summary>
        /// <param name="email">Email address of the student</param>
        /// <returns>True if student is registered, false if not</returns>
        Task<bool> IsStudentRegistered(string email);

        /// <summary>
        /// Check if student with specified index is registered.
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>True if registered, false if not</returns>
        Task<bool> IsStudentRegisteredWithIndex(string index);

        /// <summary>
        /// Validates registration code used in process of registration.
        /// </summary>
        /// <param name="code">Registration code</param>
        /// <returns>True if valid, false if not</returns>
        Task<bool> ValidateRegistrationCode(string code);

        /// <summary>
        /// Based on spefied data, this method updated registration form, adds student as registered, and finishes registration process.
        /// </summary>
        /// <param name="registerData">Student information needed for registration process completion</param>
        /// <returns></returns>
        Task<ResultMessage<bool>> FinishRegistrationProcess(RegisterUserDTO registerData);

        /// <summary>
        /// Registers root user.
        /// </summary>
        /// <param name="rootData">All information about root user</param>
        Task RegisterRootUser(RegisterRootDTO rootData);

        /// <summary>
        /// Edits student's username, description and profile image.
        /// </summary>
        /// <param name="id">Unique identifier for the student</param>
        /// <param name="profile">Data with updated information</param>
        Task<ResultMessage<StudentDTO>> EditStudentProfile(int id, ProfileUpdateDTO profile);

        /// <summary>
        /// Changes student's password, if all criteria are satisfied - for example old password must match with one in database.
        /// </summary>
        /// <param name="id">Unique identifier of the student</param>
        /// <param name="passwordDTO">Contains new password</param>
        /// <returns></returns>
        Task<PassChangeStatus> UpdateStudentPassword(int id, UpdatePasswordDTO passwordDTO);

        /// <summary>
        /// Checks if given password belongs to specified student.
        /// </summary>
        /// <param name="id">Unique identifier for the student</param>
        /// <param name="password">Password to check</param>
        /// <returns>True if password is valid, false if not</returns>
        Task<bool> ValidatePassword(int id, string password);
    }
}
