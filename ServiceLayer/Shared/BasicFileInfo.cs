using System;

namespace ServiceLayer.Students.Shared
{
    /// <summary>
    /// This class represents basic information about files.
    /// </summary>
    public class BasicFileInfo
    {
        public long UploadUniqueId { get; set; }

        public string FileName { get; set; }

        public byte[] FileData { get; set; }

        public BasicFileInfo(long uploadUniqueId, string fileName, byte[] fileData) : this(uploadUniqueId, fileData)
        {
            FileName = fileName;
        }
        public BasicFileInfo(long uploadUniqueId, byte[] fileData)
        {
            UploadUniqueId = uploadUniqueId;
            FileData = fileData;
        }
        public BasicFileInfo(string fileName, byte[] fileData)
        {
            FileName = fileName;
            FileData = fileData;
        }

        public void GenerateFileName(string fileExtension)
        {
            FileName = "File" + Guid.NewGuid() + "." + fileExtension;
        }
    }
}
