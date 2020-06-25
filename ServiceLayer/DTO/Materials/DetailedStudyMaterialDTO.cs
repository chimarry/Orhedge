namespace ServiceLayer.DTO.Materials
{
    public class DetailedStudyMaterialDTO : StudyMaterialDTO
    {
        public string AuthorFullName { get; set; }

        public string CategoryName { get; set; }

        public int? GivenRating { get; set; }

        public DetailedStudyMaterialDTO(StudyMaterialDTO baseDTO)
        {
            Uri = baseDTO.Uri;
            Name = baseDTO.Name;
            StudyMaterialId = baseDTO.StudyMaterialId;
            CategoryId = baseDTO.CategoryId;
            UploadDate = baseDTO.UploadDate;
            StudentId = baseDTO.StudentId;
            TotalRating = baseDTO.TotalRating;
            Deleted = baseDTO.Deleted;
        }
    }
}
