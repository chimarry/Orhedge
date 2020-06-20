using System;

namespace Orhedge.Helpers
{
    public class PageInformation
    {
        public int PageNumber { get; set; }

        public int TotalNumberOfItems { get; set; }

        public int MaxNumberOfItemsPerPage { get; set; }

        public PageInformation(int pageNumber, int totalNumberOfItems, int maxNumberOfItemsPerPage)
        {
            MaxNumberOfItemsPerPage = maxNumberOfItemsPerPage;
            TotalNumberOfItems = totalNumberOfItems;
            PageNumber = PageNumber = pageNumber > LastPageNumber ? LastPageNumber : pageNumber;
        }

        public int LastPageNumber
           => (int)Math.Ceiling(TotalNumberOfItems / (double)MaxNumberOfItemsPerPage) - 1;

        public int FirstPageNumber => 0;

        public int PreviousPageNumber => PageNumber == 0 ? PageNumber : PageNumber - 1;

        public int NextPageNumber => PageNumber + 1;

        public bool IsLast => PageNumber == LastPageNumber;

        public bool IsFirst => PageNumber == FirstPageNumber;
    }
}
