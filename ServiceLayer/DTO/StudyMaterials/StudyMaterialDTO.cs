using System;

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

        public double TotalRating { get; set; }

        public bool Deleted { get; set; }

        public override bool Equals(object obj)
        {
            return obj is StudyMaterialDTO dTO &&
                   Uri == dTO.Uri;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Uri);
        }
    }
}
