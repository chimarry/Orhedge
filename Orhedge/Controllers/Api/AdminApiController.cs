using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orhedge.ViewModels.Admin;
using Newtonsoft.Json;
using ServiceLayer.Services;
using ServiceLayer.ErrorHandling;
using Orhedge.AutoMapper;
using ServiceLayer.DTO;
using AutoMapper;
using Orhedge.ViewModels;
using Orhedge.Enums;

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
        public async Task<ActionResult> Edit([FromBody]EditStudentViewModel model)
        {
            await _studentService.Update(_mapper.Map<StudentDTO>(model));
            string newUrl = Url.Link("Default", new
            {
                Controller = "Admin",
                Action = "Index"
            });
            return Redirect(newUrl);
        }

        [HttpPut("delete")]
        public async Task<ActionResult> Delete([FromBody]DeleteStudentViewModel model)
        {
            await _studentService.Delete(model.StudentId);
            string newUrl = Url.Link("Default", new
            {
                Controller = "Admin",
                Action = "Index"
            });
            return Redirect(newUrl);
        }

        [HttpPost("send-confirmation-email")]
        public async Task<ActionResult> SendConfirmationEmail(RegisterFormViewModel registration)
        {
            bool isRegistered = await _studMngService.IsStudentRegistered(registration.Email);

            if (isRegistered)
            {
                return BadRequest(new { error = nameof(SendConfirmEmailStatus.AlreadyExists) });
            }
            else
            {
                RegisterFormDTO registerFormDTO = _mapper.Map<RegisterFormDTO>(registration);
                await _studMngService.GenerateRegistrationEmail(registerFormDTO);
                return NoContent();
            }
        }
    }
}