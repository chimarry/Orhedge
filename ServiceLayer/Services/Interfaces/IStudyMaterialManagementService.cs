using ServiceLayer.DTO;
using ServiceLayer.DTO.Materials;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface IStudyMaterialManagementService
    {
        /// <summary>
        /// Returns semesters with courses organized by study programs.
        /// </summary>
        Task<HashSet<DetailedSemesterDTO>> GetSemestersWithAllInformation();

        /// <summary>
        /// Returns courses with categories, that belong to specific study year.
        /// </summary>
        /// <param name="year">Value from 0 to 3</param>
        Task<List<CourseCategoryDTO>> GetCoursesByYear(int year);

        /// <summary>
        /// Saves files as study materials provided by specified student that need to be related to specific category.
        /// This method operates within transaction scope.
        /// </summary>
        /// <param name="categoryId">Unique identifier for the category</param>
        /// <param name="studentId">Unique identifier for the student</param>
        /// <param name="basicFileInfo">List of information about files (bytes, file name)</param>
        /// <returns>True if saved, false if not.</returns>
        Task<ResultMessage<bool>> SaveStudyMaterials(int categoryId, int studentId, List<BasicFileInfo> basicFileInfo);

        /// <summary>
        /// Retuns bytes and file name that represent certain study material.
        /// </summary>
        /// <param name="studyMaterialId">Unique identifier of a study material</param>
        /// <returns></returns>
        Task<ResultMessage<BasicFileInfo>> DownloadStudyMaterial(int studyMaterialId);

        /// <summary>
        /// Counts all study materials that satisfy all criterias - they must match loookup word, must be contained in specific categories, within specific course.
        /// </summary>
        /// <param name="courseId">Unique identifier for the course</param>
        /// <param name="searchFor">Lookup word</param>
        /// <param name="categories">List of categories used in filter</param>
        /// <returns>Number of elements</returns>
        Task<int> Count(int courseId, string searchFor = null, int[] categories = null);

        /// <summary>
        /// Returns list of detailed study materials (with rating), filtered, sorted and limited with number of items per request. 
        /// All returned study materials belong to specified course.
        /// </summary>
        /// <typeparam name="TKey">Type of property on which sorting is applied</typeparam>
        /// <param name="offset">Number of elements to skip</param>
        /// <param name="itemsCount">Number of elements to take</param>
        /// <param name="sortKeySelector">Function that gives property on which sorting is applied 
        /// (optional) - if not specified, items are sorted ascending by date</param>
        /// <param name="asc">Indicates direction of sorting optional) - default is ascending</param>
        /// <param name="courseId">Unique identifier of a course</param>
        /// <param name="searchFor">Lookup word (optional)</param>
        /// <param name="categories">List of categories (optional) - if not specified, are categories are included</param>
        Task<List<DetailedStudyMaterialDTO>> GetDetailedStudyMaterials<TKey>(int courseId, int offset, int itemsCount, string searchFor = null, int[] categories = null,
                                                                             Func<DetailedStudyMaterialDTO, TKey> sortKeySelector = null, bool asc = true);

        /// <summary>
        /// Applies rating student gave for certain study material, updates total rating of that study material and material's author.
        /// </summary>
        /// <param name="studyMaterialId">Unique identifier for the study material</param>
        /// <param name="studentId">Unique identifier for the student that rates material</param>
        /// <param name="authorId">Unique identifier for the student that uploaded material</param>
        /// <param name="rating">Given rating</param>
        /// <returns>True if rated, false if not</returns>
        Task<ResultMessage<bool>> Rate(int studyMaterialId, int studentId, int authorId, int rating);

        /// <summary>
        /// Foreach element of provided list of study materials, this method returns rating that specified student gave for that study material.
        /// </summary>
        /// <param name="studentId">Unique identifier of a student</param>
        /// <param name="studyMaterials">List of study materials</param>
        Task<List<DetailedStudyMaterialDTO>> AppendRating(int studentId, List<DetailedStudyMaterialDTO> studyMaterials);

        /// <summary>
        /// Moves specified study material to new category 
        /// </summary>
        /// <param name="studyMaterialId">Unique identifier for the study material</param>
        /// <param name="categoryId">Unique identifier for the category of course</param>
        /// <returns></returns>
        Task<ResultMessage<bool>> Move(int studyMaterialId, int categoryId);
    }
}
