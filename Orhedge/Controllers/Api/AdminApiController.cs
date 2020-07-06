using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Orhedge.Enums;
using Orhedge.ViewModels;
using Orhedge.ViewModels.Admin;
using ServiceLayer.DTO;
using ServiceLayer.Services;
using System.Threading.Tasks;

namespace Orhedge.Controllers
{

    // TODO: Add authorization attributes
    [Route("api/AdminApi")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStudentManagmentService _studMngService;

        public AdminApiController(IStudentService studentService, IMapper mapper, IStudentManagmentService studMngService)
            => (_studentService, _mapper, _studMngService)
            = (studentService, mapper, studMngService);

        [HttpPut]
        public async Task<ActionResult> Edit([FromBody] EditStudentViewModel model)
        {
            await _studentService.Update(_mapper.Map<StudentDTO>(model));
            return RedirectToIndexController();
        }

        [HttpPut("delete")]
        public async Task<ActionResult> Delete([FromBody] DeleteStudentViewModel model)
        {
            await _studentService.Delete(model.StudentId);
            return RedirectToIndexController();
        }

        [HttpPost("send-confirmation-email")]
        public async Task<ActionResult> SendConfirmationEmail(RegisterFormViewModel registration)
        {
            bool isEmailRegistered = await _studMngService.IsStudentRegistered(registration.Email);
            bool isIndexRegistered = await _studMngService.IsStudentRegisteredIndex(registration.IndexNumber);

            if (isEmailRegistered)
                return BadRequest(new { error = nameof(SendConfirmEmailStatus.EmailAlreadyExists) });
            else if (isIndexRegistered)
                return BadRequest(new { error = nameof(SendConfirmEmailStatus.IndexAlreadyExists) });
            else
            {
                RegisterFormDTO registerFormDTO = _mapper.Map<RegisterFormDTO>(registration);
                await _studMngService.GenerateRegistrationEmail(registerFormDTO);
                return NoContent();
            }
        }

        private ActionResult RedirectToIndexController()
                 => Ok(JsonConvert.SerializeObject(Url.Link("Default", new
                 {
                     Controller = "Admin",
                     Action = "Index",
                 })));
    }
}