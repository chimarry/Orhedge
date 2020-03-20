using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTO
{
    public class DiscussionPostDTO
    {
        public int DiscussionPostId { get; set; }
        public int StudentId { get; set; }
        public int TopicId { get; set; }
        public DateTime Created { get; set; }
        public bool Edited { get; set; }
        public string Content { get; set; }
        public bool Deleted { get; set; }
    }
}
