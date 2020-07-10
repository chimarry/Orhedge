namespace ServiceLayer.DTO.Student
{
    public class DeletedStudentDTO : StudentDTO
    {
        public const string FullName = "Orhedge User";

        public DeletedStudentDTO(StudentDTO deletedStudent)
        {
            Name = FullName;
            LastName = string.Empty;
            Index = "**";
            Email = "**";
            Username = deletedStudent.Username;
            Photo = string.Empty;
            PhotoVersion = 0;
            Description = string.Empty;
            Rating = deletedStudent.Rating;
            Privilege = deletedStudent.Privilege;
        }

        public override string Initials { get => "**"; }
    }
}
