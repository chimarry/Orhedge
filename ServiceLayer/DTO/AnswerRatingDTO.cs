using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTO
{
    public class AnswerRatingDTO
    {
        public int StudentId { get; set; }
        public int AnswerId { get; set; }
        public double Rating { get; set; }
    }
}
