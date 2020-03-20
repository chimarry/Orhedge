using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTO
{
    public class TopicRatingDTO
    {
        public int TopicId { get; set; }
        public int StudentId { get; set; }
        public double Rating { get; set; }
    }
}
