using Microsoft.EntityFrameworkCore;

namespace scientist.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Scientist> Scientist { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();  
        }
    }
}
