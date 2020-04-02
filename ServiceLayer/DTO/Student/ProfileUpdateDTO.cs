using ServiceLayer.Helpers;

namespace ServiceLayer.DTO.Student
{
    public class ProfileUpdateDTO
    {
        public string Username { get; set; }
        public string Description { get; set; }
        public IUploadedFile Photo { get; set; }
    }
}
