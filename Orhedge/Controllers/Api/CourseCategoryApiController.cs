﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Orhedge.ViewModels.CourseCategory;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Services;
using System.Threading.Tasks;

namespace Orhedge.Controllers
{
    [Route("api/CourseCategoryApi")]
    [ApiController]
    public class CourseCategoryApiController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ICourseCategoryManagementService _courseCategoryManagementService;

        public CourseCategoryApiController(ICategoryService categoryService, ICourseCategoryManagementService courseCategoryManagementService)
        {
            _categoryService = categoryService;
            _courseCategoryManagementService = courseCategoryManagementService;
        }

        [HttpPut("category")]
        public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryViewModel model)
        {
            ResultMessage<bool> deleted = await _categoryService.Delete(model.CategoryId);
            return RedirectToDetailsController(model.CourseId);
        }

        [HttpPut("studyProgram")]
        public async Task<IActionResult> DeleteFromStudyProgram([FromBody] DeleteFromStudyProgramViewModel model)
        {
            ResultMessage<bool> deleted = await _courseCategoryManagementService.DeleteFromStudyProgram(model.CourseId, model.StudyProgram, model.Semester);
            return RedirectToDetailsController(model.CourseId);
        }

        [HttpPost("studyProgram")]
        public async Task<IActionResult> AddInStudyProgram([FromBody] AddInStudyProgramViewModel model)
        {
            ResultMessage<bool> added = await _courseCategoryManagementService.AddInStudyProgram(model.CourseId, model.StudyProgram, model.Semester);
            return RedirectToDetailsController(model.CourseId);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryViewModel model)
        {
            CategoryDTO categoryDTO = new CategoryDTO()
            {
                CourseId = model.CourseId,
                Name = model.Category
            };
            ResultMessage<CategoryDTO> addedCategory = await _categoryService.Add(categoryDTO);
            return RedirectToDetailsController(model.CourseId);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] SaveCourseViewModel model)
        {
            ResultMessage<bool> addResult = await _courseCategoryManagementService.SaveCourse(model.Name, model.GetCategories(), model.Semester, model.StudyProgram);
            return Ok(JsonConvert.SerializeObject(Url.Action("Index", "CourseCategory")));
        }


        [HttpPut("delete/{courseId}")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            ResultMessage<bool> deleteCourseResult = await _courseCategoryManagementService.DeleteCourse(courseId);
            return Ok(JsonConvert.SerializeObject(Url.Action("Index", "CourseCategory")));
        }

        private ActionResult RedirectToDetailsController(int courseId)
               => Ok(JsonConvert.SerializeObject(Url.Action("Details", "CourseCategory", new
               {
                   courseId = courseId
               })));
    }
}
