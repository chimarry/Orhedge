using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orhedge.AutoMapper;
using Orhedge.Enums;
using Orhedge.Helpers;
using Orhedge.ViewModels;
using Orhedge.ViewModels.StudyMaterial;
using ServiceLayer.DTO;
using ServiceLayer.DTO.Materials;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using ServiceLayer.Students.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Orhedge.Controllers
{
    public class StudyMaterialController : Controller
    {
        private readonly IStudyMaterialManagementService _studyMaterialManagementService;
        private readonly IStudyMaterialService _studyMaterialService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public const int MaxNumberOfItemsPerPage = 2;

        public StudyMaterialController(IStudyMaterialManagementService studyMaterialManagementService,
                                       IStudyMaterialService studyMaterialService, ICategoryService categoryService, IMapper mapper)
        {
            _studyMaterialManagementService = studyMaterialManagementService;
            _studyMaterialService = studyMaterialService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Renders index page with courses grouped by study program and semester. 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            IndexViewModel indexModel = new IndexViewModel();
            HashSet<DetailedSemesterDTO> detailedSemesterDTOs = await _studyMaterialManagementService.GetSemestersWithAllInformation();
            HashSet<SemesterViewModel> semesters = _mapper.Map<HashSet<DetailedSemesterDTO>, HashSet<SemesterViewModel>>(detailedSemesterDTOs);
            indexModel.Semesters = semesters.ToList();
            indexModel.Semesters = indexModel.Semesters.OrderBy(x => x.Semester).ToList();
            return View(indexModel);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(List<IFormFile> files, int category)
        {
            List<BasicFileInfo> basicFileInfos = new List<BasicFileInfo>();
            foreach (IFormFile file in files)
                basicFileInfos.Add(_mapper.Map<BasicFileInfo>(file));
            ResultMessage<bool> isSavedResult = await _studyMaterialManagementService.SaveStudyMaterials(category, 1, basicFileInfos);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Returns index page of specific course, with its study materials. By default, no sorting or filtering
        /// is included.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Course(int courseId)
        {
            int totalNumberOfItems = await _studyMaterialManagementService.Count(courseId);
            PageInformation pageInformation = new PageInformation(0, totalNumberOfItems, MaxNumberOfItemsPerPage);
            List<StudyMaterialViewModel> detailedStudyMaterialViewModels = await GetDetailedStudyMaterials(courseId);
            CourseStudyMaterialsViewModel mainModel = new CourseStudyMaterialsViewModel(courseId, detailedStudyMaterialViewModels, pageInformation);
            int[] selectedCategories = (await _categoryService.GetAll<NoSorting>(x => x.CourseId == courseId && !x.Deleted))
                                                              .Select(x => x.CategoryId)
                                                              .ToArray();
            await SetViewInformation(courseId, categories: selectedCategories);
            return View(mainModel);
        }

        /// <summary>
        /// Sorts, filters or limits displayed study materials in specified course. It also allows word lookup.
        /// </summary>
        /// <param name="courseId">Unique identifier of the course</param>
        /// <param name="pageNumber">Next page number</param>
        /// <param name="sortCriteria">Sort criteria (optional)</param>
        /// <param name="searchFor">Lookup word (optional)</param>
        /// <param name="categories">List of filters (categories) (optional)</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> SearchSortFilter(int courseId, int pageNumber, StudyMaterialSortingCriteria sortCriteria, string searchFor, int[] categories)
        {
            int totalNumberOfItems = await _studyMaterialManagementService.Count(courseId, searchFor, categories);
            PageInformation pageinformation = new PageInformation(pageNumber, totalNumberOfItems, MaxNumberOfItemsPerPage);
            List<StudyMaterialViewModel> detailedStudyMaterialViewModels = await GetDetailedStudyMaterials(courseId, pageinformation.PageNumber, sortCriteria, searchFor, categories);
            CourseStudyMaterialsViewModel mainModel = new CourseStudyMaterialsViewModel(courseId, detailedStudyMaterialViewModels, pageinformation);
            await SetViewInformation(courseId, searchFor, sortCriteria, categories);
            return View("Course", mainModel);
        }

        /// <summary>
        /// Downloads specified study material.
        /// </summary>
        /// <param name="studyMaterialId">Unique identifier for the study material</param>
        /// <returns></returns>
        public async Task<IActionResult> DownloadStudyMaterial(int studyMaterialId)
        {
            ResultMessage<BasicFileInfo> basicFileInformation = await _studyMaterialManagementService.DownloadStudyMaterial(studyMaterialId);
            HttpContext.Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{basicFileInformation.Result.FileName}\"");
            return new FileContentResult(basicFileInformation.Result.FileData, System.Net.Mime.MediaTypeNames.Application.Octet)
            {
                FileDownloadName = basicFileInformation.Result.FileName
            };
        }

        private async Task<List<StudyMaterialViewModel>> GetDetailedStudyMaterials(int courseId, int pageNumber
            = 0, StudyMaterialSortingCriteria sortCriteria = StudyMaterialSortingCriteria.NoSorting, string searchFor = null, int[] categories = null)
        {
            int itemsCount = MaxNumberOfItemsPerPage;
            int offset = pageNumber * MaxNumberOfItemsPerPage;
            List<DetailedStudyMaterialDTO> studyMaterials = new List<DetailedStudyMaterialDTO>();
            switch (sortCriteria)
            {
                case StudyMaterialSortingCriteria.NoSorting: studyMaterials = await _studyMaterialManagementService.GetDetailedStudyMaterials<NoSorting>(courseId, offset, itemsCount, searchFor, categories); break;
                case StudyMaterialSortingCriteria.RatingAsc: studyMaterials = await _studyMaterialManagementService.GetDetailedStudyMaterials(courseId, offset, itemsCount, searchFor, categories, x => x.TotalRating); break;
                case StudyMaterialSortingCriteria.RatingDesc: studyMaterials = await _studyMaterialManagementService.GetDetailedStudyMaterials(courseId, offset, itemsCount, searchFor, categories, x => x.TotalRating, false); break;
                case StudyMaterialSortingCriteria.UploadDateAsc: studyMaterials = await _studyMaterialManagementService.GetDetailedStudyMaterials(courseId, offset, itemsCount, searchFor, categories, x => x.UploadDate); break;
                case StudyMaterialSortingCriteria.UploadDateDesc: studyMaterials = await _studyMaterialManagementService.GetDetailedStudyMaterials(courseId, offset, itemsCount, searchFor, categories, x => x.UploadDate, false); break;
            }
            studyMaterials = await _studyMaterialManagementService.AppendRating(1, studyMaterials);
            return _mapper.Map<List<DetailedStudyMaterialDTO>, List<StudyMaterialViewModel>>(studyMaterials);
        }

        /// <summary>
        /// Sets predefined values for a page.
        /// </summary>
        /// <param name="courseId">Unique identifier of a course</param>
        /// <param name="sortCriteria">Choosen sort criteria</param>
        /// <param name="categories">List of selected categories</param>
        /// <returns></returns>
        private async Task SetViewInformation(int courseId, string searchFor = null, StudyMaterialSortingCriteria sortCriteria = StudyMaterialSortingCriteria.NoSorting, int[] categories = null)
        {
            ViewBag.SortingCriteria = sortCriteria;
            ViewBag.SelectedCategories = categories;
            ViewBag.SearchFor = searchFor;
            ViewBag.AllCategories = _mapper.Map<List<CategoryDTO>, List<CategoryViewModel>>(await _categoryService.GetAll(x => x.CourseId == courseId && !x.Deleted, x => x.Name));
        }
    }
}