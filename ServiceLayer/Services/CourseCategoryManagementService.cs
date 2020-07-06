using DatabaseLayer;
using DatabaseLayer.Entity;
using DatabaseLayer.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ServiceLayer.AutoMapper;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class CourseCategoryManagementService : ICourseCategoryManagementService
    {
        private readonly ICategoryService _categoryService;
        private readonly ICourseService _courseService;
        private readonly IStudyMaterialService _studyMaterialService;
        private readonly OrhedgeContext _orhedgeContext;

        public CourseCategoryManagementService(ICategoryService categoryService, ICourseService courseService,
            IStudyMaterialService studyMaterialService, OrhedgeContext orhedgeContext)
        {
            _courseService = courseService;
            _categoryService = categoryService;
            _studyMaterialService = studyMaterialService;
            _orhedgeContext = orhedgeContext;
        }

        public async Task<ResultMessage<bool>> AddInStudyProgram(int courseId, StudyProgram studyProgram, Semester semester)
        {
            try
            {
                CourseStudyProgram courseStudyProgram = new CourseStudyProgram()
                {
                    CourseId = courseId,
                    Semester = semester,
                    StudyProgram = studyProgram,
                    StudyYear = semester.GetStudyYear()
                };
                await _orhedgeContext.AddAsync(courseStudyProgram);
                await _orhedgeContext.SaveChangesAsync();
                return new ResultMessage<bool>(true, OperationStatus.Success);
            }
            catch (DbUpdateException)
            {
                return new ResultMessage<bool>(false, OperationStatus.Exists);
            }
        }

        /// <summary>
        /// Returns total number of items that satisfy certain criteria.
        /// </summary>
        /// <param name="searchFor">Lookup word (optional)</param>
        /// <param name="studyPrograms">Unique identifiers of studyPrograms (optional)</param>
        /// <returns>Total number of items</returns>
        public async Task<int> Count(string searchFor = null, StudyProgram[] studyPrograms = null)
          => (await GetDetailedCourses(searchFor, studyPrograms)).Count();

        public async Task<ResultMessage<bool>> DeleteCourse(int courseId)
        {
            using (IDbContextTransaction transaction = await _orhedgeContext.Database.BeginTransactionAsync())
            {

                ResultMessage<bool> deletedCourse = await _courseService.Delete(courseId);

                if (!deletedCourse.IsSuccess)
                    return new ResultMessage<bool>(false, deletedCourse.Status, deletedCourse.Message);
                List<CategoryDTO> categories = await _categoryService.GetAll<NoSorting>(x => x.CourseId == courseId && !x.Deleted);
                foreach (CategoryDTO cat in categories)
                {
                    ResultMessage<bool> deleteCat = await _categoryService.DeleteWithoutTransaction(cat.CategoryId);
                    if (!deleteCat.IsSuccess)
                        return deleteCat;
                }
                List<(Semester semester, StudyProgram sp)> semestersAndStudyPrograms = await GetCourseUsage(courseId);
                foreach ((Semester, StudyProgram) semSp in semestersAndStudyPrograms)
                {
                    ResultMessage<bool> sp = await DeleteFromStudyProgram(courseId, semSp.Item2, semSp.Item1);
                    if (!sp.IsSuccess)
                        return sp;
                }
                transaction.Commit();
                return new ResultMessage<bool>(true, OperationStatus.Success);
            }
        }

        public async Task<ResultMessage<bool>> DeleteFromStudyProgram(int courseId, StudyProgram studyProgram, Semester semester)
        {
            try
            {
                CourseStudyProgram courseStudyProgram = await _orhedgeContext.CourseStudyPrograms
                                                                             .SingleOrDefaultAsync(x => x.CourseId == courseId
                                                                                                        && x.StudyProgram == studyProgram
                                                                                                        && x.Semester == semester);
                if (courseStudyProgram == null)
                    return new ResultMessage<bool>(false, OperationStatus.NotFound);
                _orhedgeContext.Remove(courseStudyProgram);
                await _orhedgeContext.SaveChangesAsync();
                return new ResultMessage<bool>(true, OperationStatus.Success);
            }
            catch (DbUpdateException)
            {
                return new ResultMessage<bool>(false, OperationStatus.Exists);
            }
        }

        public async Task<List<(Semester semester, StudyProgram studyProgram)>> GetCourseUsage(int courseId)
             => (await _orhedgeContext.CourseStudyPrograms
                                      .Where(x => x.CourseId == courseId)
                                      .Select(x => new { x.Semester, x.StudyProgram })
                                      .ToListAsync())
                                      .Select(x => (x.Semester, x.StudyProgram))
                                      .ToList();

        public async Task<List<DetailedCourseCategoryDTO>> GetDetailedCourses(int offset, int itemsCount, string searchFor = null, StudyProgram[] studyPrograms = null)
        {
            List<DetailedCourseCategoryDTO> detailedCourses = (await GetDetailedCourses(searchFor, studyPrograms))
                                    .Skip(offset)
                                    .Take(itemsCount)
                                    .ToList();

            foreach (DetailedCourseCategoryDTO dto in detailedCourses)
            {
                dto.Categories = await _categoryService.GetAll<NoSorting>(x => x.CourseId == dto.Course.CourseId && !x.Deleted);
                dto.StudyMaterialsCount = await _studyMaterialService.Count(x => !x.Deleted && dto.Categories.Select(z => z.CategoryId).Contains(x.CategoryId));
            }

            // Page
            return detailedCourses;
        }

        public async Task<string> GetName(int courseId)
        {
            CourseDTO course = await _courseService.GetSingleOrDefault(x => !x.Deleted && x.CourseId == courseId);
            return course.Name;
        }

        public async Task<ResultMessage<bool>> SaveCourse(string name, string[] categories, Semester semester, StudyProgram studyProgram)
        {
            using (IDbContextTransaction transaction = await _orhedgeContext.Database.BeginTransactionAsync())
            {
                ResultMessage<CourseDTO> addedCourse = await _courseService.Add(new CourseDTO()
                {
                    Name = name
                });
                if (!addedCourse.IsSuccess)
                    return new ResultMessage<bool>(false, addedCourse.Status, addedCourse.Message);
                foreach (string category in categories)
                {
                    ResultMessage<CategoryDTO> addedCategory = await _categoryService.Add(
                        new CategoryDTO()
                        {
                            CourseId = addedCourse.Result.CourseId,
                            Name = category
                        });
                    if (!addedCategory.IsSuccess)
                        return new ResultMessage<bool>(false, addedCategory.Status, addedCategory.Message);
                }
                ResultMessage<bool> sp = await AddInStudyProgram(addedCourse.Result.CourseId, studyProgram, semester);
                if (!sp.IsSuccess)
                    return sp;
                transaction.Commit();
                return new ResultMessage<bool>(true, OperationStatus.Success);
            }
        }

        private async Task<IQueryable<DetailedCourseCategoryDTO>> GetDetailedCourses(string searchFor = null, StudyProgram[] studyPrograms = null)
        {
            string trimmedSearchFor = searchFor == null ? string.Empty : searchFor.Trim();

            IQueryable<DetailedCourseCategoryDTO> detailedCourses =
                      _orhedgeContext.Courses
                      .Where(x => !x.Deleted && x.CourseStudyPrograms.Any(z => studyPrograms.Contains(z.StudyProgram)))
                     // Apply filter by the given lookup word
                     .Where(x => x.Name.Trim().Contains(trimmedSearchFor, StringComparison.CurrentCultureIgnoreCase))
                     .OrderBy(x => x.Name)
                     .Select(x => new DetailedCourseCategoryDTO(Mapping.Mapper.Map<CourseDTO>(x)));
            return detailedCourses;
        }
    }
}
