using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orhedge.ViewModels.StudyMaterial
{
    public class MoveStudyMaterialViewModel
    {
        /// <summary>
        /// Unique identifier of a study material to move
        /// </summary>
        public int StudyMaterialId { get; set; }

        /// <summary>
        /// Unique identifier for a course 
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Unique identifier of a new category for study material
        /// </summary>
        public int CategoryId { get; set; }
    }
}
