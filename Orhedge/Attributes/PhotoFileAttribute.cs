using ImageMagick;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Orhedge.Attributes
{
    public class PhotoFileAttribute : ValidationAttribute
    {
        private string[] _subtypes;
        public const string MIME_TYPE = "image";

        public PhotoFileAttribute(params string[] subtypes)
            => _subtypes = subtypes;
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            IFormFile file = (IFormFile)value;
            string contentType = file.ContentType.ToLowerInvariant();
            string[] mimeSplit = contentType.Split("/");
            return mimeSplit.Length == 2 &&
                mimeSplit[0] == MIME_TYPE &&
                _subtypes.Contains(mimeSplit[1])
                && ValidContents(file);
        }


        /// <summary>
        /// Determines whether image file can be parsed as specified by subtypes
        /// </summary>
        /// <param name="file">Image file</param>
        /// <returns>true if file is actually specified image type, false otherwise</returns>
        private bool ValidContents(IFormFile file)
        {
            try
            {
                using (Stream imgStream = file.OpenReadStream())
                using (MagickImage img = new MagickImage(imgStream))
                {
                    return _subtypes.Contains(img.FormatInfo.MimeType.Split('/').Last());
                }
            }
            catch(MagickException)
            {
                return false;
            }
        }
    }
}
