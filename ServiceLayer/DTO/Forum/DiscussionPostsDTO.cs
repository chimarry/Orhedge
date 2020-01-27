using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTO.Forum
{
    public class DiscussionPostsDTO
    {
        public DiscussionPostDTO[] DiscussionPosts { get; set; }
        public StudentDTO[] Authors { get; set; }
    }
}
