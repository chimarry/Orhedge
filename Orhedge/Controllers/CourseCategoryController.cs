using AutoMapper;
using DatabaseLayer.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Orhedge.Attributes;
using Orhedge.Enums;
using Orhedge.Helpers;
using Orhedge.ViewModels.CourseCategory;
using Orhedge.ViewModels.StudyMaterial;
using ServiceLayer.DTO;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orhedge.Controllers
{
    [AuthorizePrivilege(StudentPrivilege.SeniorAdmin, StudentPrivilege.JuniorAdmin)]
    public class CourseCategoryController : Controller
    {
        private readonly ICourseCategoryManagementService _courseCategoryManagementService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public CourseCategoryController(ICourseCategoryManagementService courseCategoryManagementService, IStringLocalizer<SharedResource> stringLocalizer,
                                        ICategoryService categoryService, IMapper mapper)
        {
            _stringLocalizer = stringLocalizer;
            _courseCategoryManagementService = courseCategoryManagementService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(HttpReponseStatusCode statusCode = HttpReponseStatusCode.NoStatus)
        {
            CourseCategoryIndexViewModel mainModel = await GetCourses();
            ViewBag.InfoMessage = new InfoMessage(_stringLocalizer, statusCode);
            return View(mainModel);
        }

        [HttpGet]
        public async Task<IActionResult> SearchFilter(int pageNumber, string searchFor, StudyProgram[] studyPrograms)
        {
            CourseCategoryIndexViewModel mainModel = await GetCourses(pageNumber, studyPrograms, searchFor);
            return View("Index", mainModel);
        }

        private async Task<CourseCategoryIndexViewModel> GetCourses(int pageNumber = 0, StudyProgram[] studyPrograms = null, string searchFor = null)
        {
            int offset = pageNumber * WebConstants.MAX_NUMBER_OF_COURSES_PER_PAGE;
            if (studyPrograms == null || studyPrograms.Count() == 0)
                studyPrograms = Enum.GetValues(typeof(StudyProgram)).Cast<StudyProgram>().ToArray();
            List<DetailedCourseCategoryDTO> detailedCourses = await _courseCategoryManagementService.GetDetailedCourses(offset, WebConstants.MAX_NUMBER_OF_COURSES_PER_PAGE, studyPrograms, searchFor);
            PageInformation pageInformation = new PageInformation(pageNumber, _courseCategoryManagementService.Count(studyPrograms, searchFor), WebConstants.MAX_NUMBER_OF_COURSES_PER_PAGE);
            CourseCategoryIndexViewModel mainModel = new CourseCategoryIndexViewModel(_mapper.Map<List<DetailedCourseCategoryDTO>, List<DetailedCourseViewModel>>(detailedCourses), pageInformation);
            ViewBag.SearchFor = searchFor;
            ViewBag.StudyPrograms = studyPrograms;
            return mainModel;
        }

        public async Task<IActionResult> Details(int courseId, HttpReponseStatusCode statusCode = HttpReponseStatusCode.NoStatus)
        {
            List<(Semester, StudyProgram)> semestersAndStudyPrograms = await _courseCategoryManagementService.GetCourseUsage(courseId);
            DetailsViewModel detailsViewModel = new DetailsViewModel()
            {
                DetailedCourseViewModel = new DetailedCourseViewModel()
                {
                    CourseId = courseId,
                    Name = await _courseCategoryManagementService.GetName(courseId)
                },
                SemesterAndStudyPrograms = semestersAndStudyPrograms,
            };
            detailsViewModel.DetailedCourseViewModel.Categories = _mapper.Map<List<CategoryViewModel>>(await _categoryService.GetAll<NoSorting>(x => x.CourseId == courseId && !x.Deleted));
            ViewBag.InfoMessage = new InfoMessage(_stringLocalizer, statusCode);
            return View(detailsViewModel);
        }
    }
}
