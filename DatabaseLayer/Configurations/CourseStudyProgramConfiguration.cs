using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Configurations
{
    public class CourseStudyProgramConfiguration : IEntityTypeConfiguration<CourseStudyProgram>
    {
        /// <summary>
        /// Adds configuration for CourseStudyProgram so that all values are required, and primary key is not autoincrement, but a 
        /// combination of attributes: StudyProgram, CourseId, Semester.
        /// </summary>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CourseStudyProgram> builder)
        {
            builder.HasKey(x => new { x.StudyProgram, x.CourseId, x.Semester });
            builder.Property(x => x.StudyYear).IsRequired();
        }
    }
}
