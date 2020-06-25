﻿// <auto-generated />
using System;
using DatabaseLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DatabaseLayer.Migrations
{
    [DbContext(typeof(OrhedgeContext))]
    [Migration("20200620201217_AddChatMessages")]
    partial class AddChatMessages
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DatabaseLayer.Entity.Answer", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("BestAnswer")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("Created");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("Edited")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int>("StudentId");

                    b.Property<int>("TopicId");

                    b.HasKey("AnswerId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TopicId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("DatabaseLayer.Entity.AnswerRating", b =>
                {
                    b.Property<int>("AnswerId");

                    b.Property<int>("StudentId");

                    b.Property<double>("Rating");

                    b.HasKey("AnswerId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("AnswerRatings");
                });

            modelBuilder.Entity("DatabaseLayer.Entity.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseId");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("CategoryId");

                    b.HasIndex("CourseId", "Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DatabaseLayer.Entity.ChatMessage", b =>
                {
                    b.Property<int>("ChatMessageId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<int>("StudentId");

                    b.HasKey("ChatMessageId");

                    b.HasIndex("StudentId");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("DatabaseLayer.Entity.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnswerId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("Created");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("Edited")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int>("StudentId");

                    b.HasKey("CommentId");

                    b.HasIndex("AnswerId");

                    b.HasIndex("StudentId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DatabaseLayer.Entity.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("CourseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("DatabaseLayer.Entity.CourseStudyProgram", b =>
                {
                    b.Property<int>("StudyProgram");

                    b.Property<int>("CourseId");

                    b.Property<int>("Semester");

                    b.Property<int>("StudyYear");

                    b.HasKey("StudyProgram", "CourseId", "Semester");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseStudyPrograms");
                });

            modelBuilder.Entity("DatabaseLayer.Entity.DiscussionPost", b =>
                {
                    b.Property<int>("DiscussionPostId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("Created");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("Edited")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int>("StudentId");

                    b.Property<int>("TopicId");

                    b.HasKey("DiscussionPostId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TopicId");

                    b.ToTable("DiscussionPosts");
                });

            modelBuilder.Entity("DatabaseLayer.Entity.ForumCategory", b =>
                {
                    b.Property<int>("ForumCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Order");

                    b.HasKey("ForumCategoryId");

                    b.HasIndex("Order")
                        .IsUnique();

                    b.ToTable("ForumCategories");
                });

            modelBuilder.Entity("DatabaseLayer.Entity.Registration", b =>
                {
                    b.Property<int>("RegistrationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("Index");

                    b.Property<string>("LastName");

                    b.Property<int>("Privilege");

                    b.Property<string>("RegistrationCode");

                    b.Property<DateTime>("Timestamp");

                    b.Property<bool>("Used")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.HasKey("RegistrationId");

                    b.HasIndex("Email");

                    b.HasIndex("RegistrationCode")
                        .IsUnique()
                        .HasFilter("[RegistrationCode] IS NOT NULL");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("DatabaseLayer.Entity.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Description")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Index")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<string>("Photo");

                    b.Property<int>("Privilege");

                    b.Property<double>("Rating");

                    b.Property<string>("Salt")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("StudentId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Index")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("DatabaseLayer.Entity.StudyMaterial", b =>
                {
                    b.Property<int>("StudyMaterialId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("StudentId");

                    b.Property<double>("TotalRating");

                    b.Property<DateTime>("UploadDate");

                    b.Property<string>("Uri")
                        .IsRequired();

                    b.HasKey("StudyMaterialId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("StudentId");

                    b.HasIndex("Uri")
                        .IsUnique();

                    b.ToTable("StudyMaterials");
                });

            modelBuilder.Entity("DatabaseLayer.Entity.StudyMaterialRating", b =>
                {
                    b.Property<int>("StudentId");

                    b.Property<int>("StudyMaterialId");

                    b.Property<int>("AuthorId");

                    b.Property<double>("Rating");

                    b.HasKey("StudentId", "StudyMaterialId");

                    b.HasIndex("StudyMaterialId");

                    b.ToTable("StudyMaterialRatings");
                });

            modelBuilder.Entity("DatabaseLayer.Entity.Topic", b =>
                {
                    b.Property<int>("TopicId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("Created");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("ForumCategoryId");

                    b.Property<DateTime>("LastPost");

                    b.Property<bool>("Locked")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int>("StudentId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("TopicId");

                    b.HasIndex("ForumCategoryId");

                    b.HasIndex("StudentId");

                    b.ToTable("Topics");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Topic");
                });

            modelBuilder.Entity("DatabaseLayer.Entity.TopicRating", b =>
                {
                    b.Property<int>("StudentId");

                    b.Property<int>("TopicId");

                    b.Property<double>("Rating");

                    b.HasKey("StudentId", "TopicId");

                    b.HasIndex("TopicId");

                    b.ToTable("TopicRatings");
                });

            modelBuilder.Entity("DatabaseLayer.Entity.Discussion", b =>
                {
                    b.HasBaseType("DatabaseLayer.Entity.Topic");


                    b.ToTable("Discussion");

                    b.HasDiscriminator().HasValue("Discussion");
                });

            modelBuilder.Entity("DatabaseLayer.Entity.Question", b =>
                {
                    b.HasBaseType("DatabaseLayer.Entity.Topic");


                    b.ToTable("Question");

                    b.HasDiscriminator().HasValue("Question");
                });

            modelBuilder.Entity("DatabaseLayer.Entity.Answer", b =>
                {
                    b.HasOne("DatabaseLayer.Entity.Student", "Student")
                        .WithMany("Answers")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DatabaseLayer.Entity.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DatabaseLayer.Entity.AnswerRating", b =>
                {
                    b.HasOne("DatabaseLayer.Entity.Answer", "Answer")
                        .WithMany("AnswerRatings")
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DatabaseLayer.Entity.Student", "Student")
                        .WithMany("AnswerRatings")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DatabaseLayer.Entity.Category", b =>
                {
                    b.HasOne("DatabaseLayer.Entity.Course", "Course")
                        .WithMany("Categories")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DatabaseLayer.Entity.ChatMessage", b =>
                {
                    b.HasOne("DatabaseLayer.Entity.Student", "Messeger")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DatabaseLayer.Entity.Comment", b =>
                {
                    b.HasOne("DatabaseLayer.Entity.Answer", "Answer")
                        .WithMany("Comments")
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DatabaseLayer.Entity.Student", "Student")
                        .WithMany("Comments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DatabaseLayer.Entity.CourseStudyProgram", b =>
                {
                    b.HasOne("DatabaseLayer.Entity.Course", "Course")
                        .WithMany("CourseStudyPrograms")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DatabaseLayer.Entity.DiscussionPost", b =>
                {
                    b.HasOne("DatabaseLayer.Entity.Student", "Student")
                        .WithMany("DiscussionPosts")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DatabaseLayer.Entity.Discussion", "Discussion")
                        .WithMany("DiscussionPosts")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DatabaseLayer.Entity.StudyMaterial", b =>
                {
                    b.HasOne("DatabaseLayer.Entity.Category", "Category")
                        .WithMany("StudyMaterials")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DatabaseLayer.Entity.Student", "Student")
                        .WithMany("StudyMaterials")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DatabaseLayer.Entity.StudyMaterialRating", b =>
                {
                    b.HasOne("DatabaseLayer.Entity.Student", "Student")
                        .WithMany("StudyMaterialRatingsStudents")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DatabaseLayer.Entity.StudyMaterial", "StudyMaterial")
                        .WithMany("StudyMaterialRatings")
                        .HasForeignKey("StudyMaterialId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DatabaseLayer.Entity.Topic", b =>
                {
                    b.HasOne("DatabaseLayer.Entity.ForumCategory", "ForumCategory")
                        .WithMany("Topics")
                        .HasForeignKey("ForumCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DatabaseLayer.Entity.Student", "Student")
                        .WithMany("Topics")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DatabaseLayer.Entity.TopicRating", b =>
                {
                    b.HasOne("DatabaseLayer.Entity.Student", "Student")
                        .WithMany("TopicRatings")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DatabaseLayer.Entity.Topic", "Topic")
                        .WithMany("TopicRatings")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}