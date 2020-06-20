using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Orhedge.Helpers;
using Orhedge.ViewModels.Admin;
using Orhedge.ViewModels.Student;
using ServiceLayer.DTO;
using ServiceLayer.DTO.Student;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Services;
using System.Threading.Tasks;

namespace Orhedge.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentManagmentService _studMngService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IStudentService _studService;

        public StudentController(
            IStudentManagmentService studMngService,
            IStudentService studService,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
            => (_studMngService, _mapper, _localizer, _studService)
            = (studMngService, mapper, localizer, studService);

        public async Task<IActionResult> Index([FromQuery] int id)
        {
            ResultMessage<StudentDTO> result = await _studService.GetSingleOrDefault(stud => stud.StudentId == id);
            if (result.IsSuccess)
                return View(_mapper.Map<StudentViewModel>(result.Result));

            // TODO: Consider better place for redirection
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Edit()
        {
            int studId = this.GetUserId();
            StudentDTO profile = await _studService.GetSingleOrDefault(s => s.StudentId == studId);
            EditProfileViewModel vm = _mapper.Map<EditProfileViewModel>(profile);

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProfileViewModel profile)
        {
            if (ModelState.IsValid)
            {
                ProfileUpdateDTO update = _mapper.Map<ProfileUpdateDTO>(profile);
                await _studMngService.EditStudentProfile(this.GetUserId(), update);
                return RedirectToAction("Edit");
            }
            else
                return View(profile);
        }

    }
}