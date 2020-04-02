﻿using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Student> builder)
        {
            builder.HasIndex(x => x.Username).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Index).IsUnique();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Deleted).HasDefaultValue(false);
            builder.Property(x => x.Rating).IsRequired();
            builder.Property(x => x.Privilege).IsRequired();
            builder.Property(x => x.Index).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.Salt).IsRequired();
            builder.Property(x => x.Description).HasDefaultValue(string.Empty);

            // If student is deleted, child entities are not deleted
            // This is not a problem since student can not be deleted
            builder.HasMany(x => x.Topics).WithOne(x => x.Student).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.TopicRatings).WithOne(x => x.Student).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Answers).WithOne(x => x.Student).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.AnswerRatings).WithOne(x => x.Student).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Comments).WithOne(x => x.Student).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.DiscussionPosts).WithOne(x => x.Student).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
