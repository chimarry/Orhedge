using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Orhedge.ViewModels;
using ServiceLayer.DTO;
using ServiceLayer.Services;
using System.Threading.Tasks;

namespace Orhedge.Controllers
{
    public class RegisterController : Controller
    {

        private readonly IStudentManagmentService _studentManagmentService;
        private readonly IMapper _mapper;

        public RegisterController(IStudentManagmentService studentManagmentService, IMapper mapper)
            => (_studentManagmentService, _mapper) = (studentManagmentService, mapper);

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

                // TODO: Display success view or redirect to home page
                return Content("Registration succesfull");
            }
            else
                return RedirectToAction("Index", "Home");
        }

    }
}