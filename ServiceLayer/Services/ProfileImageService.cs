using ImageMagick;
using Microsoft.Extensions.Configuration;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.Shared;
using ServiceLayer.Students.Shared;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ProfileImageService : IProfileImageService
    {
        private readonly IDocumentService _docService;
        private readonly IStudentService _studentService;
        private readonly IErrorHandler _errorHandler;
        private readonly int _resizeWidth;
        private readonly int _resizeHeight;

        public ProfileImageService(IDocumentService documentService, IStudentService studentService, IErrorHandler errorHandler, IConfiguration config)
        {
            _docService = documentService;
            _studentService = studentService;
            _errorHandler = errorHandler;
            (_resizeWidth, _resizeHeight) = (config.GetValue<int>("ProfileImageSettings:ResizeWidth"), config.GetValue<int>("ProfileImageSettings:ResizeHeight"));
        }

        /// <summary>
        /// Returns profile image bytes that belong to specific student.
        /// </summary>
        /// <param name="studentId">Unique identifier for the student</param>
        /// <returns>Array of bytes wrapped with file name</returns>
        public async Task<ResultMessage<BasicFileInfo>> GetStudentProfileImage(int studentId)
        {
            ResultMessage<StudentDTO> result = await _studentService.GetSingleOrDefault(s => s.StudentId == studentId && !s.Deleted);
            if (result.IsSuccess)
            {
                string photoPath = result.Result.Photo;
                if (photoPath == null)
                    return new ResultMessage<BasicFileInfo>(OperationStatus.NotFound);

                return await _docService.DownloadFromStorage(photoPath);
            }
            else
                return new ResultMessage<BasicFileInfo>(OperationStatus.NotFound);
        }

        /// <summary>
        /// Saves student's profile image.
        /// </summary>
        /// <param name="file">Information about file</param>
        /// <returns>Uri of the image</returns>
        public async Task<ResultMessage<string>> SaveProfileImage(IUploadedFile file)
        {

            byte[] processedImage;
            using (Stream imgStream = file.OpenReadStream())
            {
                processedImage = PreprocessImage(imgStream);
            }
            if (processedImage == null)
                return new ResultMessage<string>(OperationStatus.UnknownError);
            string path = PathBuilder.BuildPathForProfileImage();
            ResultMessage<bool> result = await _docService.UploadDocumentToStorage(path, processedImage);
            if (result.IsSuccess)
                return new ResultMessage<string>(path);
            else
                return new ResultMessage<string>(OperationStatus.FileSystemError);
        }

        /// <summary>
        /// Image gets resized.
        /// </summary>
        private byte[] PreprocessImage(Stream imgStream)
        {
            try
            {
                using (MagickImage img = new MagickImage(imgStream))
                {
                    img.Resize(_resizeWidth, _resizeHeight);
                    return img.ToByteArray();
                }
            }
            catch (MagickException ex)
            {
                _errorHandler.Handle(ex);
                return null;
            }
        }
    }
}
