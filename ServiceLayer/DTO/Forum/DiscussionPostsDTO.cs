namespace ServiceLayer.DTO.Forum
{
    public class DiscussionPostsDTO
    {
        public DiscussionPostDTO[] DiscussionPosts { get; set; }
        public StudentDTO[] Authors { get; set; }
    }
}
