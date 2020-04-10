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

namespace Orhedge.Controllers
{
    [Route("api/AdminApi")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public AdminApiController(IStudentService studentService, IMapper mapper) => (_studentService, _mapper) = (studentService, mapper);

        [HttpPut]
        public async Task<ActionResult> Edit([FromBody]EditStudentViewModel model)
        {
            ResultMessage<StudentDTO> operationResult = await _studentService.Update(_mapper.Map<StudentDTO>(model));
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
            ResultMessage<bool> operationResult = await _studentService.Delete(model.StudentId);
            string newUrl = Url.Link("Default", new
            {
                Controller = "Admin",
                Action = "Index"
            });
            return Redirect(newUrl);
        }
    }
}