using Microsoft.AspNetCore.Http;
using ServiceLayer.Helpers;
using System.IO;
using System.Threading.Tasks;

namespace Orhedge.Helpers
{
    public class FormFile : IUploadedFile
    {
        private readonly IFormFile _formFile;

        public FormFile(IFormFile file)
            => _formFile = file;
        public long Size => _formFile.Length;

        public async Task CopyToAsync(Stream target)
            => await _formFile.CopyToAsync(target);

        public async Task<byte[]> GetFileDataAsync()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                await _formFile.CopyToAsync(stream);
                return stream.ToArray();
            }
        }
    }
}
