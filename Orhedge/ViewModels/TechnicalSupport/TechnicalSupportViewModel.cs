using Orhedge.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orhedge.ViewModels.TechnicalSupport
{
    public class TechnicalSupportViewModel : PageableViewModel
    {
        public TechnicalSupportViewModel(PageInformation pageInformation) : base(pageInformation)
        {
        }

        public List<ChatMessageViewModel> ChatMessages { get; set; }
    }
}
