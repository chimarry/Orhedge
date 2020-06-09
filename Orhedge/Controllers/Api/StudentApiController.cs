using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Orhedge.Helpers;
using Orhedge.ViewModels.Student;
using ServiceLayer.DTO.Student;
using ServiceLayer.Services;
using System.Threading.Tasks;

namespace Orhedge.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStudentManagmentService _studMngService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public StudentApiController(
            IStudentManagmentService studMngService,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
            => (_studMngService, _mapper, _localizer) = (studMngService, mapper, localizer);

        [HttpPut("update-password")]
        public async Task<ActionResult> EditStudentPassword(ChangePasswordViewModel passVm)
        {
            UpdatePasswordDTO passDTO = _mapper.Map<UpdatePasswordDTO>(passVm);

            PassChangeStatus status = await _studMngService.UpdateStudentPassword(this.GetUserId(), passDTO);

            // PassChangeStatus.PassNoMatch already 
            // handled by model validation due to Compare attribute in ChangePasswordViewModel
            if (status != PassChangeStatus.Success)
                return BadRequest(new { error = nameof(PassChangeStatus.InvalidOldPass) });
            else
                return Ok();
        }

        [HttpGet("validate-pass/{password}")]
        public async Task<ActionResult> ValidateOldPass(string password)
        {
            bool valid = await _studMngService.ValidatePassword(this.GetUserId(), password);
            return Ok(JsonConvert.SerializeObject(valid));
        }
    }
}