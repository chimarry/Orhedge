using Orhedge.Helpers;

namespace Orhedge.ViewModels
{
    public class PageableViewModel
    {
        public PageInformation PageInformation { get; set; }

        public PageableViewModel(PageInformation pageInformation)
        {
            PageInformation = pageInformation;
        }
    }
}
