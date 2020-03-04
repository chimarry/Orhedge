using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orhedge.ViewModels;
using ServiceLayer.DTO.Registration;
using ServiceLayer.Models;
using ServiceLayer.Students.Interfaces;

namespace Orhedge.Controllers
{
    public class RegisterController : Controller
    {

        private readonly IStudentManagmentService _studentManagmentService;
        private readonly IMapper _mapper;

        public RegisterController(IStudentManagmentService studentManagmentService, IMapper mapper)
            => (_studentManagmentService, _mapper) = (studentManagmentService, mapper);



        [HttpPost]
        [ValidateAntiForgeryToken]
        // TODO: Uncomment this to allow only administrators to register new users
        //[Authorize(Roles = "0")]
        public async Task<IActionResult> SendRegistrationEmail([FromForm]RegisterFormViewModel registration)
        {
            if(ModelState.IsValid)
            {

                
                bool isRegistered 
                    = await _studentManagmentService.IsStudentRegistered(registration.Email);

                if (isRegistered)
                {
                    // TODO: Present error page
                    return Content("Already registered");
                }
                else
                {
                    RegisterFormDTO registerFormDTO = _mapper.Map<RegisterFormDTO>(registration);
                    await _studentManagmentService.GenerateRegistrationEmail(registerFormDTO);
                    // TODO: Display success page
                    return Content("Success");
                }
            }
            else
            {
                // TODO: Display view explaining which fields are invalid
                return Content("Display view here");
            }
        }

        public async Task<IActionResult> ShowRegisterForm([FromQuery] string code)
        {
            bool codeValid = await _studentManagmentService.ValidateRegistrationCode(code);

            if (codeValid)
            {
                // TODO: Display registration form, pass given code to form
                return Content("Display registration form here");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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

                // TODO: Display success view or redirect to home page
                return Content("Registration succesfull");
            }
            else
                // TODO: Display error page with invalid fields
                return Content("Display error page with fields");
        }

    }
}