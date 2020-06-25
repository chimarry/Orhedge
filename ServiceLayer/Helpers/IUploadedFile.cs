using System.IO;
using System.Threading.Tasks;

namespace ServiceLayer.Helpers
{
    public interface IUploadedFile
    {
        long Size { get; }
        Task CopyToAsync(Stream target);
        Task<byte[]> GetFileDataAsync();
        Stream OpenReadStream();
    }
}
