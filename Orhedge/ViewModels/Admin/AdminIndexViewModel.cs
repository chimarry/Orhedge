using System;
using System.Collections.Generic;

namespace Orhedge.ViewModels.Admin
{
    public class AdminIndexViewModel
    {
        public List<StudentViewModel> Students { get; set; }

        public int TotalNumberOfStudents { get; set; }

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value > GetLastPageNumber() ? GetLastPageNumber() : value;
        }
        public const int MaxNumberOfStudents = 2;

        private int _pageNumber;

        public AdminIndexViewModel(List<StudentViewModel> students, int totalNumberOfStudents, int pageNumber)
        {
            Students = students;
            TotalNumberOfStudents = totalNumberOfStudents;
            PageNumber = totalNumberOfStudents == 0 ? -1 : pageNumber;
        }

        public int GetLastPageNumber()
            => (int)Math.Ceiling(TotalNumberOfStudents / (double)MaxNumberOfStudents) - 1;

        public int GetFirstPageNumber()
            => 0;

        public int GetPreviousPageNumber()
            => PageNumber == 0 ? PageNumber : PageNumber - 1;

        public int GetNextPageNumber()
            => PageNumber + 1;

        public bool IsLast()
            => PageNumber == GetLastPageNumber();

        public bool IsFirst()
            => PageNumber == GetFirstPageNumber();
    }
}
