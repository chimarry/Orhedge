using System;
using System.Threading.Tasks;
using ServiceLayer.Common;
using ServiceLayer.DTO;
using ServiceLayer.Models;
using ServiceLayer.Students.Interfaces;
using ServiceLayer.Utilities;

namespace ServiceLayer.Students.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IStudentService _studentService;

        public AuthenticationService(IStudentService studentService)
            => _studentService = studentService;


        /// <summary>
        /// Performs login
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>null if credentials are not valid, otherwise a instance of LoginResponse</returns>
        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            StudentDTO student = await _studentService.GetSingleOrDefault(st => st.Username == loginRequest.Username);
            if (student == null)
                return null;

            byte[] salt = Convert.FromBase64String(student.Salt);
            string hash = Crypto.CreateHash(loginRequest.Password, salt, Constants.PASSWORD_HASH_SIZE);

            if (hash == student.PasswordHash)
                return new LoginResponse { Id = student.StudentId, Privilege = student.Privilege };         
            else
                return null;
        }
    }
}
