using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTO
{
    public class StudyMaterialDTO
    {
        public int StudyMaterialId { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public DateTime UploadDate { get; set; }

        public int StudentId { get; set; }

        public int CategoryId { get; set; }

        public bool Deleted { get; set; }
    }
}
