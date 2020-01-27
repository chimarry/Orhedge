using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orhedge.ViewModels.Forum
{
    public class DiscussionPostViewModel
    {
        public string Content { get; set; }
        public AuthorViewModel Author { get; set; }
        public DateTime Created { get; set; }
        public bool Edited { get; set; }
    }
}
