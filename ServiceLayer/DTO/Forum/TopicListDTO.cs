namespace ServiceLayer.DTO.Forum
{
    public class TopicListDTO
    {
        public TopicDTO[] Topics { get; set; }
        public StudentDTO[] Authors { get; set; }
        public int[] PostCounts { get; set; }
        public int Page { get; set; }
        public int Category { get; set; }
        public int PageCount { get; set; }

    }
}
