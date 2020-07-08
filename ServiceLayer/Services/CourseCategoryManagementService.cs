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
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class CourseCategoryManagementService : ICourseCategoryManagementService
    {
        private readonly ICategoryService _categoryService;
        private readonly ICourseService _courseService;
        private readonly IStudyMaterialService _studyMaterialService;
        private readonly IErrorHandler _errorHandler;
        private readonly OrhedgeContext _orhedgeContext;

        public CourseCategoryManagementService(ICategoryService categoryService, ICourseService courseService,
            IStudyMaterialService studyMaterialService, IErrorHandler errorHandler, OrhedgeContext orhedgeContext)
        {
            _courseService = courseService;
            _categoryService = categoryService;
            _studyMaterialService = studyMaterialService;
            _errorHandler = errorHandler;
            _orhedgeContext = orhedgeContext;
        }

        /// <summary>
        /// Related specific course with specific study program, and semester.
        /// </summary>
        /// <param name="courseId">Unique identifer for the course</param>
        /// <param name="studyProgram">Study program that needs to be related with specified course</param>
        /// <param name="semester">Semester that needs to be related with specified course</param>
        /// <returns>True if added, false if not</returns>
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
            catch (DbUpdateException ex)
            {
                return new ResultMessage<bool>(false, _errorHandler.Handle(ex));
            }
            catch (Exception ex)
            {
                return new ResultMessage<bool>(false, _errorHandler.Handle(ex));
            }
        }

        /// <summary>
        /// Returns total number of items that satisfy certain criteria.
        /// </summary>
        /// <param name="searchFor">Lookup word (optional)</param>
        /// <param name="studyPrograms">Unique identifiers of studyPrograms (optional)</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>Total number of items</returns>
        public int Count(StudyProgram[] studyPrograms, string searchFor = null)
             => GetDetailedCourses(studyPrograms, searchFor).Count();

        /// <summary>
        /// Deletes course, and related categories, study materials and connections with study programs.
        /// This method is exectuted in transaction scope.
        /// </summary>
        /// <param name="courseId">Unique identifier for the course</param>
        /// <returns>True if deleted, false if not</returns>
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

        /// <summary>
        /// Deletes course from study program and within certain semester.
        /// </summary>
        /// <param name="courseId">Unique identifer for the course</param>
        /// <param name="studyProgram">Study program from which course is being deleted</param>
        /// <param name="semester">Semester from which course if being deleted</param>
        /// <returns>True if deleted, false if not</returns>
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
            catch (DbUpdateException ex)
            {
                return new ResultMessage<bool>(false, _errorHandler.Handle(ex));
            }
            catch (Exception ex)
            {
                return new ResultMessage<bool>(false, _errorHandler.Handle(ex));
            }
        }

        /// <summary>
        /// Returns list of study programs and semesters that are related with specified course.
        /// </summary>
        /// <param name="courseId">Unique identifer for the course</param>
        /// <returns>List of tuples, where first item is semester, and second is study program</returns>
        public async Task<List<(Semester semester, StudyProgram studyProgram)>> GetCourseUsage(int courseId)
             => (await _orhedgeContext.CourseStudyPrograms
                                      .Where(x => x.CourseId == courseId)
                                      .Select(x => new { x.Semester, x.StudyProgram })
                                      .ToListAsync())
                                      .Select(x => (x.Semester, x.StudyProgram))
                                      .ToList();

        /// <summary>
        /// Returns filtered and naturally sorted list of courses, with items' count limit.
        /// </summary>
        /// <param name="offset">Number of elements to skip</param>
        /// <param name="itemsCount">Max number of elements to return</param>
        /// <param name="searchFor">Word used as search criteria</param>
        /// <param name="studyPrograms">List of allowed study programs</param>
        /// <returns>List of detailed courses</returns>
        public async Task<List<DetailedCourseCategoryDTO>> GetDetailedCourses(int offset, int itemsCount, StudyProgram[] studyPrograms, string searchFor = null)
        {
            try
            {
                List<DetailedCourseCategoryDTO> detailedCourses = GetDetailedCourses(studyPrograms, searchFor)
                                                                      .Skip(offset)
                                                                      .Take(itemsCount)
                                                                      .ToList();

                foreach (DetailedCourseCategoryDTO dto in detailedCourses)
                {
                    dto.Categories = await _categoryService.GetAll<NoSorting>(x => x.CourseId == dto.Course.CourseId && !x.Deleted);
                    dto.StudyMaterialsCount = await _studyMaterialService.Count(x => !x.Deleted && dto.Categories.Select(z => z.CategoryId).Contains(x.CategoryId));
                }
                return detailedCourses;
            }
            catch (ArgumentNullException ex)
            {
                _errorHandler.Handle(ex);
                return new List<DetailedCourseCategoryDTO>();
            }
            catch (Exception ex)
            {
                _errorHandler.Handle(ex);
                return new List<DetailedCourseCategoryDTO>();
            }
        }

        /// <summary>
        /// Returns name of a course based on its identifier.
        /// </summary>
        /// <param name="courseId">Unique identifier for the course</param>
        /// <returns>Name of the course</returns>
        public async Task<string> GetName(int courseId)
        {
            CourseDTO course = await _courseService.GetSingleOrDefault(x => !x.Deleted && x.CourseId == courseId);
            return course?.Name;
        }

        /// <summary>
        /// Adds course in database, as well as specified categories that need to be related to added course. 
        /// It also relates course with specific study program and semester.
        /// This method is defined in transaction scope.
        /// </summary>
        /// <param name="name">Name of the course to add</param>
        /// <param name="categories">List of the names of the categories that needs be added</param>
        /// <param name="semester">Belonging semester</param>
        /// <param name="studyProgram">Belonging study program</param>
        /// <returns>True if added, false if not.</returns>
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

        /// <summary>
        /// Filters, sorts and maps courses.k
        /// </summary>
        ///<exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        private IQueryable<DetailedCourseCategoryDTO> GetDetailedCourses(StudyProgram[] studyPrograms, string searchFor = null)
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
