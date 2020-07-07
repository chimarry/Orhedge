using System;

namespace ServiceLayer.DTO
{
    public class StudyMaterialRatingDTO
    {
        public int StudyMaterialId { get; set; }

        public int AuthorId { get; set; }

        public int StudentId { get; set; }

        public double Rating { get; set; }

        public override bool Equals(object obj)
        {
            return obj is StudyMaterialRatingDTO dTO &&
                   StudyMaterialId == dTO.StudyMaterialId &&
                   AuthorId == dTO.AuthorId &&
                   StudentId == dTO.StudentId &&
                   Rating == dTO.Rating;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StudyMaterialId, AuthorId, StudentId, Rating);
        }
    }
}
