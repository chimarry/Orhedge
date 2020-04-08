using Microsoft.AspNetCore.Mvc;
using Orhedge.AutoMapper;
using Orhedge.ViewModels.StudyMaterial;
using ServiceLayer.DTO;
using ServiceLayer.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orhedge.Controllers
{
    public class StudyMaterialController : Controller
    {
        private readonly IStudyMaterialManagementService _studyMaterialManagementService;

        public StudyMaterialController(IStudyMaterialManagementService studyMaterialManagementService)
        {
            _studyMaterialManagementService = studyMaterialManagementService;
        }

        public async Task<IActionResult> Index()
        {
            var indexModel = new IndexViewModel();
            HashSet<DetailedSemesterDTO> detailedSemesterDTOs = await _studyMaterialManagementService.GetSemestersWithAllInformation();
            HashSet<SemesterViewModel> semesters = MappingConfiguration.CreateMapping().Map<HashSet<DetailedSemesterDTO>, HashSet<SemesterViewModel>>(detailedSemesterDTOs);
            indexModel.Semesters = semesters.ToList();
            indexModel.Semesters = indexModel.Semesters.OrderBy(x => x.Semester).ToList();
            return View(indexModel);
        }

    }
}