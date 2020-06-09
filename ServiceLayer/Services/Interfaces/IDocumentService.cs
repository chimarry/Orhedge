using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Shared;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface IDocumentService
    {
        Task<ResultMessage<BasicFileInfo>> DownloadFromStorage(string storagePath);

        Task<ResultMessage<bool>> UploadDocumentToStorage(string storagePath, byte[] fileInfo);
    }
}
