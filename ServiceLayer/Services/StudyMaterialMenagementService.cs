using DatabaseLayer;
using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Shared;
using ServiceLayer.Students.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class StudyMaterialMenagementService : IStudyMaterialManagementService
    {
        private readonly ICourseService _courseService;
        private readonly IStudyMaterialService _studyMaterialService;
        private readonly IDocumentService _documentService;
        private readonly OrhedgeContext _context;

        public StudyMaterialMenagementService(OrhedgeContext context, IStudyMaterialService studyMaterialService, ICourseService courseService, IDocumentService documentService) : base()
        {
            _courseService = courseService;
            _context = context;
            _studyMaterialService = studyMaterialService;
            _documentService = documentService;
        }

        public async Task<HashSet<DetailedSemesterDTO>> GetSemestersWithAllInformation()
        {
            HashSet<DetailedSemesterDTO> semesters = new HashSet<DetailedSemesterDTO>();
            List<CourseDTO> courses = await _courseService.GetAll<NoSorting>();

            foreach (CourseDTO course in courses)
            {
                IEnumerable<CourseStudyProgram> programsWithThisCourse = _context.CourseStudyPrograms.Where(x => x.CourseId == course.CourseId);
                foreach (CourseStudyProgram csp in programsWithThisCourse)
                {
                    semesters.Add(csp.Semester);
                    semesters.First(x => x.Semester == csp.Semester).Courses[csp.StudyProgram].Add(course);
                }
            }
            return semesters;
        }

        public async Task<ResultMessage<bool>> SaveMaterial(StudyMaterialDTO data, BasicFileInfo fileInfo)
        {
            data.Uri = PathBuilder.BuildPathForStudyMaterial(data.CategoryId, data.CategoryId, fileInfo.FileName);
            ResultMessage<StudyMaterialDTO> studyMaterialResult = await _studyMaterialService.Add(data);
            if (!studyMaterialResult.IsSuccess)
                return new ResultMessage<bool>(false, studyMaterialResult.Status, studyMaterialResult.Message);
            return await _documentService.UploadDocumentToStorage(data.Uri, fileInfo.FileData);
        }
    }
}
