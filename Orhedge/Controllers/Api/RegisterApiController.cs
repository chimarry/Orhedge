using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Services;

namespace Orhedge.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterApiController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public RegisterApiController(IStudentService studentService)
            => _studentService = studentService;

        [HttpGet("username-exists")]
        public async Task<ActionResult> UsernameExists(string username)
        {
            ResultMessage<StudentDTO> result = await _studentService.GetSingleOrDefault(s => s.Username == username && !s.Deleted);

            return Ok(!result.IsSuccess);
        }
    }
}
