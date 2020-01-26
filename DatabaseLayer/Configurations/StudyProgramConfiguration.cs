using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Configurations
{
    public class StudyProgramConfiguration : IEntityTypeConfiguration<StudyProgram>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<StudyProgram> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Rank).IsRequired();
        }
    }
}
