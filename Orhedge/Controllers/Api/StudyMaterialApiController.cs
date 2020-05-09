using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Orhedge.ViewModels.StudyMaterial;
using ServiceLayer.DTO.Materials;
using ServiceLayer.Services;

namespace Orhedge.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyMaterialApiController : ControllerBase
    {
        private readonly IStudyMaterialManagementService _studMatMng;
        private readonly IMapper _mapper;

        public StudyMaterialApiController(IStudyMaterialManagementService studMatMng, IMapper mapper)
        {
            _studMatMng = studMatMng;
            _mapper = mapper;
        }

        [HttpGet("courses/{year}")]
        public async Task<IActionResult> GetCourses(int year)
        {
            List<CourseCategoryDTO> courses = await _studMatMng.GetCoursesByYear(year);
            List<CourseCategoryViewModel> coursesVm = _mapper.Map<List<CourseCategoryViewModel>>(courses);

            return Ok(JsonConvert.SerializeObject(coursesVm, 
                new JsonSerializerSettings
                { 
                    ContractResolver = new CamelCasePropertyNamesContractResolver() 
                }));
        }
    }
}