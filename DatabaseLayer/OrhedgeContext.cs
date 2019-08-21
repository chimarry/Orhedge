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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Student>(new StudentConfiguration());
            modelBuilder.ApplyConfiguration<StudyMaterial>(new StudyMaterialConfiguration());
            modelBuilder.ApplyConfiguration<StudyMaterialRating>(new StudyMaterialRatingConfiguration());
            modelBuilder.ApplyConfiguration<Category>(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration<Course>(new CourseConfiguration());

        }
    }
}
