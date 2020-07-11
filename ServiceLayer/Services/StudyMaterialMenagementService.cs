using DatabaseLayer;
using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ServiceLayer.AutoMapper;
using ServiceLayer.DTO;
using ServiceLayer.DTO.Materials;
using ServiceLayer.DTO.Student;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Shared;
using ServiceLayer.Students.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class StudyMaterialMenagementService : IStudyMaterialManagementService
    {
        private readonly ICourseService _courseService;
        private readonly IStudyMaterialService _studyMaterialService;
        private readonly IDocumentService _documentService;
        private readonly ICategoryService _categoryService;
        private readonly IStudentService _studentService;
        private readonly IErrorHandler _errorHandler;
        private readonly OrhedgeContext _context;

        public StudyMaterialMenagementService(IErrorHandler errorHandler, ICourseService courseService, IStudyMaterialService studyMaterialService, IDocumentService documentService,
                                              ICategoryService categoryService, IStudentService studentService, OrhedgeContext context)
        {
            _errorHandler = errorHandler;
            _courseService = courseService;
            _studyMaterialService = studyMaterialService;
            _documentService = documentService;
            _categoryService = categoryService;
            _studentService = studentService;
            _context = context;
        }

        /// <summary>
        /// Returns semesters with courses organized by study programs.
        /// </summary>
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

        /// <summary>
        /// Returns courses with categories, that belong to specific study year.
        /// </summary>
        /// <param name="year">Value from 0 to 3</param>
        public async Task<List<CourseCategoryDTO>> GetCoursesByYear(int year)
          => await _context.CourseStudyPrograms
                           .Where(csp => csp.StudyYear == year)
                           .Select(csp => new CourseCategoryDTO
                           {
                               Course = csp.Course.Deleted ? null : Mapping.Mapper.Map<CourseDTO>(csp.Course),
                               Categories = csp.Course
                                               .Categories
                                               .Where(x => !x.Deleted)
                                               .Select(cat => Mapping.Mapper.Map<CategoryDTO>(cat)).ToList()
                           })
                           .Distinct()
                           .ToListAsync();

        /// <summary>
        /// Saves list of study materials in specified category within specific course, relating those study materials with
        /// specific student.
        /// </summary>
        /// <param name="categoryId">Unique identifier for the category</param>
        /// <param name="studentId">Unique identifier for the student</param>
        /// <param name="fileInfos">List of file information</param>
        /// <returns>True if upload succeeds, false if not</returns>
        public async Task<ResultMessage<bool>> SaveStudyMaterials(int categoryId, int studentId, List<BasicFileInfo> fileInfos)
        {
            try
            {
                using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
                {
                    foreach (BasicFileInfo fileInfo in fileInfos)
                    {
                        ResultMessage<CategoryDTO> choosenCategory = await _categoryService
                                                                    .GetSingleOrDefault(x => x.CategoryId == categoryId);
                        if (!choosenCategory.IsSuccess)
                            return new ResultMessage<bool>(false, choosenCategory.Status, choosenCategory.Message);

                        StudyMaterialDTO data = new StudyMaterialDTO()
                        {
                            StudentId = studentId,
                            CategoryId = categoryId,
                            Name = fileInfo.FileName,
                            UploadDate = DateTime.Now,
                            Uri = PathBuilder.BuildPathForStudyMaterial(choosenCategory.Result.CourseId, categoryId, fileInfo.FileName)
                        };

                        ResultMessage<StudyMaterialDTO> studyMaterialResult = await _studyMaterialService.Add(data);
                        if (!studyMaterialResult.IsSuccess)
                            return new ResultMessage<bool>(false, studyMaterialResult.Status, studyMaterialResult.Message);

                        ResultMessage<bool> fsUploadResult = await _documentService.UploadDocumentToStorage(data.Uri, fileInfo.FileData);
                        if (!fsUploadResult.IsSuccess)
                            return new ResultMessage<bool>(false, fsUploadResult.Status, fsUploadResult.Message);
                    }
                    transaction.Commit();
                }
                return new ResultMessage<bool>(OperationStatus.Success);
            }
            catch (DbUpdateException ex)
            {
                return new ResultMessage<bool>(false, _errorHandler.Handle(ex));
            }
        }

        /// <summary>
        /// Foreach element of provided list of study materials, this method returns rating that specified student gave for that study material.
        /// </summary>
        /// <param name="studentId">Unique identifier of a student</param>
        /// <param name="studyMaterials">List of study materials</param>
        public async Task<List<DetailedStudyMaterialDTO>> AppendRating(int studentId, List<DetailedStudyMaterialDTO> studyMaterials)
        {
            foreach (DetailedStudyMaterialDTO detailedStudyMaterialDTO in studyMaterials)
                detailedStudyMaterialDTO.GivenRating = (int?)(await _context.StudyMaterialRatings.
                                                                      FirstOrDefaultAsync(x => x.StudentId == studentId
                                                                      && x.StudyMaterialId == detailedStudyMaterialDTO.StudyMaterialId))?.Rating;
            return studyMaterials;
        }

        /// <summary>
        /// Returns list of detailed study materials, sorted and filtered.
        /// </summary>
        /// <typeparam name="TKey">Type of the element based on which sorting is applied</typeparam>
        /// <param name="searchFor">Lookup word</param>
        /// <param name="categories">List of categories that need to be included in lookup</param>
        /// <param name="sortKeySelector">Function that says how to get element based on which sorting is applied</param>
        /// <param name="asc">Direction of order</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>List of detailed study materials or empty list</returns>
        public async Task<List<DetailedStudyMaterialDTO>> GetDetailedStudyMaterials<TKey>(int courseId, int offset, int itemsCount, string searchFor = null, int[] categories = null, Func<DetailedStudyMaterialDTO, TKey> sortKeySelector = null, bool asc = true)
        {
            IQueryable<DetailedStudyMaterialDTO> detailedStudyMaterialDTOs = await GetDetailedStudyMaterialDTOs<TKey>(courseId, searchFor, categories);

            // Sort
            if (asc && sortKeySelector != null)
                detailedStudyMaterialDTOs = detailedStudyMaterialDTOs.OrderBy(x => sortKeySelector(x));
            else if (sortKeySelector != null)
                detailedStudyMaterialDTOs = detailedStudyMaterialDTOs.OrderByDescending(x => sortKeySelector(x));
            else
                detailedStudyMaterialDTOs = detailedStudyMaterialDTOs.OrderByDescending(x => x.UploadDate);

            // Page
            return detailedStudyMaterialDTOs.Skip(offset).Take(itemsCount).ToList();
        }

        /// <summary>
        /// Returns total number of items that satisfy certain criteria.
        /// </summary>
        /// <param name="courseId">Unique identifier of course where materials belong</param>
        /// <param name="searchFor">Lookup word (optional)</param>
        /// <param name="categories">Unique identifiers of categories (optional)</param>
        /// <returns>Total number of items</returns>
        public async Task<int> Count(int courseId, string searchFor = null, int[] categories = null)
          => (await GetDetailedStudyMaterialDTOs<NoSorting>(courseId, searchFor, categories)).Count();


        /// <exception cref="ArgumentNullException"></exception>
        private async Task<IQueryable<DetailedStudyMaterialDTO>> GetDetailedStudyMaterialDTOs<TKey>(int courseId, string searchFor = null, int[] categories = null)
        {
            if (categories == null || categories.Count() == 0)
                categories = (await _categoryService.GetAll<NoSorting>(x => x.CourseId == courseId && !x.Deleted)).Select(x => x.CategoryId).ToArray();

            string trimmedSearchFor = searchFor == null ? string.Empty : searchFor.Trim();

            IQueryable<DetailedStudyMaterialDTO> detailedStudyMaterialDTOs =
                      _context.StudyMaterials
                      .Where(x => !x.Deleted && categories.Contains(x.CategoryId))
                      // Get the name of an author
                      .Join(_context.Students,
                             studyMaterial => studyMaterial.StudentId,
                             student => student.StudentId,
                             (studyMaterial, student) => new { StudyMaterial = studyMaterial, AuthorFullName = string.Format("{0} {1}", student.Name, student.LastName), student.Deleted })
                      // Get the name of a category
                      .Join(_context.Categories,
                             studyMaterialWithAuthor => studyMaterialWithAuthor.StudyMaterial.CategoryId,
                             category => category.CategoryId,
                             (studyMaterialWithAuthor, category) => new DetailedStudyMaterialDTO(Mapping.Mapper.Map<StudyMaterialDTO>(studyMaterialWithAuthor.StudyMaterial))
                             {
                                 AuthorFullName = studyMaterialWithAuthor.Deleted ? DeletedStudentDTO.FullName : studyMaterialWithAuthor.AuthorFullName,
                                 CategoryName = category.Name
                             })
                     // Apply filter by the given lookup word
                     .Where(x => x.AuthorFullName.Trim().Contains(trimmedSearchFor, StringComparison.CurrentCultureIgnoreCase)
                                                               || x.Name.Trim().Contains(trimmedSearchFor, StringComparison.CurrentCultureIgnoreCase)
                                                               || x.TotalRating.ToString().Contains(trimmedSearchFor, StringComparison.CurrentCultureIgnoreCase)
                                                               || x.UploadDate.Date.ToString().Contains(trimmedSearchFor, StringComparison.CurrentCultureIgnoreCase));
            return detailedStudyMaterialDTOs;
        }

        /// <summary>
        /// Downloads specified study material.
        /// </summary>
        /// <param name="studyMaterialId">Unique identifier used for specifying study material</param>
        /// <returns>Basic information about file wrapped with result</returns>
        public async Task<ResultMessage<BasicFileInfo>> DownloadStudyMaterial(int studyMaterialId)
        {
            ResultMessage<StudyMaterialDTO> studyMaterialDTO = await _studyMaterialService.GetSingleOrDefault
                                                      (x => x.StudyMaterialId == studyMaterialId && !x.Deleted);
            if (!studyMaterialDTO.IsSuccess)
                return new ResultMessage<BasicFileInfo>(studyMaterialDTO.Status, studyMaterialDTO.Message);
            ResultMessage<BasicFileInfo> result = await _documentService.DownloadFromStorage(studyMaterialDTO.Result.Uri);
            if(result.IsSuccess)
                result.Result.FileName = studyMaterialDTO.Result.Name;

            return result;
        }

        /// <summary>
        /// With this method, specific student has option to rate specific study material.
        /// This method uses transactions.
        /// </summary>
        /// <param name="studyMaterialId">Unique identifier for the study material</param>
        /// <param name="authorId">Unique identifier for the author</param>
        /// <param name="studentId">Unique identifier for the student that rates material</param>
        /// <param name="rating">Given rating</param>
        /// <returns></returns>
        public async Task<ResultMessage<bool>> Rate(int studyMaterialId, int studentId, int authorId, int rating)
        {
            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                StudyMaterialRating smr = await _context.StudyMaterialRatings.FirstOrDefaultAsync(x => x.StudyMaterialId == studyMaterialId && x.StudentId == studentId);
                if (smr == null)
                    await _context.StudyMaterialRatings.AddAsync(smr = new StudyMaterialRating()
                    {
                        StudentId = studentId,
                        StudyMaterialId = studyMaterialId,
                        Rating = rating
                    });
                else
                    smr.Rating = rating;
                try
                {
                    await _context.SaveChangesAsync();
                    double totalRatingForMaterial = await _context.StudyMaterialRatings
                                                       .Where(x => x.StudyMaterialId == studyMaterialId)
                                                       .AverageAsync(x => x.Rating);
                    ResultMessage<bool> updatedMaterial = await _studyMaterialService.UpdateRating(studyMaterialId, totalRatingForMaterial);
                    if (!updatedMaterial.IsSuccess)
                        return new ResultMessage<bool>(updatedMaterial.Status, updatedMaterial.Message);
                    double totalRatingForAuthor = await _context.StudyMaterialRatings
                                                                  .Where(x => x.StudyMaterial.StudentId == authorId)
                                                                  .AverageAsync(x => x.Rating);
                    ResultMessage<bool> updatedStudent = await _studentService.UpdateRating(authorId, totalRatingForAuthor);
                    if (!updatedStudent.IsSuccess)
                        return new ResultMessage<bool>(updatedStudent.Status, updatedStudent.Message);
                    transaction.Commit();
                    return new ResultMessage<bool>(true, OperationStatus.Success);
                }
                catch (DbUpdateException ex)
                {
                    transaction.Rollback();
                    return new ResultMessage<bool>(false, _errorHandler.Handle(ex));
                }
            }
        }

        public async Task<ResultMessage<bool>> Move(int studyMaterialId, int categoryId)
        {
            try
            {
                StudyMaterial studyMaterial = await _context.StudyMaterials.SingleOrDefaultAsync(sm => sm.StudyMaterialId == studyMaterialId);

                if (studyMaterial == null)
                    return new ResultMessage<bool>(false, OperationStatus.NotFound);
                studyMaterial.CategoryId = categoryId;
                await _context.SaveChangesAsync();
                return new ResultMessage<bool>(true, OperationStatus.Success);
            }
            catch (DbUpdateException ex)
            {
                OperationStatus status = _errorHandler.Handle(ex);
                return new ResultMessage<bool>(false, status);
            }
        }
    }
}
