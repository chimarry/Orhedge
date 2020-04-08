using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Shared;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface IDocumentService
    {
        Task<BasicFileInfo> DownloadFromStorage(string storagePath);

        Task<Status> UploadDocumentToStorage(string storagePath, byte[] fileInfo);
    }
}
