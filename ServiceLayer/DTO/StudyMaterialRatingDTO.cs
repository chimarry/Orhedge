﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTO
{
    public class StudyMaterialRatingDTO
    {
        public int StudyMaterialId { get; set; }
        public int AuthorId { get; set; }
        public int StudentId { get; set; }
        public double Rating { get; set; }
    }
}