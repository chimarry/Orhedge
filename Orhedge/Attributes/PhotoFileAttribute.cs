using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
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
                _subtypes.Contains(mimeSplit[1]);
        }
    }
}
