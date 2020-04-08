using DatabaseLayer;
using DatabaseLayer.Entity;
using ServiceLayer.AutoMapper;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Shared;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class LocalDocumentService : IDocumentService
    {
        public Task<BasicFileInfo> DownloadFromStorage(string storagePath)
        {
            throw new NotImplementedException();
        }

        public async Task<Status> UploadDocumentToStorage(string storagePath, byte[] file)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(storagePath));
                using (FileStream fileStream = File.Create(storagePath))
                    await fileStream.WriteAsync(file);
                return Status.SUCCESS;
            }
            catch (IOException)
            {
                throw;
            }
        }
    }
}

