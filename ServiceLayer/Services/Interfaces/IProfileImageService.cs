using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Students.Shared;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IProfileImageService
    {
        /// <summary>
        /// Returns profile image bytes that belong to specific student.
        /// </summary>
        /// <param name="studentId">Unique identifier for the student</param>
        /// <returns>Array of bytes wrapped with file name</returns>
        Task<ResultMessage<BasicFileInfo>> GetStudentProfileImage(int studentId);

        /// <summary>
        /// Saves student's profile image.
        /// </summary>
        /// <param name="file">Information about file</param>
        /// <returns>Uri of the image</returns>
        Task<ResultMessage<string>> SaveProfileImage(IUploadedFile file);
    }
}
