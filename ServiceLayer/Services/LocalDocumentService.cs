using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Shared;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class LocalDocumentService : IDocumentService
    {
        public Task<ResultMessage<BasicFileInfo>> DownloadFromStorage(string storagePath)
        {
            throw new NotImplementedException();
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

