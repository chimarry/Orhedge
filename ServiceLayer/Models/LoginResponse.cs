using DatabaseLayer.Enums;

namespace ServiceLayer.Models
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public StudentPrivilege Privilege { get; set; }
    }
}
