using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Configurations
{
    public class CourseStudyProgramConfiguration : IEntityTypeConfiguration<CourseStudyProgram>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CourseStudyProgram> builder)
        {
            builder.HasKey(x => new { x.StudyProgram, x.CourseId });
            builder.Property(x => x.Semester).IsRequired();
            builder.Property(x => x.StudyYear).IsRequired();
        }
    }
}
