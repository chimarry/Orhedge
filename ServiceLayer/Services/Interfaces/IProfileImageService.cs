using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Students.Shared;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IProfileImageService
    {
        Task<ResultMessage<BasicFileInfo>> GetStudentProfileImage(int studentId);
        Task<ResultMessage<string>> SaveProfileImage(IUploadedFile file);
    }
}
