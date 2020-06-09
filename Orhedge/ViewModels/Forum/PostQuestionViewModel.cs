using System.ComponentModel.DataAnnotations;

namespace Orhedge.ViewModels.Forum
{
    public class PostQuestionViewModel
    {
        [Required]
        public int ForumCategoryId { get; set; }
        [StringLength(500, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }
        [StringLength(1000, MinimumLength = 3)]
        [Required]
        public string Content { get; set; }
    }
}
