using Orhedge.Helpers;
using System;
using System.Collections.Generic;

namespace Orhedge.ViewModels.Admin
{
    public class AdminIndexViewModel : PageableViewModel
    {
        public List<StudentViewModel> Students { get; set; }

        public AdminIndexViewModel(List<StudentViewModel> students, PageInformation pageInformation) : base(pageInformation)
        {
            Students = students;
        }
    }
}
