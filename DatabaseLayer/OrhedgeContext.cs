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

        public DbSet<CourseStudyProgram> CourseStudyPrograms { get; set; }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        /// <summary>
        /// Applies configurations for each entity class.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new StudyMaterialConfiguration());
            modelBuilder.ApplyConfiguration(new StudyMaterialRatingConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new RegistrationConfiguration());
            modelBuilder.ApplyConfiguration(new CourseStudyProgramConfiguration());
            modelBuilder.ApplyConfiguration(new ChatMessageConfiguration());
        }
    }
}
