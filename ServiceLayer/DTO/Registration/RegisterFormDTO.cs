using DatabaseLayer.Enums;

namespace ServiceLayer.DTO
{
    public class RegisterFormDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Index { get; set; }
        public StudentPrivilege Privilege { get; set; }
    }
}
