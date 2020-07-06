using DatabaseLayer.Enums;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface ICourseCategoryManagementService
    {
        Task<int> Count(string searchFor = null, StudyProgram[] studyPrograms = null);
        Task<string> GetName(int courseId);
        Task<ResultMessage<bool>> SaveCourse(string name, string[] categories, Semester semester, StudyProgram studyProgram);
        Task<ResultMessage<bool>> DeleteFromStudyProgram(int courseId, StudyProgram studyProgram, Semester semester);
        Task<ResultMessage<bool>> AddInStudyProgram(int courseId, StudyProgram studyProgram, Semester semester);
        Task<List<DetailedCourseCategoryDTO>> GetDetailedCourses(int offset, int itemsCount, string searchFor = null, StudyProgram[] studyPrograms = null);
        Task<ResultMessage<bool>> DeleteCourse(int courseId);
        Task<List<(Semester semester, StudyProgram studyProgram)>> GetCourseUsage(int courseId);
    }
}
