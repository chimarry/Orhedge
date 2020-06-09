using DatabaseLayer.Enums;

namespace Orhedge.ViewModels.Admin
{
    public class EditStudentViewModel
    {
        public int StudentId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Index { get; set; }

        public StudentPrivilege Privilege { get; set; }
    }
}
