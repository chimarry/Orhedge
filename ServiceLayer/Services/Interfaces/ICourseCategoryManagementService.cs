using DatabaseLayer.Enums;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface ICourseCategoryManagementService
    {
        /// <summary>
        /// Counts number of courses that belong to specified list of study programs, and satisfy search criteria that can be determined by
        ///  the given search word.
        /// </summary>
        /// <param name="searchFor">Determines search criteria</param>
        /// <param name="studyPrograms">List of study programs</param>
        /// <returns>number of elements</returns>
        int Count(StudyProgram[] studyPrograms, string searchFor = null);

        /// <summary>
        /// Returns name of a course based on its identifier.
        /// </summary>
        /// <param name="courseId">Unique identifier for the course</param>
        /// <returns>Name of the course</returns>
        Task<string> GetName(int courseId);

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
        Task<ResultMessage<bool>> SaveCourse(string name, string[] categories, Semester semester, StudyProgram studyProgram);

        /// <summary>
        /// Deletes course from study program and within certain semester.
        /// </summary>
        /// <param name="courseId">Unique identifer for the course</param>
        /// <param name="studyProgram">Study program from which course is being deleted</param>
        /// <param name="semester">Semester from which course if being deleted</param>
        /// <returns>True if deleted, false if not</returns>
        Task<ResultMessage<bool>> DeleteFromStudyProgram(int courseId, StudyProgram studyProgram, Semester semester);

        /// <summary>
        /// Related specific course with specific study program, and semester.
        /// </summary>
        /// <param name="courseId">Unique identifer for the course</param>
        /// <param name="studyProgram">Study program that needs to be related with specified course</param>
        /// <param name="semester">Semester that needs to be related with specified course</param>
        /// <returns>True if added, false if not</returns>
        Task<ResultMessage<bool>> AddInStudyProgram(int courseId, StudyProgram studyProgram, Semester semester);

        /// <summary>
        /// Returns filtered and naturally sorted list of courses, with items' count limit.
        /// </summary>
        /// <param name="offset">Number of elements to skip</param>
        /// <param name="itemsCount">Max number of elements to return</param>
        /// <param name="searchFor">Word used as search criteria</param>
        /// <param name="studyPrograms">List of allowed study programs</param>
        /// <returns>List of detailed courses</returns>
        Task<List<DetailedCourseCategoryDTO>> GetDetailedCourses(int offset, int itemsCount, StudyProgram[] studyPrograms, string searchFor = null);

        /// <summary>
        /// Deletes course, and related categories, study materials and connections with study programs.
        /// This method is exectuted in transaction scope.
        /// </summary>
        /// <param name="courseId">Unique identifier for the course</param>
        /// <returns>True if deleted, false if not</returns>
        Task<ResultMessage<bool>> DeleteCourse(int courseId);

        /// <summary>
        /// Returns list of study programs and semesters that are related with specified course.
        /// </summary>
        /// <param name="courseId">Unique identifer for the course</param>
        /// <returns>List of tuples, where first item is semester, and second is study program</returns>
        Task<List<(Semester semester, StudyProgram studyProgram)>> GetCourseUsage(int courseId);
    }
}
