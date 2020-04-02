using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orhedge.ViewModels.Student;
using ServiceLayer.Services;
using Orhedge.Helpers;
using ServiceLayer.DTO.Student;
using AutoMapper;
using Microsoft.Extensions.Localization;
using ServiceLayer.DTO;

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

        public IActionResult Index()
        {
            return View();
        }

        
        public async Task<IActionResult> EditProfile()
        {
            int studId = this.GetUserId();
            StudentDTO profile = await _studService.GetSingleOrDefault(s => s.StudentId == studId);
            EditProfileViewModel vm = _mapper.Map<EditProfileViewModel>(profile);
            
            return View(vm);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditProfileViewModel profile)
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

        public IActionResult EditPassword()
            => View();

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPassword(ChangePasswordViewModel passVm)
        {
            if (ModelState.IsValid)
            {
                UpdatePasswordDTO passwordDTO = _mapper.Map<UpdatePasswordDTO>(passVm);
                PassChangeStatus status = await _studMngService.
                    UpdateStudentPassword(this.GetUserId(), passwordDTO);

                if (status == PassChangeStatus.InvalidOldPass)
                {
                    ModelState.AddModelError(nameof(ChangePasswordViewModel.OldPassword), _localizer["PassNoMatch"]);
                    return View();
                }
                else if (status == PassChangeStatus.PassNoMatch)
                {
                    ModelState.AddModelError(nameof(ChangePasswordViewModel.NewPassword), _localizer["PassNoMatch"]);
                    return View();
                }


                return RedirectToAction("Index");

            }
            else
                return View();
        }
    }
}