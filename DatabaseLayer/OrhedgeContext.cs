
using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer
{
    public class OrhegeContext : DbContext
    {
        public OrhegeContext(DbContextOptions<OrhegeContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<User> Users { get; set; }
    }
}
