using DatabaseLayer.Enums;

namespace Orhedge.ViewModels.Admin
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public double Rating { get; set; }
        public string Index { get; set; }
        public StudentPrivilege Privilege { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public int? PhotoVersion { get; set; }
    }
}
