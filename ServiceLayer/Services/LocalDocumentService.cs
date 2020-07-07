using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Shared;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class LocalDocumentService : IDocumentService
    {
        /// <summary>
        /// Based on specified path, this methods reads file data from local filesystem and returns array of bytes, wrapped with filename.
        /// </summary>
        /// <param name="storagePath">Path that indicates where file is located</param>
        public async Task<ResultMessage<BasicFileInfo>> DownloadFromStorage(string storagePath)
        {
            try
            {
                string fileName = Path.GetFileName(storagePath);
                byte[] fileData = await File.ReadAllBytesAsync(storagePath);
                return new ResultMessage<BasicFileInfo>(new BasicFileInfo(fileName, fileData), OperationStatus.Success);
            }
            catch (IOException ex)
            {
                return new ResultMessage<BasicFileInfo>(OperationStatus.FileSystemError, ex.Message);
            }
        }

        /// <summary>
        /// Saves array of bytes permanently on local filesystem  and relates
        /// that file with provided path on  aunique way - creates corresponding folder structure.
        /// </summary>
        /// <param name="storagePath">Path that indicates directory structure with a file</param>
        /// <param name="fileInfo">File's bytes</param>
        /// <returns>True if uploaded, false if not</returns>
        public async Task<ResultMessage<bool>> UploadDocumentToStorage(string storagePath, byte[] file)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(storagePath));
                using (FileStream fileStream = File.Create(storagePath))
                    await fileStream.WriteAsync(file);
                return new ResultMessage<bool>(true, OperationStatus.Success);
            }
            catch (IOException)
            {
                return new ResultMessage<bool>(false, OperationStatus.FileSystemError);
            }
        }
    }
}

