using DatabaseLayer.Configurations;
using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer
{
    public class OrhedgeContext : DbContext
    {
        public OrhedgeContext(DbContextOptions<OrhedgeContext> dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudyMaterialRating> StudyMaterialRatings { get; set; }
        public DbSet<StudyMaterial> StudyMaterials { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<DiscussionPost> DiscussionPosts { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ForumCategory> ForumCategories { get; set; }
        public DbSet<TopicRating> TopicRatings { get; set; }
        public DbSet<AnswerRating> AnswerRatings { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Discussion> Discussions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new StudyMaterialConfiguration());
            modelBuilder.ApplyConfiguration(new StudyMaterialRatingConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new RegistrationConfiguration());
            modelBuilder.ApplyConfiguration(new TopicConfiguration());
            modelBuilder.ApplyConfiguration(new DiscussionPostConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new ForumCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TopicRatingConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerRatingConfiguration());

        }
    }
}
