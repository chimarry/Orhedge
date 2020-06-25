using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orhedge.ViewModels.StudyMaterial
{
    public class StudyMaterialViewModel
    {
        public int StudyMaterialId { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public DateTime UploadDate { get; set; }

        public string AuthorFullName { get; set; }

        public double TotalRating { get; set; }

        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        public int GivenRating { get; set; }

        public int StudentId { get; set; }
    }
}
