using System;

namespace ServiceLayer.DTO
{
    public class AnswerDTO
    {
        public int AnswerId { get; set; }
        public int TopicId { get; set; }
        public int StudentId { get; set; }
        public DateTime Created { get; set; }
        public bool Deleted { get; set; }
        public bool Edited { get; set; }
        public string Content { get; set; }
        public bool BestAnswer { get; set; }
    }
}
