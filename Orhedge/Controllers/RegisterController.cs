using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Orhedge.Helpers;
using Orhedge.ViewModels;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Orhedge.Controllers
{
    public class RegisterController : Controller
    {

        private readonly IStudentManagmentService _studentManagmentService;
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;

        public RegisterController(IStudentManagmentService studentManagmentService, IMapper mapper, IStudentService studentService)
            => (_studentManagmentService, _mapper, _studentService) = (studentManagmentService, mapper, studentService);

        public async Task<IActionResult> ShowRegisterForm([FromQuery] string code)
        {
            bool codeValid = await _studentManagmentService.ValidateRegistrationCode(code);

            if (codeValid)
            {
                ViewBag.RegistrationCode = code;
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterStudent(RegisterViewModel registration)
        {
            if (ModelState.IsValid)
            {

                bool codeValid = await _studentManagmentService.ValidateRegistrationCode(registration.RegistrationCode);
                if (!codeValid)
                    return RedirectToAction("Index", "Home");

                RegisterUserDTO registerData = _mapper.Map<RegisterUserDTO>(registration);

                await _studentManagmentService.RegisterStudent(registerData);

                ResultMessage<StudentDTO> result = await _studentService.GetSingleOrDefault(s => s.Username == registration.Username);
                if (result.IsSuccess)
                {
                    ClaimsPrincipal claimsPrincipal = GetClaimsPrincipal(result.Result);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                    return RedirectToAction("Index", "StudyMaterial");
                }
                else
                    // MAybe eturn error page
                    return Content("Internal server error");
            }
            else
                return RedirectToAction("Index", "Home");
        }

        private ClaimsPrincipal GetClaimsPrincipal(StudentDTO student)
            => AuthenticationHelpers.GetClaimsPrincipal(student.StudentId, student.Privilege);

    }
}