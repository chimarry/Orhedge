using System;

namespace ServiceLayer.DTO
{
    public class TopicDTO
    {
        public int TopicId { get; set; }
        public int StudentId { get; set; }
        public string Title { get; set; }
        public int ForumCategoryId { get; set; }
        public DateTime Created { get; set; }
        public bool Locked { get; set; }
        public bool Deleted { get; set; }
        public string Content { get; set; }
        public DateTime LastPost { get; set; }
    }
}
