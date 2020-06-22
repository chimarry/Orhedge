using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Shared;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class LocalDocumentService : IDocumentService
    {
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

