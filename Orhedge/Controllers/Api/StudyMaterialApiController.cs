using AutoMapper;
using DatabaseLayer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Orhedge.Attributes;
using Orhedge.Enums;
using Orhedge.Helpers;
using Orhedge.ViewModels.StudyMaterial;
using ServiceLayer.DTO;
using ServiceLayer.DTO.Materials;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orhedge.Controllers
{

    [Route("api/StudyMaterialApi")]
    [ApiController]
    [Authorize]
    public class StudyMaterialApiController : ControllerBase
    {
        private readonly IStudyMaterialManagementService _studyMaterialManagementService;
        private readonly IStudyMaterialService _studyMaterialService;
        private readonly IMapper _mapper;

        public StudyMaterialApiController(IStudyMaterialManagementService studyMaterialManagementService,
                                          IStudyMaterialService studyMaterialService, IMapper mapper)
        {
            _studyMaterialManagementService = studyMaterialManagementService;
            _studyMaterialService = studyMaterialService;
            _mapper = mapper;
        }

        [HttpGet("courses/{year}")]
        public async Task<IActionResult> GetCourses(int year)
        {
            List<CourseCategoryDTO> courses = await _studyMaterialManagementService.GetCoursesByYear(year);
            List<CourseCategoryViewModel> coursesVm = _mapper.Map<List<CourseCategoryViewModel>>(courses);

            return Ok(JsonConvert.SerializeObject(coursesVm,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
        }

        /// <summary>
        /// Changes information about specific study material, and redirects back to main controller.
        /// </summary>
        /// <param name="model">Information about study material</param>
        /// <returns></returns>
        [HttpPut]
        [AuthorizePrivilege(StudentPrivilege.JuniorAdmin, StudentPrivilege.SeniorAdmin)]
        public async Task<ActionResult> Edit([FromBody] EditStudyMaterialViewModel model)
        {
            ResultMessage<StudyMaterialDTO> updated = await _studyMaterialService.Update(_mapper.Map<StudyMaterialDTO>(model));
            return RedirectToMainController(model.CourseId, updated.Status);
        }

        /// <summary>
        /// Deletes selected study material and returns control to main controller.
        /// </summary>
        /// <param name="model">Information about study material</param>
        /// <returns></returns>
        [HttpPut("delete")]
        [AuthorizePrivilege(StudentPrivilege.JuniorAdmin, StudentPrivilege.SeniorAdmin)]
        public async Task<ActionResult> Delete([FromBody] DeleteStudyMaterialViewModel model)
        {
            ResultMessage<bool> isDeleted = await _studyMaterialService.Delete(model.StudyMaterialId);
            return RedirectToMainController(model.CourseId, isDeleted.Status);
        }

        [HttpPut("rate")]
        [AuthorizePrivilege(StudentPrivilege.JuniorAdmin, StudentPrivilege.SeniorAdmin, StudentPrivilege.Normal)]
        public async Task<IActionResult> Rate([FromBody] RateStudyMaterialViewModel rateStudyMaterial)
        {
            int logginStudentId = this.GetUserId();
            ResultMessage<bool> ratingResult = await _studyMaterialManagementService.Rate(rateStudyMaterial.StudyMaterialId, logginStudentId, rateStudyMaterial.AuthorId, rateStudyMaterial.Rating);
            return Ok(JsonConvert.SerializeObject(ratingResult));
        }

        [HttpPut("move")]
        [AuthorizePrivilege(StudentPrivilege.JuniorAdmin, StudentPrivilege.SeniorAdmin)]
        public async Task<IActionResult> Move([FromBody] MoveStudyMaterialViewModel moveVm)
        {
            ResultMessage<bool> result = await _studyMaterialManagementService.Move(moveVm.StudyMaterialId, moveVm.CategoryId);
            return RedirectToMainController(moveVm.CourseId, result.Status);
        }

        private ActionResult RedirectToMainController(int courseId, OperationStatus operationStatus)
            => Ok(JsonConvert.SerializeObject(Url.Link("Default", new
            {
                Controller = "StudyMaterial",
                Action = "Course",
                courseId,
                statusCode = operationStatus.Map()
            })));
    }
}