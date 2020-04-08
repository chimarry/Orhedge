using Orhedge.Helpers;
using ServiceLayer.Students.Shared;
using System;

namespace Orhedge.ViewModels.StudyMaterial
{
    public class CreateStudyMaterialViewModel
    {
        public string Name { get; set; }

        public string Uri { get; set; }

        public DateTime UploadDate { get; set; }

        public int StudentId { get; set; }

        public int CategoryId { get; set; }

        public BasicFileInfo BasicFileInfo { get; set; }
    }
}
