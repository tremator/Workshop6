using Microsoft.EntityFrameworkCore;

namespace api.Models
{
      public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> users { get; set; }
        
    }
}