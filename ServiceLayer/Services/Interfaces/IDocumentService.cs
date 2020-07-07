using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Shared;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface IDocumentService
    {
        /// <summary>
        /// Based on specified path, method must have capability to read bytes from filesystem, or cloud, 
        /// depending on implementation type, and return that array of bytes, wrapped with filename.
        /// </summary>
        /// <param name="storagePath">Path that indicates where file is located</param>
        Task<ResultMessage<BasicFileInfo>> DownloadFromStorage(string storagePath);

        /// <summary>
        /// Saves array of bytes permanently on filesystem or cloud, depending on implementation type, and relates
        /// that file with provided path on a unique way.
        /// </summary>
        /// <param name="storagePath">Path that indicates relationship with file. For example, where file should be located</param>
        /// <param name="fileInfo">File's bytes</param>
        /// <returns>True if uploaded, false if not</returns>
        Task<ResultMessage<bool>> UploadDocumentToStorage(string storagePath, byte[] fileInfo);
    }
}
