using System;

namespace ServiceLayer.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public int AnswerId { get; set; }
        public int StudentId { get; set; }
        public DateTime Created { get; set; }
        public bool Edited { get; set; }
        public bool Deleted { get; set; }
        public string Content { get; set; }
    }
}
