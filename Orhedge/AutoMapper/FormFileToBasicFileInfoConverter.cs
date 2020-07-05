using AutoMapper;
using Microsoft.AspNetCore.Http;
using ServiceLayer.Students.Shared;
using System.IO;

namespace Orhedge.AutoMapper
{
    /// <summary>
    /// Gives mechanism of converting IFileForm object into BasicFileInfo object. It copies file's bytes and name.
    /// </summary>
    public class FormFileToBasicFileInfoConverter : ITypeConverter<IFormFile, BasicFileInfo>
    {
        public BasicFileInfo Convert(IFormFile source, BasicFileInfo destination, ResolutionContext context)
        {
            using (MemoryStream memoryStream = new MemoryStream(new byte[source.Length]))
            {
                source.CopyTo(memoryStream);
                destination = new BasicFileInfo(Path.GetFileName(source.FileName), memoryStream.ToArray());
            }
            return destination;
        }
    }
}
