using ServiceLayer.DTO;
using ServiceLayer.Helpers;
using ServiceLayer.Models;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IStudentService _studentService;

        public AuthenticationService(IStudentService studentService)
            => _studentService = studentService;

        /// <summary>
        /// Enables user to log in.
        /// </summary>
        /// <param name="loginRequest">Must be not null value</param>
        /// <returns>Null if credentials are not valid, otherwise an instance of LoginResponse</returns>
        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            StudentDTO student = await _studentService.GetSingleOrDefault(st => st.Username == loginRequest.Username);
            if (student == null)
                return null;

            byte[] salt = Convert.FromBase64String(student.Salt);
            string hash = Security.CreateHash(loginRequest.Password, salt, Constants.PASSWORD_HASH_SIZE);

            if (hash == student.PasswordHash)
                return new LoginResponse { Id = student.StudentId, Privilege = student.Privilege };
            else
                return null;
        }
    }
}
